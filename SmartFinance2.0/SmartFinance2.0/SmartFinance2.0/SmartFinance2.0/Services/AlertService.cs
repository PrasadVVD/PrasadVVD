using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartFinance.Services
{
    public interface IAlertService
    {
        void DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
    }

    public class AlertService : IAlertService
    {
        public void DisplayAlert(string title, string message, string cancel)
        {
            Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
