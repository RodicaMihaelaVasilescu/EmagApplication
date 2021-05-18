using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using EmagApplication.Model;

namespace EmagApplication.ViewModel
{
  class MainWindowViewModel
  {
    public ObservableCollection<ObservableCollection<Item>> ListOfItems { get; set; } = new ObservableCollection<ObservableCollection<Item>>();
    public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
    public ObservableCollection<Filter> FiltersList { get; set; } = new ObservableCollection<Filter>();

    private string ParfumeCategory = "Perfume";
    public MainWindowViewModel()
    {
      AddItems();
      AddFilters();
    }

    private void AddFilters()
    {
      FiltersList.Add(new Filter
      {
        Title = "Disponibilitate"
      });


      var Category = new Filter
      {
        Title = "Category"
      };
      Category.FilterListItem.Add("All");
      Category.FilterListItem.Add("Perfume");
      Category.FilterListItem.Add("Books");
      FiltersList.Add(Category);


      FiltersList.Add(new Filter
      {
        Title = "Price"
      });
      FiltersList.Add(new Filter
      {
        Title = "Brand"
      });
      FiltersList.Add(new Filter
      {
        Title = "Disponibil prin EasyBox"
      });
      FiltersList.Add(new Filter
      {
        Title = "Pentru"
      });
      FiltersList.Add(new Filter
      {
        Title = "Rating"
      });
      FiltersList.Add(new Filter
      {
        Title = "Disponibilitate"
      });
      FiltersList.Add(new Filter
      {
        Title = "Tip Produs"
      });
      FiltersList.Add(new Filter
      {
        Title = "Set"
      });
      FiltersList.Add(new Filter
      {
        Title = "Tip"
      });
      FiltersList.Add(new Filter
      {
        Title = "Cantitate"
      });
      FiltersList.Add(new Filter
      {
        Title = "Categorie olfactiva"
      });
      FiltersList.Add(new Filter
      {
        Title = "Livrat de"
      });
      FiltersList.Add(new Filter
      {
        Title = "Disponibilitate in Showroom"
      });
    }

    private void AddItems()
    {

      Items.Add(new Item
      {
        Name = "Perfume Nina Ricci - Bella",
        Category = ParfumeCategory,
        Icon = new BitmapImage(new Uri("pack://application:,,,/" + "Resources/perfume_nina_ricci_bella.jpg")),
        Price = 210
      }); ;
      Items.Add(new Item
      {
        Name = "Perfume Nina Ricci - Nina",
        Category = ParfumeCategory,
        Icon = new BitmapImage(new Uri("pack://application:,,,/" + "Resources/perfume_nina_ricci_nina.jpg")),
        Price = 190
      });
      Items.Add(new Item
      {
        Name = "Perfume Nina Ricci - Rose",
        Category = ParfumeCategory,
        Icon = new BitmapImage(new Uri("pack://application:,,,/" + "Resources/perfume_nina_ricci_rose.jpg")),
        Price = 200
      });

      Items.Add(new Item
      {
        Name = "Perfume Nina Ricci - L'eau",
        Category = ParfumeCategory,
        Icon = new BitmapImage(new Uri("pack://application:,,,/" + "Resources/perfume_nina_ricci_eau.jpg")),
        Price = 250
      });

      Items.Add(new Item
      {
        Name = "Perfume Nina Ricci - Avenue Motagne",
        Category = ParfumeCategory,
        Icon = new BitmapImage(new Uri("pack://application:,,,/" + "Resources/perfume_nina_ricci_avenue_motagne.jpg")),
        Price = 150
      });

      Items.Add(new Item
      {
        Name = "Perfume Nina Ricci - Nina Rouge",
        Category = ParfumeCategory,
        Icon = new BitmapImage(new Uri("pack://application:,,,/" + "Resources/perfume_nina_ricci_rouge.jpg")),
        Price = 150
      });

      Items.Add(new Item
      {
        Name = "Agatha Christie - Death on Nile",
        Category = "Book",
        Icon = new BitmapImage(new Uri("pack://application:,,,/" + "Resources/book_agatha_christie_death.png")),
        Price = 25
      });

      Items.Add(new Item
      {
        Name = "Agatha Christie - Murder",
        Category = "Book",
        Icon = new BitmapImage(new Uri("pack://application:,,,/" + "Resources/book_agatha_christie_murder.png")),
        Price = 30
      });


      var List = new ObservableCollection<Item>();
      for (int i = 1; i <= Items.Count; i++)
      {
        List.Add(Items[i - 1]);
        if (i % 4 == 0)
        {
          ListOfItems.Add(List);
          List = new ObservableCollection<Item>();
        }
      }
      if (List.Any())
      {
        ListOfItems.Add(List);
      }

    }
  }
}
