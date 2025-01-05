using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.MainApp.Services
{
    public class MenuService : IMenuService
    {
        private readonly IContactService _contactService;

        public MenuService(IContactService contactService)
        {
            _contactService = contactService;
        }

        public void Show()
        {
            while (true)
            {
                MainMenu();
            }
        }

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"{"1.", -5} Add contact");
            Console.WriteLine($"{"2.", -5} View contacts");
            Console.WriteLine($"{"Q.", -5} Quit application");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Q:
                    ExitOption();
                    break;
                case ConsoleKey.D1:
                    CreateOption();
                    break;
                case ConsoleKey.D2:
                    ViewOption();
                    break;
                default:
                    break;
            }
        }

        public static void ExitOption()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Do you want to exit the application? (y/n): ");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Y)
                {
                    Environment.Exit(0);
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    break;
                }
            }
        }

        public void CreateOption()
        {
            try
            {
                Console.Clear();

                Console.Write("First Name: ");
                string? firstName = Console.ReadLine();
                if (string.IsNullOrEmpty(firstName))
                {
                    MessageLog("First name cannot be empty. Please try again.");
                    return;
                }

                Console.Write("Last Name: ");
                string? lastName = Console.ReadLine();
                if (string.IsNullOrEmpty(lastName))
                {
                    MessageLog("Last name cannot be empty. Please try again.");
                    return;
                }

                Console.Write("Email: ");
                string? email = Console.ReadLine();
                if (string.IsNullOrEmpty(email) || !ValidationHelper.IsValidEmail(email))
                {
                    MessageLog("Invalid email format. Please try again.");
                    return;
                }

                Console.Write("Phone number: ");
                string? phoneNumber = Console.ReadLine();

                if (
                    string.IsNullOrEmpty(phoneNumber)
                    || !ValidationHelper.IsValidPhoneNumber(phoneNumber)
                )
                {
                    MessageLog("Invalid phone number format. Please try again.");
                    return;
                }

                Console.Write("Address: ");
                string? address = Console.ReadLine();
                if (string.IsNullOrEmpty(address))
                {
                    MessageLog("Address cannot be empty. Please try again.");
                    return;
                }

                Console.Write("City: ");
                string? city = Console.ReadLine();
                if (string.IsNullOrEmpty(city))
                {
                    MessageLog("City cannot be empty. Please try again.");
                    return;
                }

                Console.Write("Postal code: ");
                string? postalCode = Console.ReadLine();
                if (string.IsNullOrEmpty(postalCode))
                {
                    MessageLog("Postal code cannot be empty. Please try again.");
                    return;
                }

                ContactModel contact = ContactFactory.CreateContact(
                    firstName,
                    lastName,
                    email,
                    phoneNumber,
                    address,
                    city,
                    postalCode
                );
                _contactService.Add(contact);
                MessageLog($"{contact.FirstName} {contact.LastName} added to contact list!");
            }
            catch (Exception ex)
            {
                MessageLog(ex.Message);
            }
        }

        public void ViewOption()
        {
            Console.Clear();
            var contacts = _contactService.GetAll();

            if (contacts.Any())
            {
                foreach (var contact in contacts)
                {
                    Console.WriteLine(
                        $"{contact.FirstName} {contact.LastName}, email: {contact.Email}, phone number: {contact.PhoneNumber}, Address: {contact.Address}, City: {contact.City}, Postal Code: {contact.PostalCode}"
                    );
                    Console.ReadKey();
                }
            }
            else
            {
                MessageLog("No contacts available.");
            }
        }

        public static void MessageLog(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
