﻿using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        Shell.SetNavBarIsVisible(this, false);
    }
}
