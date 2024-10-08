﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace Mobile.ViewModels
{

	public class LoginPageViewModel : ViewModelBase
	{
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;
        public string Email { get; set; }
        public string Password { get =>_password; set => SetProperty(ref _password, value); }
        public bool IsRunning { get => _isRunning; set => SetProperty(ref _isRunning, value); }
        public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(login));

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Login";
            IsEnabled = true;
        }

        private async void login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a email.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a password.", "Accept");
                return;
            }

            
            await App.Current.MainPage.DisplayAlert("You are in!", "Welcome to adventure!", "Ok");
        }
    }
}
