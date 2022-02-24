using DTN.Web.Middlewares;
using Serilog;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using DTN.Services.Interface;
using DTN.Services;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DTN.Services.Configuration;

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
    builder.Services.AddSingleton<IFileValidator>(sp => new FileValidator(sp.GetService<IConfiguration>()));
    builder.Services.AddTransient(typeof(ICsvSerializer<>), typeof(CsvSerializer<>));
    builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
    builder.Services.AddSingleton<IUserService, UserService>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDefaultAWSOptions(configuration.GetAWSOptions());
    builder.Services.Configure<DynamoDbConfiguration>(options => configuration.GetSection("DynamoDb").Bind(options));
    builder.Services.AddTransient<IOnboardingRepository, OnboardingRepository>();
    builder.Services.AddTransient<IOnboardingService, OnboardingService>();
    builder.Services.AddTransient<IEmailService, EmailService>();
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

