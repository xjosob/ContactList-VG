﻿using MauiApp1.Interfaces;

namespace MauiApp1.Services
{
    public class AlertService : IAlertService
    {
        public async Task DisplayAlert(string title, string message, string cancel)
        {
            if (Shell.Current?.CurrentPage != null)
            {
                await Shell.Current.CurrentPage.DisplayAlert(title, message, cancel);
            }
        }
    }
}
