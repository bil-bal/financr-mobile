using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace financr.ViewModels
{
    public interface IPageService
    {
        Task PushModalAsync(Page page);
        Task DisplayAlert(string title, string message, string ok);
        Task PopModalAsync();
    }
}
