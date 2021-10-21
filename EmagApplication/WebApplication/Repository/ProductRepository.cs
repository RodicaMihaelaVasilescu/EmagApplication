using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DataAccess.Constants;
using DataAccess.Model;
using WebApplication;
using WebApplication.Helpers;

namespace WebApplication
{

  public class ProductRepository : IProductRepository
  {
    private readonly IDictionary<Guid, Product> products;

    private static ProductRepository instance = null;

    public ProductRepository()
    {
      products = new Dictionary<Guid, Product>();

      var id = Guid.NewGuid();
      products.Add(id, new Product
      {
        Id = id,
        Name = "Perfume Nina Ricci - Bella",
        Category = FilterCategoryConstants.Perfume,
        Icon = @"pack://application:,,,/" + "Resources/perfume_nina_ricci_bella.jpg",
        Price = 210
      });

      id = Guid.NewGuid(); 
      products.Add(id, new Product
      {
        Id = id,
        Name = "Perfume Nina Ricci - Nina",
        Category = FilterCategoryConstants.Perfume,
        Icon = @"pack://application:,,,/" + "Resources/perfume_nina_ricci_nina.jpg",
        Price = 190
      });

      id = Guid.NewGuid(); 
      products.Add(id, new Product
      {
        Id = id,
        Name = "Perfume Nina Ricci - Rose",
        Category = FilterCategoryConstants.Perfume,
        Icon = @"pack://application:,,,/" + "Resources/perfume_nina_ricci_rose.jpg",
        Price = 200
      });

      id = Guid.NewGuid(); 
      products.Add(id, new Product
      {
        Id = id,
        Name = "Perfume Nina Ricci - L'eau",
        Category = FilterCategoryConstants.Perfume,
        Icon = @"pack://application:,,,/" + "Resources/perfume_nina_ricci_eau.jpg",
        Price = 250
      });


      id = Guid.NewGuid(); 
      products.Add(id, new Product
      {
        Id = id,
        Name = "Perfume Nina Ricci - Avenue Motagne",
        Category = FilterCategoryConstants.Perfume,
        Icon = @"pack://application:,,,/" + "Resources/perfume_nina_ricci_avenue_motagne.jpg",
        Price = 150
      });

      id = Guid.NewGuid(); 
      products.Add(id, new Product
      {
        Id = id,
        Name = "Perfume Nina Ricci - Nina Rouge",
        Category = FilterCategoryConstants.Perfume,
        Icon = @"pack://application:,,,/" + "Resources/perfume_nina_ricci_rouge.jpg",
        Price = 150,
        Description = "Newest collection"
      });

      id = Guid.NewGuid(); 
      products.Add(id, new Product
      {
        Id = id,
        Name = "Black Wrist Watch",
        Category = FilterCategoryConstants.Electronics,
        Icon = @"pack://application:,,,/" + "Resources/watch_item.png",
        Price = 320
      });

      id = Guid.NewGuid();
      products.Add(id, new Product
      {
        Id = id,
        Name = "Agatha Christie - Death on the Nile",
        Category = FilterCategoryConstants.Book,
        Icon = @"pack://application:,,,/" + "Resources/book_agatha_christie_death.png",
        Price = 25
      });

      id = Guid.NewGuid(); products.Add(id, new Product
      {
        Id = id,
        Name = "Agatha Christie - Murder",
        Category = FilterCategoryConstants.Book,
        Icon = @"pack://application:,,,/" + "Resources/book_agatha_christie_murder.png",
        Price = 30
      });

      id = Guid.NewGuid(); 
      products.Add(id, new Product
      {
        Id = id,
        Name = "Programming - Python",
        Category = FilterCategoryConstants.Book,
        Icon = @"pack://application:,,,/" + "Resources/book_programming_python.png",
        Price = 34,
        Description = "Python is an interpreted high-level general-purpose programming language. Python's design philosophy emphasizes code readability with its notable use of significant indentation. Its language constructs as well as its object-oriented approach aim to help programmers write clear, logical code for small and large-scale projects.[29] Python is dynamically - typed and garbage - collected.It supports multiple programming paradigms"

      });
      id = Guid.NewGuid(); 
      products.Add(id, new Product
      {
        Id = id,
        Name = "Programming - Node JS",
        Category = FilterCategoryConstants.Book,
        Icon = @"pack://application:,,,/" + "Resources/book_programming_nodejs.png",
        Price = 35,
        Description = "Node.js is an open - source, cross - platform, back - end JavaScript runtime environment that runs on the V8 engine and executes JavaScript code outside a web browser.Node.js lets developers use JavaScript to write command line tools and for server - side scripting"
      });

      id = Guid.NewGuid();
      products.Add(id, new Product
      {
        Id = id,
        Name = "Keyboard",
        Category = FilterCategoryConstants.Electronics,
        Icon = @"pack://application:,,,/" + "Resources/keyboard.png",
        Price = 35
      });

    }
    public static ProductRepository Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new ProductRepository();
        }
        return instance;
      }
    }

    public bool Add(Product entity)
    {
      Instance.products.Add(entity.Id, entity);
      //return true;
      return Instance.products.TryGetValue(entity.Id, out entity);
    }

    public IEnumerable<Product> GetAll()
    {
      return Instance.products.Values;
    }

    public IEnumerable<Product> GetAllByCategory(string category)
    {
      return Instance.products.Values.Where(x => x.Category == category);
    }

    public Product GetById(Guid id)
    {
      return Instance.products[id];
    }

    public bool Delete(Guid id)
    {
      var result = Instance.products.Remove(id);

      if (result)
      {
        return true;
      }

      return false;
    }

    public void Update(Product entity)
    {
      Instance.products.Remove(entity.Id);
      Instance.products.Add(entity.Id, entity);
    }
  }
}