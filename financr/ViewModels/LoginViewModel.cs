using financr.financrAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace financr.ViewModels
{
    public class LoginViewModel : BaseViewModel
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

        public ICommand OnRegisterClickedCommand { get; private set; }

        public LoginViewModel(IPageService pageService)
        {
            _pageService = pageService;

            OnRegisterClickedCommand = new Command(async () => await OnRegisterClickedAsync());
        }

        async public Task OnLoginClickedAsync()
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                InfoLabel = "enter username and password.";
                throw new Exception();
            }
            else
            {
                InfoLabel = "logging in";

                try
                {
                    string txt = await ApiServices.GetToken(Username.Trim().ToLower(), Password);
                    InfoLabel = "login successfull.";
                    await SecureStorage.SetAsync("token", txt);
                    ApiServices.client.DefaultRequestHeaders.Add("Authorization", $"Token {txt}");

                    await Task.Delay(2000);
                }
                catch (Exception ex)
                {
                    InfoLabel = "login unsuccessfull.";
                    throw ex;
                }
            }
        }

        async public Task OnRegisterClickedAsync()
        {
            InfoLabel = "";
            await _pageService.PushModalAsync(new RegisterPage());
        }
    }
}
