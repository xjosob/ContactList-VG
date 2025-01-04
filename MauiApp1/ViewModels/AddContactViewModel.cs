using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Interfaces;

namespace MauiApp1.ViewModels
{
    public partial class AddContactViewModel : ObservableObject
    {
        private readonly IContactService _contactService;
        private readonly IAlertService _alertService;

        public AddContactViewModel(IContactService contactService, IAlertService alertService)
        {
            _contactService = contactService;
            _alertService = alertService;
            Contact = new ContactModel();
        }

        private ContactModel _contact = new ContactModel();
        public ContactModel Contact
        {
            get => _contact;
            set => SetProperty(ref _contact, value);
        }

        [RelayCommand]
        public async Task AddContact()
        {
            if (
                string.IsNullOrEmpty(Contact.FirstName)
                || string.IsNullOrEmpty(Contact.LastName)
                || string.IsNullOrEmpty(Contact.Email)
                || string.IsNullOrEmpty(Contact.PhoneNumber)
                || string.IsNullOrEmpty(Contact.Address)
                || string.IsNullOrEmpty(Contact.City)
                || string.IsNullOrEmpty(Contact.PostalCode)
            )
            {
                await _alertService.DisplayAlert(
                    "Missing information",
                    "Please fill in all fields",
                    "OK"
                );
                return;
            }
            if (!ValidationHelper.IsValidEmail(Contact.Email))
            {
                await _alertService.DisplayAlert(
                    "Invalid email",
                    "Please enter a valid email address",
                    "OK"
                );
                return;
            }
            if (!ValidationHelper.IsValidPhoneNumber(Contact.PhoneNumber))
            {
                await _alertService.DisplayAlert(
                    "Invalid phone number",
                    "Please enter a valid phone number",
                    "OK"
                );
                return;
            }

            if (!ValidationHelper.IsValidPostalCode(Contact.PostalCode))
            {
                await _alertService.DisplayAlert(
                    "Invalid postal code",
                    "Please enter a valid postal code",
                    "OK"
                );
                return;
            }

            var newContact = ContactFactory.CreateContact(
                Contact.FirstName,
                Contact.LastName,
                Contact.Email,
                Contact.PhoneNumber,
                Contact.Address,
                Contact.City,
                Contact.PostalCode
            );

            _contactService.Add(newContact);
            Contact = new ContactModel();

            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//MainPage");

                if (Shell.Current.CurrentPage?.BindingContext is MainViewModel mainView)
                {
                    mainView.UpdateContacts();
                }
            }
        }
    }
}
