using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace WpfApplication.Repository
{
	public class ProductService //: IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		//public async Task<Product> GetProductByIdAsync(int id)
		//{
		//	return await _productRepository.GetProductByIdAsync(id);
		//}

		//public async Task<Product> AddProductAsync(Product product)
		//{
		//	return await _productRepository.AddAsync(product);
		//}
	}

}
