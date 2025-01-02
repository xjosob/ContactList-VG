using Business.Interfaces;
using Business.Services;
using MauiApp1.Interfaces;
using MauiApp1.Services;
using MauiApp1.ViewModels;
using MauiApp1.Views;
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

            builder.Services.AddSingleton<AddContactViewModel>();
            builder.Services.AddSingleton<AddContactView>();

            builder.Services.AddSingleton<EditContactViewModel>();
            builder.Services.AddSingleton<EditContactView>();

            builder.Services.AddSingleton<IContactService, ContactService>();
            builder.Services.AddSingleton<IFileService>(sp => new FileService());
            builder.Services.AddSingleton<IAlertService, AlertService>();

            builder.Logging.AddDebug();

            return builder.Build();
        }
    }
}
