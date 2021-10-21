using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace WpfApplication.Utility
{
	class ItemEventArgs : EventArgs

	{
		public Product Product { get; set; }

		public ItemEventArgs(Product product)
		{
			this.Product = product;
		}

	}
}
