using MauiApp1.Views;

namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(AddContactView), typeof(AddContactView));
            Routing.RegisterRoute(nameof(EditContactView), typeof(EditContactView));
        }
    }
}
