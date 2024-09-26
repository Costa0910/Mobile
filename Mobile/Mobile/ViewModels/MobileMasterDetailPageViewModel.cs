using Mobile.Helpers;
using Mobile.ItemViewModels;
using Mobile.Models;
using Mobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mobile.ViewModels
{
	public class MobileMasterDetailPageViewModel : ViewModelBase
	{
        private readonly INavigationService _navigationService;

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public MobileMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
                

            LoadMenus();
        }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Title = Languages.Products,
                    PageName = $"{nameof(ProductsPage)}",
                    Icon = "ic_shopping"
                },
                new Menu
                {
                    Title = Languages.ModifyUser,
                    PageName = $"{nameof(ModifyUserPage)}",
                    Icon = "ic_person"
                },
                new Menu
                {
                    Title = Languages.ShowPurchaseHistory,
                    PageName = $"{nameof(ShowHistoryPage)}",
                    Icon = "ic_history",
                    IsLoginRequired = true
                },
                new Menu
                {
                    Title = Languages.ShowShoppingCart,
                    PageName = $"{nameof(ShowCartPage)}",
                    Icon = "ic_shopping_cart"
                },
                new Menu
                {
                    Title = Languages.Login,
                    PageName = $"{nameof(LoginPage)}",
                    Icon = "ic_exit_to_app"
                },
            };

            Menus = new ObservableCollection<MenuItemViewModel> (
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    Title = m.Title,
                    IsLoginRequired = m.
                    IsLoginRequired,
                    PageName = m.PageName
                }).ToList());
        }
    }
}
