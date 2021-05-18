using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmagApplication.Model
{
  class Filter: INotifyPropertyChanged
  {
    private string _selectedItem;

    public string Title { get; set; }
    public List<string> FilterListItem { get; set; } = new List<string>();
    public string SelectedItem
    {
      get { return _selectedItem; }
      set
      {
        if (_selectedItem == value) return;
        _selectedItem = value;
        
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedItem"));
      }
    }


    public event PropertyChangedEventHandler PropertyChanged;
  }
}
