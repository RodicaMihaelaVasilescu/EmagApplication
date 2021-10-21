using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using WebApplication;

namespace WebApplication
{

	public interface IProductService : IService<Product>
	{
		IEnumerable<Product> GetAllByCategory(string category);

		Task<bool> AddProductAsync(Product product);

		Task<bool> DeleteProductAsync(Guid id);
	}
}