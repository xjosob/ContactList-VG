namespace MauiApp1.Interfaces
{
    public interface IAlertService
    {
        Task DisplayAlert(string title, string message, string cancel);
    }
}
