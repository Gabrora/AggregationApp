using AggregationApp.Data;
using AggregationApp.Interfaces;
using AggregationApp.Services;
using Microsoft.EntityFrameworkCore;
using NLog.Web;


var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
try
{
    logger.Debug("starting the Application...");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IElectricityService, ElectricityService>();
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

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

    using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
            context.Database.EnsureCreated();
    }

    var scope = app.Services.CreateScope();
    var service = scope.ServiceProvider.GetService<IElectricityService>();

    if (service != null)
        service.ProcessData();


    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex, "Exception during execution");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
public partial class Program { }

