using Mobile.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mobile.ViewModels
{
	public class ShowCartPageViewModel : ViewModelBase
	{
        public ShowCartPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.ShowShoppingCart;
        }
	}
}
