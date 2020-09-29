using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace financr.ViewModels
{
    public class PageService : IPageService
    {
        async public Task DisplayAlert(string title, string message, string ok)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, ok);
        }

        async public Task PopModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        async public Task PushModalAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }
    }
}
