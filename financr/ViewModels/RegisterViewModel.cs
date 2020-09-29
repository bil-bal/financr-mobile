using financr.financrAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace financr.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                SetValue(ref _username, value);
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetValue(ref _password, value);
            }
        }

        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get
            {
                return _passwordConfirm;
            }
            set
            {
                SetValue(ref _passwordConfirm, value);
            }
        }

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

        public ICommand OnBackCommand { get; private set; }
        public ICommand OnRegisterCommand { get; private set; }

        public RegisterViewModel(IPageService pageService)
        {
            _pageService = pageService;

            OnBackCommand = new Command(async () => await OnBackClickedAsync());
            OnRegisterCommand = new Command(async () => await OnRegisterClickedAsync());
        }

        async public Task OnRegisterClickedAsync()
        {
            try
            {
                if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(PasswordConfirm))
                {
                    InfoLabel = "enter username and password.";
                    throw new Exception();
                }
                else if (Password != PasswordConfirm)
                {
                    InfoLabel = "passwords do not match.";
                    throw new Exception();
                }
                else
                {

                    InfoLabel = "registering user...";
                    // TODO - put in API class
                    string json = JsonConvert.SerializeObject(new Dictionary<string, string>() { { "username", Username.Trim().ToLower() }, { "password", PasswordConfirm } });

                    var response = await ApiServices.client.PostAsync(
                        "https://financronline.de/api/create-user/",
                        new StringContent(json, Encoding.UTF8, "application/json"));
                    string txt = await response.Content.ReadAsStringAsync();
                    try
                    {
                        Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(txt);
                        InfoLabel = "user registered";

                        await SecureStorage.SetAsync("token", dict["token"]);

                        await Task.Delay(2000);

                        await _pageService.PopModalAsync();
                    }
                    catch (Exception)
                    {
                        InfoLabel = "registering unsuccessfull";
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        async public Task OnBackClickedAsync()
        {
            InfoLabel = "";
            await _pageService.PopModalAsync();
        }
    }
}
