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

        private ObservableCollection<ContactModel> _contacts = [];
        public ObservableCollection<ContactModel> Contacts
        {
            get => _contacts;
            set => SetProperty(ref _contacts, value);
        }

        [RelayCommand]
        public void DeleteContact(ContactModel contact)
        {
            var currentPage = Application.Current?.Windows[0]?.Page;

            if (currentPage == null)
            {
                return;
            }

            try
            {
                _contactService.Delete(contact);
                UpdateContacts();
            }
            catch (Exception ex)
            {
                currentPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        public void UpdateContacts()
        {
            Contacts = new ObservableCollection<ContactModel>(_contactService.GetAll());
        }
    }
#pragma warning restore CA1416
}
