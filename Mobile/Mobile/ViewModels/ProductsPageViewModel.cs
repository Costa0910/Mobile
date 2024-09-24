using Mobile.Models;
using Mobile.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
	public class ProductsPageViewModel : ViewModelBase
	{
        private bool _isRunning;
        private readonly IAPIService _apiService;
        private ObservableCollection<ProductResponse> _products;
        private INavigationService _navigationService { get; }
        private string _query;
        private List<ProductResponse> _myProducts;
        private DelegateCommand _queryCommand;

        public ObservableCollection<ProductResponse> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public string Query
        {
            get => _query;
            set {
                SetProperty(ref _query, value);
                ShowProducts();
            }
        }

        public DelegateCommand QueryCommand => _queryCommand ?? (_queryCommand = new DelegateCommand(ShowProducts));

        public ProductsPageViewModel(INavigationService navigationService, IAPIService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Products page";
            LoadProductsAsync();
        }

        
       

        private async void LoadProductsAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet) {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Check your internet connection", "Ok");
                });
                return;
            }
            IsRunning = true;

            string baseUrl = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.GetListAsync<ProductResponse>(baseUrl, "/api", "/Products");

            IsRunning = false;
            if (!response.IsSuccess) {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, try again", "Ok");
                return;
            }

            _myProducts = (List<ProductResponse>)response.Result;
            ShowProducts();
        }

        private void ShowProducts()
        {
            if (string.IsNullOrEmpty(Query))
            {
                Products = new ObservableCollection<ProductResponse>(_myProducts);
            } else
            {
                Products = new ObservableCollection<ProductResponse>(_myProducts
                    .Where(p => p.Name.ToLower().Contains(Query.ToLower())));
            }
        }
    }
}   
