using financr.financrAPI;
using financr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace financr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPageViewModel ViewModel
        {
            get
            {
                return BindingContext as MainTabbedPageViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

        public MainTabbedPage()
        {
            InitializeComponent();

            ViewModel = new MainTabbedPageViewModel(new PageService());


            ContentPage addPage = new AddPage();
            addPage.Title = "add";
            ContentPage viewPage = new ViewPage();
            viewPage.Title = "view";
            ContentPage graphsPage = new GraphsPage();
            graphsPage.Title = "graphs";

            Children.Add(addPage);
            Children.Add(viewPage);
            Children.Add(graphsPage);
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();
            ApiServices.client.DefaultRequestHeaders.Remove("Authorization");

            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}