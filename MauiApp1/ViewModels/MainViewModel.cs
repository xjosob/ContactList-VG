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

        public MainViewModel(IContactService contactService)
        {
            _contactService = contactService;
            UpdateContacts();
        }

        public ContactModel Contact { get; private set; } = new();

        private ObservableCollection<ContactModel> _contacts = new();
        public ObservableCollection<ContactModel> Contacts
        {
            get => _contacts;
            set => SetProperty(ref _contacts, value);
        }

        [RelayCommand]
        public async Task AddContact()
        {
            var currentPage = Application.Current?.Windows[0]?.Page;

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
            UpdateContacts();

            Contact = new ContactModel();
        }

        public void UpdateContacts()
        {
            Contacts = new ObservableCollection<ContactModel>(_contactService.GetAll());
        }
    }
#pragma warning restore CA1416
}
