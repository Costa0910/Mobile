using System;
using Mobile.Models;
using Mobile.Views;
using Prism.Commands;
using Prism.Navigation;

namespace Mobile.ItemViewModels
{
	public class MenuItemViewModel : Menu
	{
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        public MenuItemViewModel(INavigationService navigationService)
		{
            _navigationService = navigationService;
        }

        private async void SelectMenuAsync()
        {
            await _navigationService.NavigateAsync($"/{nameof(MobileMasterDetailPage)}/NavigationPage/{PageName}");
        }
    }
}

