using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace WebApplication
{
	public interface IProductRepository : IRepository<Product>
	{
		IEnumerable<Product> GetAllByCategory(string category);
	}
}