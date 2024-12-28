using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp1.ViewModels
{
    public partial class EditContactViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IContactService _contactService;

        public ContactModel Contact { get; private set; } = new();

        public EditContactViewModel(IContactService contactService)
        {
            _contactService = contactService;
        }

        [RelayCommand]
        public async Task EditContact(ContactModel contact)
        {
            if (contact == null)
            {
                return;
            }

            try
            {
                _contactService.Edit(contact);
            }
            catch (Exception ex)
            {
                var currentPage = Shell.Current?.CurrentPage;
                if (currentPage == null)
                {
                    return;
                }
                await currentPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }

            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (
                query.TryGetValue("Contact", out var contact)
                && contact is ContactModel contactModel
            )
            {
                Contact = contactModel;
            }
        }
    }
}
