using financr.financrAPI;
using financr.Models;
using financr.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace financr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPage : ContentPage
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

        public ViewPage()
        {
            InitializeComponent();
        }

        async private void editExpense_Clicked(object sender, EventArgs e)
        {
            //TODO - cleanup
            var mi = (MenuItem)sender;
            ExpenseModel exp = (ExpenseModel)mi.CommandParameter;

            var editPage = new EditPage(this.BindingContext);

            await ViewModel.OnEditExpenseClickedAsync(exp, editPage);
        }

        //async private void deleteExpense_Clicked(object sender, EventArgs e)
        //{
        //    var mi = (MenuItem)sender;
        //    ExpenseModel exp = (ExpenseModel)mi.CommandParameter;

        //    await (BindingContext as MainTabbedPageViewModel).OnDeleteExpenseClickedAsync(exp);
        //}

        async private void ListView_Refreshing(object sender, EventArgs e)
        {
            await ViewModel.OnFilterClickedAsync();

            viewListView.EndRefresh();
        }

        //async private void filterButton_Clicked(object sender, EventArgs e)
        //{
        //    await (BindingContext as MainTabbedPageViewModel).OnFilterClickedAsync();
        //}

        //async private void resetFilterButton_Clicked(object sender, EventArgs e)
        //{
        //    await (BindingContext as MainTabbedPageViewModel).OnFilterResetClickedAsync();
        //}
    }
}