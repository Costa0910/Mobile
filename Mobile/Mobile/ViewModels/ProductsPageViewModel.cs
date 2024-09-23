using Mobile.Models;
using Mobile.Services;
using Prism.Navigation;
using System.Collections.Generic;

namespace Mobile.ViewModels
{
	public class ProductsPageViewModel : ViewModelBase
	{
        private readonly IAPIService _apiService;
        private List<ProductResponse> _products;

        public ProductsPageViewModel(INavigationService navigationService, IAPIService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Products page";
            LoadProductsAsync();
        }

        public List<ProductResponse> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        private async void LoadProductsAsync()
        {
            string baseUrl = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.GetListAsync<ProductResponse>(baseUrl, "/api", "/Products");

            if (!response.IsSuccess) {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, try again", "Ok");
                return;
            }

            Products = (List<ProductResponse>)response.Result;
        }
    }
}
