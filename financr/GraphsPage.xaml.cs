using financr.financrAPI;
using financr.Models;
using financr.ViewModels;
using Newtonsoft.Json;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace financr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraphsPage : ContentPage
    {
        public GraphsPage()
        {
            InitializeComponent();

            PieChart.Legend = new ChartLegend();

            NumericalAxis secondaryAxis = new NumericalAxis()
            {
                RangePadding = NumericalPadding.Additional
            };

            LineChart.PrimaryAxis = new CategoryAxis() { IsInversed = true };
            LineChart.SecondaryAxis = secondaryAxis;

            ColumnChart.PrimaryAxis = new CategoryAxis() { IsInversed = true };
            ColumnChart.SecondaryAxis = secondaryAxis;
        }

        //private void pieChart_Clicked(object sender, EventArgs e)
        //{
        //    (BindingContext as MainTabbedPageViewModel).OnCategoryChartClicked();
        //}

        //private void linesChart_Clicked(object sender, EventArgs e)
        //{
        //    (BindingContext as MainTabbedPageViewModel).OnDailyChartClicked();
        //}

        //private void barChart_Clicked(object sender, EventArgs e)
        //{
        //    (BindingContext as MainTabbedPageViewModel).OnMonthlyChartClicked();
        //}
    }
}