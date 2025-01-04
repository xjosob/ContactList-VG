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
    public partial class EditContactViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IContactService _contactService;
        private readonly IAlertService _alertService;

        public ContactModel Contact { get; private set; } = new();

        public EditContactViewModel(IContactService contactService, IAlertService alertService)
        {
            _contactService = contactService;
            _alertService = alertService;
        }

        [RelayCommand]
        public async Task EditContact()
        {
            if (Contact == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(Contact.Id))
            {
                await _alertService.DisplayAlert("Error", "Invalid Contact: Missing Id.", "OK");
                return;
            }
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

            try
            {
                _contactService.Edit(Contact);
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync("//MainPage");
                }
                if (Shell.Current?.CurrentPage?.BindingContext is MainViewModel mainViewModel)
                {
                    mainViewModel.UpdateContacts();
                }
            }
            catch (Exception ex)
            {
                await _alertService.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (
                query.TryGetValue("Contact", out var contact)
                && contact is ContactModel contactModel
            )
            {
                Contact = new ContactModel
                {
                    Id = contactModel.Id,
                    FirstName = contactModel.FirstName,
                    LastName = contactModel.LastName,
                    Email = contactModel.Email,
                    PhoneNumber = contactModel.PhoneNumber,
                    Address = contactModel.Address,
                    City = contactModel.City,
                    PostalCode = contactModel.PostalCode,
                };

                OnPropertyChanged(nameof(Contact));
            }
        }
    }
}
