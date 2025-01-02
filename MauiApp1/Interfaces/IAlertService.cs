using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Interfaces
{
    public interface IAlertService
    {
        Task DisplayAlert(string title, string message, string cancel);
    }
}
