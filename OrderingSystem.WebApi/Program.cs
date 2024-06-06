using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using OrderingSystem.Application;
using OrderingSystem.Application.Utils;
using OrderingSystem.Infrastructure.Databases.OrderingSystem;
using OrderingSystem.WebApi.Middlewares;
using Serilog;
using Serilog.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test01", Version = "v1" });

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
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<OrderingSystemDbContext>(options =>
{
    options.UseNpgsql(dataSource, x => x.MigrationsAssembly("OrderingSystem.Infrastructure"));
    options.UseSnakeCaseNamingConvention();
});
//enable authorization
builder.Services.AddAuthorization();

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

//map identity route
app.MapGroup("/api/usermanager").MapIdentityApi<IdentityUser<Guid>>();

app.MapControllers();

app.UseAuthorization();


app.Run();
