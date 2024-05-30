using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using OrderingSystem.Infrastructure.Databases.OrderingSystem;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<OrderingSystemDbContext>(options =>
{
    options.UseNpgsql(dataSource);
    options.UseSnakeCaseNamingConvention();
});

//enable authorization
builder.Services.AddAuthorization();
//add identity api
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<OrderingSystemDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//map identity route
app.MapIdentityApi<IdentityUser>();

app.Run();
