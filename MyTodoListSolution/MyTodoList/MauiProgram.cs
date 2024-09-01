using Microsoft.Extensions.Logging;
using MyTodoList.AppContextDb;
using MyTodoList.Repository.Services;

namespace MyTodoList
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddFont("Montserrat-Bold.ttf", "font01");
                    fonts.AddFont("Montserrat-Light.ttf", "font02");
                });

            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddScoped<ITarefaRepository,TarefaRepository>();

            var dbContext = new AppDbContext();
            //dbContext.Database.EnsureDeletedAsync().Wait();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
