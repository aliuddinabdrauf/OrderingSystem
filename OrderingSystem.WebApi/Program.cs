using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using OrderingSystem.Application;
using OrderingSystem.Application.Hubs;
using OrderingSystem.Application.Utils;
using OrderingSystem.Infrastructure.Databases.OrderingSystem;
using OrderingSystem.Infrastructure.Dtos;
using OrderingSystem.WebApi;
using OrderingSystem.WebApi.Middlewares;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering System", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

//logging using serilog
var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Debug()
    .WriteTo.Logger(l => l.Filter
        .ByIncludingOnly(evt => evt.Level is LogEventLevel.Error or LogEventLevel.Warning or LogEventLevel.Fatal)
        .WriteTo.File(path: "Logs/ex_.log", outputTemplate: "{Timestamp:o} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}{Data}",
            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7, rollOnFileSizeLimit: true, fileSizeLimitBytes: 1000000))
    .WriteTo.Logger(l => l.Filter
        .ByIncludingOnly(evt => evt.Level is LogEventLevel.Information or LogEventLevel.Debug)
        .WriteTo.File(path: "Logs/cp_.log", outputTemplate: "{Timestamp:o} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}{Data]",
            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7, rollOnFileSizeLimit: true, fileSizeLimitBytes: 1000000))
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//database
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("OrderingSystemDatabase"));
//map enum
dataSourceBuilder.MapEnum<MenuType>();
dataSourceBuilder.MapEnum<PaymentType>();
dataSourceBuilder.MapEnum<EntityState>();
dataSourceBuilder.MapEnum<OrderStatus>();
dataSourceBuilder.MapEnum<MenuStatus>();
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<OrderingSystemDbContext>(options =>
{
    options.UseNpgsql(dataSource, x => x.MigrationsAssembly("OrderingSystem.Infrastructure"));
    options.UseSnakeCaseNamingConvention();
});
//enable authorization
builder.Services.AddAuthorization();

//add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    // Adding Jwt Bearer
.AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidAudience = builder.Configuration["JWT:ValidAudience"],
         ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
     };
     //refer to https://learn.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-8.0
     // We have to hook the OnMessageReceived event in order to
     // allow the JWT authentication handler to read the access
     // token from the query string when a WebSocket or 
     // Server-Sent Events request comes in.

     // Sending the access token in the query string is required when using WebSockets or ServerSentEvents
     // due to a limitation in Browser APIs. We restrict it to only calls to the
     // SignalR hub in this code.
     // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
     // for more information about security considerations when using
     // the query string to transmit the access token.
     options.Events = new JwtBearerEvents
     {
         OnMessageReceived = context =>
         {
             var accessToken = context.Request.Query["access_token"];

             // If the request is for our hub...
             var path = context.HttpContext.Request.Path;
             if (!string.IsNullOrEmpty(accessToken) &&
                 (path.StartsWithSegments("/api/hubs/order")))
             {
                 // Read the token out of the query string
                 context.Token = accessToken;
             }
             return Task.CompletedTask;
         }
     };
 });


//add identity api
builder.Services.AddIdentityApiEndpoints<IdentityUser<Guid>>(e =>
{
})
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<OrderingSystemDbContext>();


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;

    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
});

builder.Services.AddApplicationServices();

builder.Services.AddHttpContextAccessor();
//add signalR for realtime data
builder.Services.AddSignalR();

//db map images
TypeAdapterConfig<TblMenu, MenuDto>.NewConfig()
    .Map(dest => dest.Images, src => src.MenuImages.Select(o => o.Image));

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    StaticValue.envType = EnvType.Development;
}
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.MapHub<OrderHub>("api/hubs/order");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
