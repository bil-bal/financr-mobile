using financr.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace financr.financrAPI
{
    class ApiServices
    {
        public static HttpClient client = new HttpClient();


        const string ExpensesURL = "https://financronline.de/api/expenses/";
        const string CategoriesURL = "https://financronline.de/api/category/";
        const string MonthlyURL = "https://financronline.de/api/monthly/";


        async public static Task<List<ExpenseModel>> GetExpensesAsync(string url = ExpensesURL)
        {
            var response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            List<Dictionary<string, string>> expDict = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);            

            return ConvertDictToExpenseModelList(expDict);
        }

        private static List<ExpenseModel> ConvertDictToExpenseModelList(List<Dictionary<string, string>> inputDict)
        {
            List<ExpenseModel> returnList = new List<ExpenseModel>();

            foreach (var d in inputDict)
            {
                returnList.Add(new ExpenseModel()
                {
                    Id = int.Parse(d["id"]),
                    Category = d["category"],
                    Date = DateTime.Parse(d["date"]),
                    Price = decimal.Parse(d["price_b"], CultureInfo.InvariantCulture),
                    Notes = d["notes"]
                });
            }

            return returnList;
        }

        async public static Task<List<CategoryModel>> GetCategoriesAsync(string url = CategoriesURL)
        {
            var response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            List<Dictionary<string, string>> categories = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);

            return ConvertDictToCategoryModelList(categories);
        }

        private static List<CategoryModel> ConvertDictToCategoryModelList(List<Dictionary<string, string>> inputDict)
        {
            List<CategoryModel> returnList = new List<CategoryModel>();

            foreach (Dictionary<string, string> d in inputDict)
            {
                returnList.Add(new CategoryModel()
                {
                    Id = int.Parse(d["id"]),
                    Category = d["cat"]
                }) ;
            }

            return returnList;
        }

        async public static Task<MonthlyModel> GetMonthlyAsync(string url = MonthlyURL)
        {
            var response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            List<Dictionary<string, string>> categories = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);

            return ConvertDictToMonthlyModelList(categories);
        }

        private static MonthlyModel ConvertDictToMonthlyModelList(List<Dictionary<string, string>> inputDict)
        {
            MonthlyModel mon = new MonthlyModel();

            foreach (Dictionary<string, string> d in inputDict)
            {
                mon.Id = int.Parse(d["id"]);
                mon.MonthlyLimit = decimal.Parse(d["monthly"], CultureInfo.InvariantCulture);
            }

            return mon;
        }

        async public static Task<ExpenseModel> PostExpensesAsync(string date, string category, string price, string notes = "-", string url = ExpensesURL)
        {
            string json = JsonConvert.SerializeObject(
                    new Dictionary<string, string>() {
                    { "category", category },
                    { "price_b", price },
                    { "date", date },
                    { "notes", notes }
                    });

            var response = await ApiServices.client.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json"));

            json = await response.Content.ReadAsStringAsync();

            Dictionary<string, string> expDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            ExpenseModel exp = new ExpenseModel()
            {
                Category = expDict["category"],
                Id = int.Parse(expDict["id"]),
                Price = decimal.Parse(expDict["price_b"], CultureInfo.InvariantCulture),
                Date = DateTime.Parse(expDict["date"]),
                Notes = expDict["notes"]
            };

            return exp;
        }

        async public static Task<ExpenseModel> PatchExpensesAsync(string id, string date, string category, string price, string notes = "-", string url = ExpensesURL)
        {
            string json = JsonConvert.SerializeObject(
                    new Dictionary<string, string>() {
                    { "id", id },
                    { "category", category },
                    { "price_b", price },
                    { "date", date },
                    { "notes", notes }
                    });

            string patchUrl = $"{url}{id}/";

            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, patchUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var response = await ApiServices.client.SendAsync(request);

            json = await response.Content.ReadAsStringAsync();

            Dictionary<string, string> expDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            ExpenseModel exp = new ExpenseModel()
            {
                Category = expDict["category"],
                Id = int.Parse(expDict["id"]),
                Price = decimal.Parse(expDict["price_b"], CultureInfo.InvariantCulture),
                Date = DateTime.Parse(expDict["date"]),
                Notes = expDict["notes"]
            };

            return exp;
        }

        async public static Task<CategoryModel> PostCategoryAsync(string category, string url = CategoriesURL)
        {
            string json = JsonConvert.SerializeObject(
                    new Dictionary<string, string>() {
                    { "cat", category }
                    });

            var response = await ApiServices.client.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json"));

            json = await response.Content.ReadAsStringAsync();

            Dictionary<string, string> catDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            CategoryModel cat = new CategoryModel()
            {
                Id = int.Parse(catDict["id"]),
                Category = catDict["cat"]
            };

            return cat;
        }

        async public static Task<MonthlyModel> PostMonthlyAsync(string monthly, string url = MonthlyURL)
        {
            string json = JsonConvert.SerializeObject(
                    new Dictionary<string, string>() {
                    { "monthly", monthly }
                    });

            var response = await ApiServices.client.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json"));

            json = await response.Content.ReadAsStringAsync();

            Dictionary<string, string> monthlyDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            MonthlyModel mon = new MonthlyModel()
            {
                Id = int.Parse(monthlyDict["id"]),
                MonthlyLimit = decimal.Parse(monthlyDict["monthly"], CultureInfo.InvariantCulture)
            };

            return mon;
        }

        async public static Task DeleteExpenseAsync(int id, string url = ExpensesURL)
        {
            string deleteUrl = $"{url}{id}/";
            var response = await ApiServices.client.DeleteAsync(deleteUrl);
        }

        async public static Task DeleteCategoryAsync(int id, string url = CategoriesURL)
        {
            string deleteUrl = $"{url}{id}/";
            var response = await ApiServices.client.DeleteAsync(deleteUrl);
        }

        async public static Task<string> GetToken(string username, string password)
        {
            string json = JsonConvert.SerializeObject(new Dictionary<string, string>() { { "username", username.Trim().ToLower() }, { "password", password } });
            var response = await ApiServices.client.PostAsync(
                "https://financronline.de/api-token-auth/",
                new StringContent(json, Encoding.UTF8, "application/json"));
            string txt = await response.Content.ReadAsStringAsync();
            try
            {
                Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(txt);
                return dict["token"];
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
