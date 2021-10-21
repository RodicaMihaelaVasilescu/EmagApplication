using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataAccess.Model
{
  public class Product
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Icon { get; set; } = @"pack://application:,,,/Resources/no_photo.png";
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; }
    public bool Disponibility { get; set; } = true;
    public double Price { get; set; }
    public double Rating { get; set; }
    public string Brand { get; set; }
  }
}
