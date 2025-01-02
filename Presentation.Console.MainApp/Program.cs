using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp.Interfaces;
using Presentation.ConsoleApp.MainApp.Services;

var serviceProdiver = new ServiceCollection()
    .AddSingleton<IFileService>(new FileService("contacts.json"))
    .AddSingleton<IContactService, ContactService>()
    .AddTransient<MenuService>()
    .BuildServiceProvider();

IMenuService menuService = serviceProdiver.GetRequiredService<MenuService>();

menuService.Show();
