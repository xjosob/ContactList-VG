using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp1.ViewModels
{
    public partial class AddContactViewModel(IContactService contactService) : ObservableObject
    {
        private readonly IContactService _contactService = contactService;

        public ContactModel Contact { get; private set; } = new();

        [RelayCommand]
        public async Task AddContact()
        {
            var currentPage = Shell.Current?.CurrentPage;

            if (currentPage == null)
            {
                return;
            }
            if (
                string.IsNullOrEmpty(Contact.FirstName)
                || string.IsNullOrEmpty(Contact.LastName)
                || string.IsNullOrEmpty(Contact.Email)
                || string.IsNullOrEmpty(Contact.PhoneNumber)
            )
            {
                await currentPage.DisplayAlert(
                    "Missing information",
                    "Please fill in all fields",
                    "OK"
                );
                return;
            }
            if (!ValidationHelper.IsValidEmail(Contact.Email))
            {
                await currentPage.DisplayAlert(
                    "Invalid email",
                    "Please enter a valid email address",
                    "OK"
                );
                return;
            }
            if (!ValidationHelper.IsValidPhoneNumber(Contact.PhoneNumber))
            {
                await currentPage.DisplayAlert(
                    "Invalid phone number",
                    "Please enter a valid phone number",
                    "OK"
                );
                return;
            }

            var newContact = ContactFactory.CreateContact(
                Contact.FirstName,
                Contact.LastName,
                Contact.Email,
                Contact.PhoneNumber
            );

            _contactService.Add(newContact);

            Contact = new ContactModel();

            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
        }
    }
}
