using System.Collections.ObjectModel;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Interfaces;

namespace MauiApp1.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IContactService _contactService;
        private readonly IAlertService _alertService;

        public MainViewModel(IContactService contactService, IAlertService alertService)
        {
            _contactService = contactService;
            _alertService = alertService;
            UpdateContacts();
        }

        private ObservableCollection<ContactModel> _contacts = [];
        public ObservableCollection<ContactModel> Contacts
        {
            get => _contacts;
            set => SetProperty(ref _contacts, value);
        }

        [RelayCommand]
        public async Task NavigateToAddContact()
        {
            await Shell.Current.GoToAsync("AddContactView");
        }

        [RelayCommand]
        public async Task NavigateToEditContact(ContactModel contact)
        {
            var navigationParameter = new ShellNavigationQueryParameters { { "Contact", contact } };
            await Shell.Current.GoToAsync("EditContactView", navigationParameter);
        }

        [RelayCommand]
        public async Task DeleteContact(ContactModel contact)
        {
            if (contact == null)
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
                await _alertService.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        public void UpdateContacts()
        {
            Contacts.Clear();
            foreach (var contact in _contactService.GetAll())
            {
                Contacts.Add(contact);
            }
        }
    }
}
