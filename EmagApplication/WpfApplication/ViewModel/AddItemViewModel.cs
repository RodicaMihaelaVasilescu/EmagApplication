using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;
using Prism.Commands;
using DataAccess.Constants;
using DataAccess.Model;
using WpfApplication.Services;
using WpfApplication.Utility;
using WpfApplication.Validators;
using DataAccess.Dto;

namespace WpfApplication.ViewModel
{
	class AddItemViewModel : INotifyPropertyChanged
	{
		private ProductDto _product;
		private string title;
		private string _price;
		private string _category;
		private string _selectedCategory;
		private string _nameValidationMessage;
		private string _priceValidationMessage;
		private string _categoryValidationMessage;
		private bool _isNameValid = true;
		private bool _isPriceValid = true;
		private bool _isCategoryValid = true;
		private ItemValidator Validator = new ItemValidator();
		private BitmapImage _icon = new BitmapImage(new Uri("pack://application:,,,/" + "Resources/no_photo.png"));

		public String Name { get; set; }

		public List<String> Categories { get; set; } =
		  typeof(FilterCategoryConstants).GetAllPublicConstantValues<string>().Where(c => c != FilterCategoryConstants.All).ToList();
		public String Category
		{
			get => _category;
			set
			{
				if (_category == value) return;
				_category = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Category"));
			}
		}

		public string Price { get; set; }
		public BitmapImage Icon
		{
			get => _icon;
			set
			{
				if (_icon == value) return;
				_icon = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
			}
		}

		public String SelectedCategory
		{
			get => _selectedCategory;
			set
			{
				if (_selectedCategory == value) return;
				_selectedCategory = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedCategory"));
			}
		}

		public bool IsNameValid
		{
			get { return _isNameValid; }
			set
			{
				if (value == _isNameValid) return;
				_isNameValid = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsNameValid"));

			}
		}

		public string NameValidationMessage
		{
			get { return _nameValidationMessage; }
			set
			{
				if (value == _nameValidationMessage) return;
				_nameValidationMessage = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NameValidationMessage"));
			}
		}


		public bool IsCategoryValid
		{
			get { return _isCategoryValid; }
			set
			{
				if (value == _isCategoryValid) return;
				_isCategoryValid = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsCategroyValid"));

			}
		}

		public string CategoryValidationMessage
		{
			get { return _categoryValidationMessage; }
			set
			{
				if (value == _categoryValidationMessage) return;
				_categoryValidationMessage = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CategoryValidationMessage"));
			}
		}



		public bool IsPriceValid
		{
			get { return _isPriceValid; }
			set
			{
				if (value == _isPriceValid) return;
				_isPriceValid = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsPriceValid"));

			}
		}

		public string PriceValidationMessage
		{
			get { return _priceValidationMessage; }
			set
			{
				if (value == _priceValidationMessage) return;
				_priceValidationMessage = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PriceValidationMessage"));
			}
		}

		public String Description { get; set; }
		public ICommand UploadCommand { get; set; }
		public ICommand AddCommand { get; set; }
		public ICommand ResetCommand { get; set; }
		public Action CloseAction { get; set; }


		public event EventHandler<ItemEventArgs> AddItemEvent;
		public event EventHandler<ItemEventArgs> EditItemEvent;

		private ProductService _productService;
		public AddItemViewModel(ProductService productService, ProductDto product = null)
		{
			this._productService = productService;
			if (product == null)
			{
				this._product = new ProductDto();
				ButtonContent = "Add";
			}
			else
			{
				this._product = product;
				Name = product.Name;
				Price = product.Price.ToString();
				Description = product.Description;
				Category = product.Category;
				SelectedCategory = Category;
				if (!product.Icon.StartsWith(@"/"))
					Icon = new BitmapImage(new Uri(product.Icon));
				ButtonContent = "Edit";
			}
			UploadCommand = new DelegateCommand(UploadCommandExecute);
			Application.Current.Dispatcher.Invoke(() =>
			{
				AddCommand = new DelegateCommand(async () => await AddCommandExecute());
			});

		}


		public string ButtonContent { get; set; }


		public event PropertyChangedEventHandler PropertyChanged;

		private void UploadCommandExecute()
		{
			OpenFileDialog OpenFile = new OpenFileDialog();
			OpenFile.Multiselect = false;
			OpenFile.Title = "Select Picture(s)";
			OpenFile.Filter = "ALL supported Graphics| *.jpeg; *.jpg;*.png;";
			if (OpenFile.ShowDialog() == true)
			{
				Icon = new BitmapImage(new Uri(OpenFile.FileName));
			}
		}


		private async Task AddCommandExecute()
		{
			if (!IsItemValid())
			{
				return;
			}
			if (SelectedCategory == null)
      {
				SelectedCategory = FilterCategoryConstants.All;

			}

			if (ButtonContent == "Add")
			{
				await _productService.PostItem(new Product
				{
					Id = Guid.NewGuid(),
					Icon = Icon.UriSource.AbsoluteUri,
					Name = Name,
					Price = Convert.ToDouble(Price),
					Category = SelectedCategory,
					Description = Description
				});
				CloseAction.Invoke();
			}
			else
			{
				await _productService.PutItem(new Product
				{
					Id = _product.Id,
					Icon = Icon.UriSource.AbsoluteUri,
					Name = Name,
					Price = Convert.ToDouble(Price),
					Category = Category,
					Description = Description
				});
				CloseAction.Invoke();
			}
		}

		private bool IsItemValid()
		{
			bool isNameValid = NameValidation();
			bool isPriceValid = PriceValidation();
			bool isCategoryValid = CategoryValidation();
			return isNameValid && isPriceValid && isCategoryValid;
		}

		private bool PriceValidation()
		{
			var validationResult = Validator.ValidateItemPrice(Convert.ToDouble(Price));
			PriceValidationMessage = validationResult.ValidationMessage;
			IsPriceValid = validationResult.IsValid;
			return IsPriceValid;
		}

		private bool NameValidation()
		{
			var validationResult = Validator.ValidateItemName(Name);
			NameValidationMessage = validationResult.ValidationMessage;
			IsNameValid = validationResult.IsValid;
			return IsNameValid;
		}

		private bool CategoryValidation()
		{
			var validationResult = Validator.ValidateItemCategory(SelectedCategory);
			CategoryValidationMessage = validationResult.ValidationMessage;
			IsCategoryValid = validationResult.IsValid;
			return IsCategoryValid;
		}
	}
}
