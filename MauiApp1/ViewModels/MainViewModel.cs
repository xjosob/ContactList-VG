using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp1.ViewModels
{
#pragma warning disable CA1416 // Validate platform compatibility
    public partial class MainViewModel : ObservableObject
    {
        private readonly IContactService _contactService;
        private readonly Page _mainPage;

        public MainViewModel(IContactService contactService, Page mainPage)
        {
            _contactService = contactService;
            _mainPage = mainPage;
            UpdateContacts();
        }

        private readonly ContactModel _contact = new();

        private ObservableCollection<ContactModel> _contacts = new();
        public ObservableCollection<ContactModel> Contacts
        {
            get => _contacts;
            set => SetProperty(ref _contacts, value);
        }

        [RelayCommand]
        public async Task AddContact()
        {
            if (
                string.IsNullOrEmpty(_contact.FirstName)
                || string.IsNullOrEmpty(_contact.LastName)
                || string.IsNullOrEmpty(_contact.Email)
                || string.IsNullOrEmpty(_contact.PhoneNumber)
            )
            {
                if (_mainPage != null)
                {
                    await _mainPage.DisplayAlert(
                        "Missing information",
                        "Please fill in all fields",
                        "OK"
                    );
                }
                return;
            }
            if (!ValidationHelper.IsValidEmail(_contact.Email))
            {
                if (_mainPage != null)
                {
                    await _mainPage.DisplayAlert(
                        "Invalid email",
                        "Please enter a valid email address",
                        "OK"
                    );
                }
                return;
            }
            if (!ValidationHelper.IsValidPhoneNumber(_contact.PhoneNumber))
            {
                var _mainPage = Application.Current?.Windows[0]?.Page;
                if (_mainPage != null)
                {
                    await _mainPage.DisplayAlert(
                        "Invalid phone number",
                        "Please enter a valid phone number",
                        "OK"
                    );
                }
                return;
            }

            var newContact = ContactFactory.CreateContact(
                _contact.FirstName,
                _contact.LastName,
                _contact.Email,
                _contact.PhoneNumber
            );

            _contactService.Add(newContact);
            UpdateContacts();

            _contact.FirstName = string.Empty;
            _contact.LastName = string.Empty;
            _contact.Email = string.Empty;
            _contact.PhoneNumber = string.Empty;
        }

        public void UpdateContacts()
        {
            Contacts = new ObservableCollection<ContactModel>(_contactService.GetAll());
        }
    }
#pragma warning restore CA1416
}
