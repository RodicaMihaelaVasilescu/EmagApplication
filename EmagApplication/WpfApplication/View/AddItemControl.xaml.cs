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

namespace WpfApplication.View
{
	/// <summary>
	/// Interaction logic for AddItemControl.xaml
	/// </summary>
	public partial class AddItemControl : UserControl
	{
		public AddItemControl()
		{
			InitializeComponent();
		}



		private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
		{

			if (!char.IsDigit(e.Text, e.Text.Length - 1))
				e.Handled = true;
		}
	}
}
