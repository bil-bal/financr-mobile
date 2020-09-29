using financr.financrAPI;
using financr.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace financr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            BindingContext = new RegisterViewModel(new PageService());

            InitializeComponent();
        }

        //async private void register_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        await (BindingContext as RegisterViewModel).OnRegisterClickedAsync();
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        private void username_Completed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((BindingContext as RegisterViewModel).Password))
            {
                password1.Focus();
            }
        }

        private void password1_Completed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((BindingContext as RegisterViewModel).PasswordConfirm))
            {
                password2.Focus();
            }
        }

        //async private void back_Clicked(object sender, EventArgs e)
        //{
        //    await (BindingContext as RegisterViewModel).OnBackClickedAsync();
        //}
    }
}