﻿using System;
using Mobile.Models;
using Mobile.Views;
using Prism.Commands;
using Prism.Navigation;

namespace Mobile.ItemViewModels
{
	public class ProductItemViewModel : ProductResponse
	{
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectProductCommand;

        public ProductItemViewModel(INavigationService navigationService)
		{
            _navigationService = navigationService;
        }

        public DelegateCommand SelectProductCommand => _selectProductCommand ?? (_selectProductCommand = new DelegateCommand(SelectProductAsync));

        private async void SelectProductAsync()
        {
            NavigationParameters parameters = new NavigationParameters {
                {"product", this }
            };

            await _navigationService.NavigateAsync(nameof(ProductDetailsPage), parameters);
        }
    }
}

