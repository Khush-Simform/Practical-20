using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using WebApiCore.Configurations;
using WebApiCore.Controllers;
using WebApiCore.Data;
using WebApiCore.Services.CustomersServices;
using WebApiCore.Services.UnitOfWork;


var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(
        options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Demo",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();

        }
        );
    //builder.Services.AddAuthentication().AddJwtBearer();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    var connectionString = builder.Configuration.GetConnectionString("default");
    builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IUserServices, UserServices>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddDbContext<DataContext>();
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    //builder.Services.AddControllers(options =>
    //{
    //    options.Filters.Add<GlobalAuthorizationFilter>();
    //});
    var app = builder.Build();

    app.UseExceptionHandler(options =>
    {
        options.Run(
            async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var ex = context.Features.Get<IExceptionHandlerFeature>();
                if (ex != null)
                {
                    await context.Response.WriteAsync(ex.Error.Message);
                }
            }
            );
    });

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();


    app.MapControllers();
    app.AppGlobalErrorHandler();
    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}