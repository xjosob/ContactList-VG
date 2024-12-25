using Business.Interfaces;
using Business.Services;
using MauiApp1.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiApp1
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
                });

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<IContactService, ContactService>();
            builder.Services.AddSingleton<IFileService, FileService>(sp => new FileService(
                "Data",
                "contacts.json"
            ));

            builder.Logging.AddDebug();

            return builder.Build();
        }
    }
}
