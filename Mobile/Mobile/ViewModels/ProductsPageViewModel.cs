    using Mobile.Helpers;
    using Mobile.ItemViewModels;
    using Mobile.Models;
    using Mobile.Services;
    using Prism.Commands;
    using Prism.Navigation;
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
            private ObservableCollection<ProductItemViewModel> _products;
            private INavigationService _navigationService { get; }
            private string _query;
            private List<ProductResponse> _myProducts;
            private DelegateCommand _queryCommand;

            public ObservableCollection<ProductItemViewModel> Products
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
                        await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                    });
                    return;
                }
                IsRunning = true;

                string baseUrl = App.Current.Resources["UrlAPI"].ToString();
                var response = await _apiService.GetListAsync<ProductResponse>(baseUrl, "/api", "/Products");

                IsRunning = false;
                if (!response.IsSuccess) {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                    return;
                }

                _myProducts = (List<ProductResponse>)response.Result;
                ShowProducts();
            }

            private void ShowProducts()
            {
                if (string.IsNullOrEmpty(Query))
                {
                    Products = new ObservableCollection<ProductItemViewModel>(_myProducts.Select(p =>new ProductItemViewModel(_navigationService) {
                        Name = p.Name,
                        Id = p.Id,
                        Price = p.Price,
                        ImageFullPath = p.ImageFullPath,
                        LastPurchase = p.LastPurchase,
                        LastSale = p.LastSale,
                        IsAvailable = p.IsAvailable,
                        Stock = p.Stock,
                        User = p.User,
                        ImageUrl = p.ImageUrl
                    }).ToList());
                } else
                {
                    Products = new ObservableCollection<ProductItemViewModel>(_myProducts
                        .Where(p => p.Name.ToLower()
                        .Contains(Query.ToLower()))
                        .Select(p => new ProductItemViewModel(_navigationService) {
                            Name = p.Name,
                            Id = p.Id,
                            Price = p.Price,
                            ImageFullPath = p.ImageFullPath,
                            LastPurchase = p.LastPurchase,
                            LastSale = p.LastSale,
                            IsAvailable = p.IsAvailable,
                            Stock = p.Stock,
                            User = p.User,
                            ImageUrl = p.ImageUrl
                        }).ToList());
                }
            }
        }
    }   
