
using Serilog;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DTN.Logic.Helpers.Interfaces;
using DTN.Logic.Services.Interfaces;
using DTN.Logic.Repositories.Interfaces;
using DTN.Logic.Configurations;
using DTN.Logic.Helpers;
using DTN.Logic.Services;
using DTN.Logic.Repositories;
using DTN.Web.Middlewares;
using Microsoft.Extensions.Options;

try
{

    var builder = WebApplication.CreateBuilder(args);

    ConfigurationManager configuration = builder.Configuration;
    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

    Log.Information("Starting web host");
   
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddControllers();  
 
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDefaultAWSOptions(configuration.GetAWSOptions());
    builder.Services.Configure<DynamoDbConfiguration>(options => configuration.GetSection("DynamoDb").Bind(options));
    builder.Services.AddTransient<IVehicleRepository, VehicleRepository>();    
    builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
    builder.Services.AddSingleton<IFileValidator>(sp => new FileValidator(sp.GetService<IConfiguration>()));
    builder.Services.AddTransient(typeof(ICsvSerializer<>), typeof(CsvSerializer<>));
    builder.Services.AddSingleton<IUserService, UserService>();
    builder.Services.AddTransient<IVehicleService, VehicleService>();
    builder.Services.AddTransient<IFileService, FileService>();

    //DynamoDbConfiguration
    builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
    builder.Services.AddSingleton(provider => new DynamoDBContextConfig
    {
        Conversion = DynamoDBEntryConversion.V2,
        ConsistentRead = true,
        SkipVersionCheck = false,
        TableNamePrefix = provider.GetRequiredService<IOptions<DynamoDbConfiguration>>().Value.TableNamePrefix,
        IgnoreNullValues = false,
    });
    builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();


    builder.Services.AddSpaStaticFiles(configuration =>
    {
        configuration.RootPath = "ClientApp/build";
    });

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    app.UseMiddleware<CustomExceptionHandlingMiddleware>();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();       
    }

    if (app.Environment.IsDevelopment())
    { 
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSpaStaticFiles();
    app.UseRouting();


    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");
    });

    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "ClientApp";

        if (app.Environment.IsDevelopment())
        {
            spa.UseReactDevelopmentServer(npmScript: "start");
        }
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

