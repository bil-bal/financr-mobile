using financr.financrAPI;
using financr.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace financr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            BindingContext = new LoginViewModel(new PageService());

            InitializeComponent();
        }

        async private void password_Completed(object sender, EventArgs e)
        {
            try
            {
                await (BindingContext as LoginViewModel).OnLoginClickedAsync();

                var newMain = new NavigationPage(new MainTabbedPage());
                //newMain.BarBackgroundColor = Color.FromHex("#20b2aa");
                GradientStopCollection gradient = new GradientStopCollection();
                gradient.Add(new GradientStop() { Color = Color.FromRgb(246, 246, 246), Offset = 0.7F });
                gradient.Add(new GradientStop() { Color = Color.FromRgb(32, 178, 170), Offset = 1.0F });

                newMain.BarBackground = new LinearGradientBrush(gradient) { StartPoint = new Point(0.0, 0.5), EndPoint = new Point(1.0, 0.5) };

                Application.Current.MainPage = newMain;
            }
            catch (Exception)
            {

            }
        }

        private void username_Completed(object sender, EventArgs e)
        {
            password.Focus();
        }

        //async private void register_Clicked(object sender, EventArgs e)
        //{
        //    await (BindingContext as LoginViewModel).OnRegisterClickedAsync();
        //}
    }
}