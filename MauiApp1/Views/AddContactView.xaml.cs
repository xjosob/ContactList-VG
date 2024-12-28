using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class AddContactView : ContentPage
{
    public AddContactView(AddContactViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
