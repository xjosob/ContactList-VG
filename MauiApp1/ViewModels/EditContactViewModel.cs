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
    public partial class EditContactViewModel(IContactService contactService) : ObservableObject
    {
        private readonly IContactService _contactService = contactService;

        public ContactModel Contact { get; private set; } = new();

        [RelayCommand]
        public async Task EditContact(ContactModel contact)
        {
            var currentPage = Application.Current?.Windows[0]?.Page;
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
                if (currentPage == null)
                {
                    return;
                }
                await currentPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
