using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp.MainApp.Services;

var serviceProdiver = new ServiceCollection()
    .AddSingleton<IFileService>(new FileService("contacts.json"))
    .AddSingleton<IContactService, ContactService>()
    .AddTransient<MenuService>()
    .BuildServiceProvider();

MenuService menuService = serviceProdiver.GetRequiredService<MenuService>();

menuService.Show();
