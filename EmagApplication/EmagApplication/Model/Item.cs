using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EmagApplication.Model
{
  class Item
  {
    public string Name { get; set; }
    public BitmapImage Icon { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; set; } = true;
    public double Price { get; set; }
    public double Rating { get; set; }
    public string Brand { get; set; }

  }
}
