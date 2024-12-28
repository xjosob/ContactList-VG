using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class EditContactView : ContentPage
{
    public EditContactView(EditContactViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
