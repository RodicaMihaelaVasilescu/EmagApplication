using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Commands;
using DataAccess.Dto;
using DataAccess.Model;
using DataAccess.Constants;
using WpfApplication.Helper;
using WpfApplication.Services;
using WpfApplication.Utility;
using Microsoft.AspNet.SignalR.Client;

namespace WpfApplication.ViewModel
{

	class MainWindowViewModel : INotifyPropertyChanged
	{
		private string _searchText;
		private List<ProductDto> AllItems { get; set; } = new List<ProductDto>();
		private CollectionViewSource _filteredItems = new CollectionViewSource();
		private AddItemViewModel _addItemViewModel;
		private ProductDto _selectedProduct;

		public CollectionViewSource FilteredItems
		{
			get => _filteredItems;
			set
			{
				if (_filteredItems == value) return;
				_filteredItems = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredItems"));
			}
		}

		public ICommand SearchCommand { get; set; }
		public ICommand LoadCommand { get; set; }
		public ICommand AddItemCommand { get; set; }
		public ICommand EditItemCommand { get; set; }
		public ICommand RemoveItemCommand { get; set; }
		public ICommand ResetCommand { get; set; }

		public ProductDto SelectedProduct
		{
			get => _selectedProduct;
			set
			{
				if (_selectedProduct == value) return;
				_selectedProduct = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProduct"));
			}

		}
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Filter> FiltersList { get; set; } = new ObservableCollection<Filter>();

		public String SearchText
		{
			get => _searchText;
			set
			{
				if (_searchText == value) return;
				_searchText = value;
				if (AllItems.Any())
				{
					FilteredItems.Source = AllItems.Where(i => i.Name.ToLower().Contains(value.ToLower())).ToList();
				}
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SearchText"));
			}
		}


		#region Constructor
		//private SignalRHubClient _palletStorageDeviceHubClient;
		public MainWindowViewModel()
		{
			SearchCommand = new DelegateCommand(SearchCommandExecute);
			AddItemCommand = new DelegateCommand(AddItemCommandExecute);
			EditItemCommand = new DelegateCommand(EditItemCommandExecute);
			RemoveItemCommand = new DelegateCommand(async () => await RemoveItemCommandExecute());
			LoadCommand = new DelegateCommand(async () => await LoadCommandExecute());
			_productService = new ProductService(client);
			ProductService productService = new ProductService(client);

			productService.ItemReceived += ProductService_ItemReceived;
			productService.EmptyItemReceived += ProductService_EmptyItemReceived;
			productService.ItemDeleted += ProductService_RemoveProductCommand;
			productService.ItemPut += ProductService_PutProductCommand;

			ApplicationHub app = new ApplicationHub();

			AddItems();
			AddFilters();
			Task loadCommand = LoadCommandExecute();
		}

		private void ProductService_PutProductCommand(Product obj)
		{
			Application.Current.Dispatcher.Invoke(() =>
		   {
			   var product = AllItems.FirstOrDefault(o => o.Id == obj.Id);
			   if (product == null)
			   {
				   return;
			   }

			   //AllItems.Remove(productToRemove);
			   product.Name = obj.Name;
			   product.Price = obj.Price;
			   product.Category = obj.Category;
			   product.Icon = obj.Icon;
			   product.Description = obj.Description;
			   //AllItems.Add(product);
			   FilteredItems.Source = AllItems;
			   ICollectionView dataView = CollectionViewSource.GetDefaultView(AllItems);
			   dataView.Refresh();
		   }
					);
		}

		private async Task LoadCommandExecute()
		{
			var allItems = new List<Product>();
			await Application.Current.Dispatcher.Invoke(async () =>
			{
				allItems = await _productService.GetAllItems();
			});

			AllItems = new List<ProductDto>();
			foreach (var item in allItems)
			{
				AllItems.Add(new ProductDto(item));
			}
			FilteredItems.Source = AllItems;
			ICollectionView dataView = CollectionViewSource.GetDefaultView(AllItems);
			dataView.Refresh();
			FilteredItems.Source = AllItems;
		}

		private void ProductService_EmptyItemReceived(string obj)
		{

			Application.Current.Dispatcher.Invoke(async () =>
			 {
				 var id = Guid.NewGuid();
				 await _productService.PostItem(new Product
				 {
					 Id = id,
					 Category = FilterCategoryConstants.All,
					 Name = obj,
					 Price = 0
				 });
			 });

		}

		private void ProductService_ItemReceived(Product obj)
		{
			AllItems.Add(new ProductDto(obj));
			Application.Current.Dispatcher.Invoke(async () =>
			{
				FilteredItems.Source = AllItems;
				ICollectionView dataView = CollectionViewSource.GetDefaultView(AllItems);
				dataView.Refresh();
			});
		}

		private HttpClient client = new HttpClient();
		private ProductService _productService;
		public void EditItemCommandExecute()
		{
			if (SelectedProduct == null)
			{
				return;
			}
			Window addItemWindow = new Window();
			_addItemViewModel = new AddItemViewModel(_productService, SelectedProduct);

			WindowManager.CreateGeneralWindow(addItemWindow, _addItemViewModel, "AddItem", "WpfApplication.View.AddItemControl");
			if (_addItemViewModel.CloseAction == null)
			{
				_addItemViewModel.CloseAction = () => addItemWindow.Close();
			}
			addItemWindow.Show();

			_addItemViewModel.AddItemEvent += this.AddItemInList;
			_addItemViewModel.EditItemEvent += this.EditItemInList;
		}



