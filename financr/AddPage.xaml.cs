using financr.financrAPI;
using financr.Models;
using financr.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    public partial class AddPage : ContentPage
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

        public AddPage()
        {
            InitializeComponent();
        }

        //async private void addExpense_Clicked(object sender, EventArgs e)
        //{
        //    await (BindingContext as MainTabbedPageViewModel).OnAddExpenseClickedAsync();
        //}

        //async private void addCategory_Clicked(object sender, EventArgs e)
        //{
        //    await (BindingContext as MainTabbedPageViewModel).OnAddCategoryClickedAsync();
        //}

        //async private void monthlyLimitButton_Clicked(object sender, EventArgs e)
        //{
        //    await (BindingContext as MainTabbedPageViewModel).OnAddMonthlyClickedAsync();
        //}

        //async private void deleteExpense_Clicked(object sender, EventArgs e)
        //{
        //    var mi = (MenuItem)sender;
        //    ExpenseModel exp = (ExpenseModel)mi.CommandParameter;

        //    await (BindingContext as MainTabbedPageViewModel).OnDeleteExpenseClickedAsync(exp);
        //}

        //async private void deleteCategory_Clicked(object sender, EventArgs e)
        //{
        //    var mi = (MenuItem)sender;
        //    CategoryModel cat = (CategoryModel)mi.CommandParameter;

        //    await (BindingContext as MainTabbedPageViewModel).OnDeleteCategoryClickedAsync(cat);
        //}

        async private void editExpense_Clicked(object sender, EventArgs e)
        {
            //TODO - cleanup
            var mi = (MenuItem)sender;
            ExpenseModel exp = (ExpenseModel)mi.CommandParameter;

            var editPage = new EditPage(this.BindingContext);

            await ViewModel.OnEditExpenseClickedAsync(exp, editPage);
        }
    }
}