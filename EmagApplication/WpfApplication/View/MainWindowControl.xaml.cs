using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication.ViewModel;

namespace WpfApplication.View
{
  /// <summary>
  /// Interaction logic for MainWindowControl.xaml
  /// </summary>
  public partial class MainWindowControl : UserControl
  {
		private MainWindowViewModel vm = new MainWindowViewModel();
		public MainWindowControl()
		{
			InitializeComponent();
			ItemsPanel.Width = System.Windows.SystemParameters.WorkArea.Width - 400;
			DataContext = vm;
		}

		private void EditItem_OnClick(object sender, RoutedEventArgs e)
		{
			vm.EditItemCommandExecute();
		}

		private void RemoveItem_OnClick(object sender, RoutedEventArgs e)
		{
			vm.RemoveItemCommandExecute();
		}
	}
}