		public void AddItemCommandExecute()
		{
			Window addItemWindow = new Window();
			_addItemViewModel = new AddItemViewModel(_productService);

			WindowManager.CreateGeneralWindow(addItemWindow, _addItemViewModel, "AddItem", "WpfApplication.View.AddItemControl");
			if (_addItemViewModel.CloseAction == null)
			{
				_addItemViewModel.CloseAction = () => addItemWindow.Close();
			}
			addItemWindow.Show();

			_addItemViewModel.AddItemEvent += this.AddItemInList;

		}

		public async Task RemoveItemCommandExecute()
		{
			if (SelectedProduct == null)
				return;
			await _productService.DeleteItem(SelectedProduct.Id);
		}

		private void ProductService_RemoveProductCommand(Guid selectedProduct)
		{
			if (selectedProduct == null)
			{
				return;
			}
			Application.Current.Dispatcher.Invoke(async () =>
			{
				AllItems.RemoveAll(i => i.Id == selectedProduct);
				ICollectionView dataView = CollectionViewSource.GetDefaultView(AllItems);
				dataView.Refresh();
			});
		}

		#endregion
		private void EditItemInList(object sender, ItemEventArgs e)
		{
			Product product = e.Product;
			var existingItem = AllItems.Where(i => i.Name == product.Name).FirstOrDefault();
			FilteredItems.Source = AllItems;
			ICollectionView dataView = CollectionViewSource.GetDefaultView(AllItems);
			dataView.Refresh();
			_addItemViewModel.CloseAction();
		}
		public void AddItemInList(object sender, ItemEventArgs e)
		{
			Product product = e.Product;
			AllItems.Add(new ProductDto(product));
			FilteredItems.Source = AllItems;
			_addItemViewModel.CloseAction();
			ICollectionView dataView = CollectionViewSource.GetDefaultView(_filteredItems.Source);
			dataView.Refresh();
		}

		public void FilterSelectedItem_PropertyChanged(object sender, EventArgs e)
		{
			Filter filter = sender as Filter;
			ICollectionView dataView = CollectionViewSource.GetDefaultView(_filteredItems.Source);

			dataView.SortDescriptions.Clear();
			FilteredItems.Source = AllItems;
			if (filter.Title == FilterTitleConstants.Price)
			{
				if (filter.SelectedProduct == FilterPriceConstants.Ascendent)
				{
					dataView.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
				}
				else if (filter.SelectedProduct == FilterPriceConstants.Descendent)
				{
					dataView.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));
				}
			}
			else if (filter.Title == FilterTitleConstants.Category)
			{
				if (filter.SelectedProduct == "All")
				{
					dataView.Filter = (obj) =>
					{
						return true;
					};
				}
				else
				{
					dataView.Filter = (obj) =>
					{
						ProductDto product = obj as ProductDto;
						return product != null && (product.Category.Contains(filter.SelectedProduct) || filter.SelectedProduct == "All");
					};

				}
			}
			else if (filter.Title == FilterTitleConstants.Disponibility)
			{
				FilteredItems.Source = AllItems.Where(i => filter.SelectedProduct == YesNoConstants.Yes ? i.Disponibility : !i.Disponibility).ToList();
			}
			dataView.Refresh();
		}


		private void AddFilters()
		{
			AddFilter(FilterTitleConstants.Disponibility, typeof(YesNoConstants).GetAllPublicConstantValues<string>());
			AddFilter(FilterTitleConstants.Category, typeof(FilterCategoryConstants).GetAllPublicConstantValues<string>());
			AddFilter(FilterTitleConstants.Price, typeof(FilterPriceConstants).GetAllPublicConstantValues<string>());


			FiltersList.Add(new Filter
			{
				Title = FilterTitleConstants.Brand,
			});

			AddFilter(FilterTitleConstants.EasyBoxAvailability, typeof(YesNoConstants).GetAllPublicConstantValues<string>());
			FiltersList.Add(new Filter
			{
				Title = FilterTitleConstants.For
			});
			FiltersList.Add(new Filter
			{
				Title = FilterTitleConstants.Rating
			});
			FiltersList.Add(new Filter
			{
				Title = FilterTitleConstants.ProductType
			});
			FiltersList.Add(new Filter
			{
				Title = FilterTitleConstants.Set
			});
			FiltersList.Add(new Filter
			{
				Title = FilterTitleConstants.Type
			});
			FiltersList.Add(new Filter
			{
				Title = FilterTitleConstants.Quantity
			});
			FiltersList.Add(new Filter
			{
				Title = FilterTitleConstants.DeliveredBy
			});
			AddFilter(FilterTitleConstants.ShowroomAvailability, typeof(YesNoConstants).GetAllPublicConstantValues<string>());
		}

		private void AddFilter(string disponibility, List<string> list)
		{
			Filter f = (new Filter
			{
				Title = disponibility,
				FilterListItem = list
			});
			f.SelectedProductChanged += this.FilterSelectedItem_PropertyChanged;
			FiltersList.Add(f);
		}

		private void AddItems()
		{

			var allItems = new List<Product>();
			Application.Current.Dispatcher.Invoke(async () =>
		   {
			   allItems = await _productService.GetAllItems();
		   });
			foreach (var item in allItems)
			{
				AllItems.Add(new ProductDto(item));
			}
			FilteredItems.Source = AllItems;
			ICollectionView dataView = CollectionViewSource.GetDefaultView(AllItems);
			dataView.Refresh();
			FilteredItems.Source = AllItems;

		}

		private void SearchCommandExecute()
		{

		}
	}
}
