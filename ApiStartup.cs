using Emgu.CV;
using System.Runtime.InteropServices;
using AgrobotV2.Camera;


namespace AgrobotV2;
public static class ApiStartup
{
    private static WebApplicationBuilder? builder;

    public static void Begin(string[] args)
    {
        builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddLogging();

        var _ = builder.Services.BuildServiceProvider();
        var logger = _.GetRequiredService<ILogger<CameraService>>();
        var loggerFactory = _.GetRequiredService<ILoggerFactory>();
        var loggerLocal = loggerFactory.CreateLogger("General");


        if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            
            loggerLocal.LogInformation($"detected system is Windows. Setting parameters for DShow...");
            builder.Services.AddTransient<CameraService>(_ =>
                new CameraService(0, VideoCapture.API.DShow,  1280, 720, 50, logger));
        }
        else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            loggerLocal.LogInformation($"detected system is Linux.Setting parameters for V4L2...");
            builder.Services.AddTransient<CameraService>(_ =>
                new CameraService(0, VideoCapture.API.V4L2,  1280, 720, 50, logger));
        }    

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRouting();

        app.MapControllers();


        app.Run();
    }

}
