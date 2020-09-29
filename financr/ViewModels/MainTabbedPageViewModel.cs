using financr.financrAPI;
using financr.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace financr.ViewModels
{
    public class MainTabbedPageViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;

        // Lists for data
        public ObservableCollection<CategoryModel> CategoryList { get; set; } = new ObservableCollection<CategoryModel>();
        public ObservableCollection<ExpenseModel> ExpenseList { get; set; } = new ObservableCollection<ExpenseModel>();
        public ObservableCollection<ExpenseModel> SortedExpenseList { get; set; } = new ObservableCollection<ExpenseModel>();

        // Lists for charts
        public ObservableCollection<PieModel> PieList { get; set; } = new ObservableCollection<PieModel>();
        public ObservableCollection<LinesModel> LinesList { get; set; } = new ObservableCollection<LinesModel>();
        public ObservableCollection<ColumnsModel> ColumnList { get; set; } = new ObservableCollection<ColumnsModel>();

        private MonthlyModel _monthlyLimit = new MonthlyModel() { Id = 0, MonthlyLimit = 0 };
        public MonthlyModel MonthlyLimit
        {
            get
            {
                return _monthlyLimit;
            }
            set
            {
                SetValue(ref _monthlyLimit, value);
            }
        }

        private decimal _total;
        public decimal Total
        {
            get
            {
                return _total;
            }
            set
            {
                SetValue(ref _total, value);
            }
        }

        //Properties for Expense - AddPage
        private int _idExpense;
        public int IdExpense 
        {
            get
            {
                return _idExpense;
            }
            set
            {
                SetValue(ref _idExpense, value);
            }
        }
        private DateTime _dateExpense = DateTime.Now;
        public DateTime DateExpense 
        {
            get
            {
                return _dateExpense;
            }
            set
            {
                SetValue(ref _dateExpense, value);
            }
        }
        private string _priceExpense;
        public string PriceExpense
        {
            get
            {
                return _priceExpense;
            }
            set
            {
                SetValue(ref _priceExpense, value);
            }
        }
        private CategoryModel _categoryExpense;
        public CategoryModel CategoryExpense
        {
            get
            {
                return _categoryExpense;
            }
            set
            {
                SetValue(ref _categoryExpense, value);
            }
        }
        private string _notesExpense;
        public string NotesExpense
        {
            get
            {
                return _notesExpense;
            }
            set
            {
                SetValue(ref _notesExpense, value);
            }
        }

        //Properties for Category - AddPage
        private string _addCategoryString;
        public string AddCategoryString
        {
            get
            {
                return _addCategoryString;
            }
            set
            {
                SetValue(ref _addCategoryString, value);
            }
        }

        //Properties for Monthlylimit - AddPage
        private string _addMonthlyLimitString;
        public string AddMonthlyLimitString
        {
            get
            {
                return _addMonthlyLimitString;
            }
            set
            {
                SetValue(ref _addMonthlyLimitString, value);
            }
        }

        //Properties for Filter - ViewPage
        private bool _checkBoxFilter;
        public bool CheckBoxFilter
        {
            get
            {
                return _checkBoxFilter;
            }
            set
            {
                SetValue(ref _checkBoxFilter, value);
            }
        }
        private DateTime _dateStartFilter = DateTime.Now;
        public DateTime DateStartFilter
        {
            get
            {
                return _dateStartFilter;
            }
            set
            {
                SetValue(ref _dateStartFilter, value);
            }
        }
        private DateTime _dateEndFilter = DateTime.Now;
        public DateTime DateEndFilter
        {
            get
            {
                return _dateEndFilter;
            }
            set
            {
                SetValue(ref _dateEndFilter, value);
            }
        }
        private CategoryModel _categoryFilter;
        public CategoryModel CategoryFilter
        {
            get
            {
                return _categoryFilter;
            }
            set
            {
                SetValue(ref _categoryFilter, value);
            }
        }
        private string _notesFilter;
        public string NotesFilter
        {
            get
            {
                return _notesFilter;
            }
            set
            {
                SetValue(ref _notesFilter, value);
            }
        }

        //Properties for Graphs - GraphPage
        private bool _pieChartVisible = true;
        public bool PieChartVisible
        {
            get
            {
                return _pieChartVisible;
            }
            set
            {
                SetValue(ref _pieChartVisible, value);
            }
        }
        private bool _linesChartVisible;
        public bool LinesChartVisible
        {
            get
            {
                return _linesChartVisible;
            }
            set
            {
                SetValue(ref _linesChartVisible, value);
            }
        }
        private bool _barChartVisible;
        public bool BarChartVisible
        {
            get
            {
                return _barChartVisible;
            }
            set
            {
                SetValue(ref _barChartVisible, value);
            }
        }

        //Properties for EditPage
        private string _infoLabel;
        public string InfoLabel
        {
            get
            {
                return _infoLabel;
            }
            set
            {
                SetValue(ref _infoLabel, value);
            }
        }

        //Commands

        public ICommand OnAddExpenseCommand { get; private set; }
        public ICommand OnAddCategoryCommand { get; private set; }
        public ICommand OnAddMonthlyCommand { get; private set; }
        public ICommand OnDeleteExpenseCommand { get; private set; }
        public ICommand OnDeleteCategoryCommand { get; private set; }
        public ICommand OnFilterCommand { get; private set; }
        public ICommand OnFilterResetCommand { get; private set; }
        public ICommand OnCategoryChartCommand { get; private set; }
        public ICommand OnDailyChartCommand { get; private set; }
        public ICommand OnMonthlyChartCommand { get; private set; }
        public ICommand OnEditSaveCommand { get; private set; }
        public ICommand OnEditBackCommand { get; private set; }

        public MainTabbedPageViewModel(IPageService pageService)
        {
            _pageService = pageService;

            InitLists();

            OnAddExpenseCommand = new Command(async () => await OnAddExpenseClickedAsync());
            OnAddCategoryCommand = new Command(async () => await OnAddCategoryClickedAsync());
            OnAddMonthlyCommand = new Command(async () => await OnAddMonthlyClickedAsync());

            OnDeleteExpenseCommand = new Command<ExpenseModel>(async (exp) => await OnDeleteExpenseClickedAsync(exp));
            OnDeleteCategoryCommand = new Command<CategoryModel>(async (cat) => await OnDeleteCategoryClickedAsync(cat));

            OnFilterCommand = new Command(async () => await OnFilterClickedAsync());
            OnFilterResetCommand = new Command(async () => await OnFilterResetClickedAsync());

            OnCategoryChartCommand = new Command(OnCategoryChartClicked);
            OnMonthlyChartCommand = new Command(OnMonthlyChartClicked);
            OnDailyChartCommand = new Command(OnDailyChartClicked);

            OnEditSaveCommand = new Command(async () => await OnEditSaveClickedAsync());
            OnEditBackCommand = new Command(async () => await OnEditBackClickedAsync());
        }

        async private void InitLists()
        {
            await GetExpenseListAsync();
            await GetCategoryListAsync();
            await GetMonthlyPropAsync();
        }

        async private Task GetExpenseListAsync()
        {
            ExpenseList.Clear();
            SortedExpenseList.Clear();

            List<ExpenseModel> data = await ApiServices.GetExpensesAsync();
            List<ExpenseModel> sortedData = new List<ExpenseModel>(data);

            sortedData.Sort((x, y) => DateTime.Compare(y.Date, x.Date));

            foreach (ExpenseModel exp in data)
            {
                ExpenseList.Add(exp);
            }

            foreach (ExpenseModel exp in sortedData)
            {
                SortedExpenseList.Add(exp);
            }

            CalculateTotal();
            CreateLinesList();
            CreateColumnList();
            CreatePieList();
        }

        async private Task GetFilteredListAsync(string url)
        {
            SortedExpenseList.Clear();

            List<ExpenseModel> data = await ApiServices.GetExpensesAsync(url);

            data.Sort((x, y) => DateTime.Compare(y.Date, x.Date));

            foreach (ExpenseModel exp in data)
            {
                SortedExpenseList.Add(exp);
            }

            CalculateTotal();
            CreateLinesList();
            CreateColumnList();
            CreatePieList();
        }

        async private Task GetCategoryListAsync()
        {
            List<CategoryModel> data = await ApiServices.GetCategoriesAsync();

            foreach (CategoryModel cat in data)
            {
                CategoryList.Add(cat);
            }
        }

        async private Task GetMonthlyPropAsync()
        {
            MonthlyModel data = await ApiServices.GetMonthlyAsync();

            MonthlyLimit = data;
        }

        async private Task AddExpenseAsync(string date, string category, string price, string notes)
        {
            ExpenseModel exp = await ApiServices.PostExpensesAsync(date, category, price, notes);

            await GetExpenseListAsync();
        }

        async private Task AddCategoryAsync(string category)
        {
            CategoryModel cat = await ApiServices.PostCategoryAsync(category);

            AddCategoryString = "";
            CategoryList.Add(cat);
        }

        async private Task AddMonthlyAsync(string monthly)
        {
            MonthlyModel mon = await ApiServices.PostMonthlyAsync(monthly);

            MonthlyLimit = mon;
        }

        async private Task PatchExpenseAsync(string id, string date, string category, string price, string notes)
        {
            ExpenseModel patchedExp = await ApiServices.PatchExpensesAsync(id, date, category, price, notes);

            await GetExpenseListAsync();
        }

        async private Task DeleteExpenseAsync(ExpenseModel exp)
        {
            await ApiServices.DeleteExpenseAsync(exp.Id);

            await GetExpenseListAsync();
        }

        async private Task DeleteCategoryAsync(CategoryModel cat)
        {
            if (ExpenseList.Where(x => x.Category == cat.Category).Count() > 0)
            {
                await _pageService.DisplayAlert("Error deleting category", "There is one or more expense with this category in the database. Change categories or remove expenses in order to delete.", "Ok");
                return;
            }
            else
            {
                await ApiServices.DeleteCategoryAsync(cat.Id);

                CategoryExpense = null;
                CategoryList.Remove(cat);
            }
        }

        async private Task FilterList()
        {
            await GetFilteredListAsync(GetFilterUrl());
        }

        private void CalculateTotal()
        {
            decimal d = 0M;

            foreach (ExpenseModel exp in SortedExpenseList)
            {
                d += exp.Price;
            }

            Total = d;
        }

        private void CreateColumnList()
        {
            ColumnList.Clear();

            Dictionary<string, decimal> categoryDict = new Dictionary<string, decimal>();

            foreach (ExpenseModel exp in SortedExpenseList)
            {
                if (categoryDict.ContainsKey(exp.Date.ToString("MM-yyyy")))
                {
                    categoryDict[exp.Date.ToString("MM-yyyy")] += exp.Price;
                }
                else
                {
                    categoryDict[exp.Date.ToString("MM-yyyy")] = exp.Price;
                }
            }

            foreach (KeyValuePair<string, decimal> kvp in categoryDict)
            {
                ColumnList.Add(new ColumnsModel() { Date = kvp.Key, Value = kvp.Value });
            }
        }

        private void CreateLinesList()
        {
            LinesList.Clear();

            Dictionary<string, decimal> categoryDict = new Dictionary<string, decimal>();

            foreach (ExpenseModel exp in SortedExpenseList)
            {
                if (categoryDict.ContainsKey(exp.Date.ToString("dd-MM-yyyy")))
                {
                    categoryDict[exp.Date.ToString("dd-MM-yyyy")] += exp.Price;
                }
                else
                {
                    categoryDict[exp.Date.ToString("dd-MM-yyyy")] = exp.Price;
                }
            }

            foreach (KeyValuePair<string, decimal> kvp in categoryDict)
            {
                LinesList.Add(new LinesModel() { Date = kvp.Key, Value = kvp.Value });
            }
        }

        private void CreatePieList()
        {
            PieList.Clear();

            Dictionary<string, decimal> categoryDict = new Dictionary<string, decimal>();

            foreach (ExpenseModel exp in SortedExpenseList)
            {
                if (categoryDict.ContainsKey(exp.Category))
                {
                    categoryDict[exp.Category] += exp.Price;
                }
                else
                {
                    categoryDict[exp.Category] = exp.Price;
                }
            }

            foreach (KeyValuePair<string, decimal> kvp in categoryDict)
            {
                PieList.Add(new PieModel() { Category = kvp.Key, Value = kvp.Value });
            }
        }

        async private Task OnAddExpenseClickedAsync()
        {
            if (!string.IsNullOrEmpty(_notesExpense) && !string.IsNullOrEmpty(_priceExpense) && _categoryExpense != null)
            {
                if (decimal.Parse(_priceExpense) <= 0)
                {
                    await _pageService.DisplayAlert("Failed to add expense", "Price can't be a negativ number.", "OK");
                    return;
                }
                string dateOnly = _dateExpense.Date.ToString("yyyy-MM-dd");
                DateTime now = DateTime.Now;
                string timeOnly = now.ToString("HH:mm:ss");
                string dateString = $"{dateOnly} {timeOnly}";

                string categoryString = _categoryExpense.Category;

                string priceString = _priceExpense.Replace(",", ".");
                string notesString = _notesExpense;

                await AddExpenseAsync(dateString, categoryString, priceString, notesString);
            }
            else
            {
                await _pageService.DisplayAlert("Failed to add expense", "All fields required to be filled.", "OK");
            }
        }

        async private Task OnAddCategoryClickedAsync()
        {
            if (!string.IsNullOrEmpty(_addCategoryString))
            {
                await AddCategoryAsync(_addCategoryString);
            }
            else
            {
                await _pageService.DisplayAlert("Failed to add category", "Enter a category name in order to add a new category.", "OK");
            }
        }

        async private Task OnAddMonthlyClickedAsync()
        {
            if (!string.IsNullOrEmpty(_addMonthlyLimitString) && decimal.Parse(_addMonthlyLimitString) >= 0)
            {
                await AddMonthlyAsync(_addMonthlyLimitString);
            }
            else
            {
                await _pageService.DisplayAlert("Failed to add monthly limit", "Enter a monthly limit.", "OK");
            }
        }

        async public Task OnEditExpenseClickedAsync(ExpenseModel exp, EditPage page)
        {
            IdExpense = exp.Id;
            DateExpense = exp.Date;
            PriceExpense = exp.Price.ToString();

            CategoryModel cat = CategoryList.Where(x => x.Category == exp.Category).First();

            CategoryExpense = cat;
            NotesExpense = exp.Notes;

            await _pageService.PushModalAsync(page);
        }

        async private Task OnDeleteExpenseClickedAsync(ExpenseModel exp)
        {
            await DeleteExpenseAsync(exp);
        }

        async private Task OnDeleteCategoryClickedAsync(CategoryModel cat)
        {
            await DeleteCategoryAsync(cat);            
        }

        async public Task OnFilterClickedAsync()
        {
            await FilterList();
        }

        async private Task OnFilterResetClickedAsync()
        {

            CategoryFilter = null;
            NotesFilter = "";
            CheckBoxFilter = false;

            await FilterList();
        }

        async private Task OnEditSaveClickedAsync()
        {
            if (!string.IsNullOrEmpty(_notesExpense) && !string.IsNullOrEmpty(_priceExpense) && !string.IsNullOrEmpty(_categoryExpense.Category))
            {
                if (decimal.Parse(_priceExpense) <= 0)
                {
                    await _pageService.DisplayAlert("Failed to edit expense", "Price can't be a negativ number.", "OK");
                    return;
                }

                string id = _idExpense.ToString();

                string dateOnly = _dateExpense.Date.ToString("yyyy-MM-dd");
                DateTime now = DateTime.Now;
                string timeOnly = now.ToString("HH:mm:ss");
                string date = $"{dateOnly} {timeOnly}";

                string category = _categoryExpense.Category;

                string price = _priceExpense.Replace(",", ".");
                string notes = _notesExpense;

                InfoLabel = "saving...";

                await PatchExpenseAsync(id, date, category, price, notes);

                InfoLabel = "saving complete";

                await Task.Delay(2000);

                ResetExpense();
                InfoLabel = "";

                await _pageService.PopModalAsync();
            }
            else
            {
                await _pageService.DisplayAlert("Failed to edit expense", "All fields required to be filled.", "OK");
            }
        }
        
        async private Task OnEditBackClickedAsync()
        {
            InfoLabel = "";

            ResetExpense();

            await _pageService.PopModalAsync();
        }

        public void OnCategoryChartClicked()
        {
            PieChartVisible = true;
            LinesChartVisible = false;
            BarChartVisible = false;
        }

        public void OnMonthlyChartClicked()
        {
            PieChartVisible = false;
            LinesChartVisible = false;
            BarChartVisible = true;
        }

        public void OnDailyChartClicked()
        {
            PieChartVisible = false;
            LinesChartVisible = true;
            BarChartVisible = false;
        }

        private string GetFilterUrl()
        {
            string URL = "https://financronline.de/api/expenses/?";
            string addToURL = "";

            if (_checkBoxFilter)
            {
                if (_dateStartFilter > _dateEndFilter)
                {
                    string end = _dateStartFilter.Date.ToString("yyyy-MM-dd");
                    string start = _dateEndFilter.Date.ToString("yyyy-MM-dd");
                    addToURL = addToURL + $"&start={start}&end={end}";

                }
                else
                {
                    string start = _dateStartFilter.Date.ToString("yyyy-MM-dd");
                    string end = _dateEndFilter.Date.ToString("yyyy-MM-dd");
                    addToURL = addToURL + $"&start={start}&end={end}";
                }
            }

            if (_categoryFilter != null)
            {
                addToURL = addToURL + $"&category={_categoryFilter.Category}";
            }

            if (!string.IsNullOrEmpty(_notesFilter))
            {
                string notes = _notesFilter;
                addToURL = addToURL + $"&notes={notes}";
            }

            URL += addToURL;

            return URL;
        }

        private void ResetExpense()
        {
            IdExpense = 0;
            DateExpense = DateTime.Now;
            PriceExpense = null;
            CategoryExpense = null;
            NotesExpense = null;
        }
    }
}
