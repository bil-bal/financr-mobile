using financr.Models;
using financr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace financr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        //public ExpenseModel ExpenseToEdit { get; set; }


        public EditPage(object context) // ExpenseModel exp, 
        {
            BindingContext = context;

            InitializeComponent();
        }

        //async private void back_Clicked(object sender, EventArgs e)
        //{
        //    await (BindingContext as MainTabbedPageViewModel).OnEditBackClickedAsync();
        //}

        //async private void save_Clicked(object sender, EventArgs e)
        //{
        //    await (BindingContext as MainTabbedPageViewModel).OnEditSaveClickedAsync();
        //}
    }
}