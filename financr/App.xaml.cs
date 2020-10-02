using financr.financrAPI;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace financr
{
    public partial class App : Application
    {
        private bool loggedIn = false;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("");
            
            InitializeComponent();

            CheckIfLoggedIn();

            if (!loggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var newMain = new NavigationPage(new MainTabbedPage());
                //newMain.BarBackgroundColor = Color.FromHex("#20b2aa");
                GradientStopCollection gradient = new GradientStopCollection();
                gradient.Add(new GradientStop() { Color = Color.FromRgb(246, 246, 246), Offset = 0.7F });
                gradient.Add(new GradientStop() { Color = Color.FromRgb(32, 178, 170), Offset = 1.0F });

                newMain.BarBackground = new LinearGradientBrush(gradient) { StartPoint = new Point(0.0, 0.5), EndPoint = new Point(1.0, 0.5) };
                
                MainPage = newMain;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        async private void CheckIfLoggedIn()
        {
            if (await SecureStorage.GetAsync("token") == null)
            {
                loggedIn = false;
            }
            else
            {
                if (!ApiServices.client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    ApiServices.client.DefaultRequestHeaders.Add("Authorization", $"Token {await SecureStorage.GetAsync("token")}");
                }

                loggedIn = true;
            }
        }
    }
}
