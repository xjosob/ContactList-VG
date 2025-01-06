using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.MainApp.Services
{
    public class MenuService(IContactService contactService) : IMenuService
    {
        private readonly IContactService _contactService = contactService;

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
            while (true)
            {
                try
                {
                    Console.Clear();

                    int currentStep = 1;
                    string firstName = string.Empty;
                    string lastName = string.Empty;
                    string email = string.Empty;
                    string phoneNumber = string.Empty;
                    string address = string.Empty;
                    string city = string.Empty;
                    string postalCode = string.Empty;

                    while (true)
                    {
                        Console.Clear();
                        switch (currentStep)
                        {
                            case 1:

                                Console.WriteLine("Enter first name: ");
                                firstName = Console.ReadLine() ?? string.Empty;
                                if (string.IsNullOrEmpty(firstName))
                                {
                                    MessageLog(
                                        "First name is required. Press 'Enter' to retry or 'Backspace' to exit."
                                    );
                                    if (!WaitForValidKeyPress())
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    currentStep++;
                                }
                                break;
                            case 2:

                                Console.WriteLine("Enter last name: ");
                                lastName = Console.ReadLine() ?? string.Empty;
                                if (string.IsNullOrEmpty(lastName))
                                {
                                    MessageLog(
                                        "Last name is required. Press 'Enter' to retry or 'Backspace' to exit."
                                    );
                                    if (!WaitForValidKeyPress())
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    currentStep++;
                                }
                                break;
                            case 3:

                                Console.WriteLine("Enter email: ");
                                email = Console.ReadLine() ?? string.Empty;
                                if (string.IsNullOrEmpty(email))
                                {
                                    MessageLog(
                                        "Email is required. Press 'Enter' to retry or 'Backspace' to exit."
                                    );
                                    if (!WaitForValidKeyPress())
                                    {
                                        return;
                                    }
                                }
                                else if (!ValidationHelper.IsValidEmail(email))
                                {
                                    MessageLog(
                                        "Invalid email input. Press 'Enter' to retry or 'Backspace' to exit."
                                    );
                                    if (!WaitForValidKeyPress())
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    currentStep++;
                                }
                                break;
                            case 4:

                                Console.WriteLine("Enter phone number: ");
                                phoneNumber = Console.ReadLine() ?? string.Empty;
                                if (string.IsNullOrEmpty(phoneNumber))
                                {
                                    MessageLog(
                                        "Phone number is required. Press 'Enter' to retry or 'Backspace' to exit."
                                    );
                                    if (!WaitForValidKeyPress())
                                    {
                                        return;
                                    }
                                }
                                else if (!ValidationHelper.IsValidPhoneNumber(phoneNumber))
                                {
                                    MessageLog(
                                        "Invalid phone number input. Press 'Enter' to retry or 'Backspace' to exit."
                                    );
                                    if (!WaitForValidKeyPress())
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    currentStep++;
                                }
                                break;
                            case 5:

                                Console.WriteLine("Enter address: ");
                                address = Console.ReadLine() ?? string.Empty;
                                if (string.IsNullOrEmpty(address))
                                {
                                    MessageLog(
                                        "Address is required. Press 'Enter' to retry or 'Backspace' to exit."
                                    );
                                    if (!WaitForValidKeyPress())
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    currentStep++;
                                }
                                break;
                            case 6:

                                Console.WriteLine("Enter city: ");
                                city = Console.ReadLine() ?? string.Empty;
                                if (string.IsNullOrEmpty(city))
                                {
                                    MessageLog(
                                        "City is required. Press 'Enter' to retry or 'Backspace' to exit."
                                    );
                                    if (!WaitForValidKeyPress())
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    currentStep++;
                                }
                                break;
                            case 7:

                                Console.WriteLine("Enter postal code: ");
                                postalCode = Console.ReadLine() ?? string.Empty;
                                if (string.IsNullOrEmpty(postalCode))
                                {
                                    MessageLog(
                                        "Postal code is required. Press 'Enter' to retry or 'Backspace' to exit."
                                    );
                                    if (!WaitForValidKeyPress())
                                    {
                                        return;
                                    }
                                }
                                else if (!ValidationHelper.IsValidPostalCode(postalCode))
                                {
                                    MessageLog(
                                        "Invalid postal code format. Press 'Enter' to retry or 'Backspace' to exit."
                                    );
                                    if (!WaitForValidKeyPress())
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    currentStep++;
                                }
                                break;
                            case 8:

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
                                MessageLog(
                                    $"{contact.FirstName} {contact.LastName} added to contact list!"
                                );
                                Console.ReadKey();
                                return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageLog(ex.Message);
                }
            }
        }

        private static bool WaitForValidKeyPress()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Backspace)
                {
                    return false;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    return true;
                }
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
                }
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                MessageLog("No contacts available.");
                Console.ReadKey();
            }
        }

        public static void MessageLog(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
        }
    }
}
