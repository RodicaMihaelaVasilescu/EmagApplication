using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using WebApplication.Hub;
using WebApplication.Timer;

namespace WebApplication
{
	using System.Web.Http;
	using System.Web.Http.Results;

	using Microsoft.AspNet.SignalR;

	public class ProductService : IProductService
	{
		private readonly IProductRepository productRepository;

		private readonly IHubContext productsHub;

		//private static readonly AddProductSimulator simulator = new AddProductSimulator(TimeSpan.FromSeconds(5));

		public ProductService(IProductRepository productRepository)
		{
			this.productRepository = productRepository;

			this.productsHub = GlobalHost.ConnectionManager.GetHubContext<ItemHub>();

		}

		public async Task<bool> AddProductAsync(Product entity)
		{
			if (this.productRepository.Add(entity))
			{
				await this.productsHub.Clients.All.AddNewProductToPage(entity);

				return true;
			}

			return false;
		}

		public async Task<bool> AddNewItemProductAsync()
		{
			await this.productsHub.Clients.All.AddNewProductToPage();

			return true;
		}

		public void Add(Product entity)
		{
			this.productRepository.Add(entity);
		}

		public IEnumerable<Product> GetAll()
		{
			return this.productRepository.GetAll();
		}

		public IEnumerable<Product> GetAllByCategory(string category)
		{
			return this.productRepository.GetAllByCategory(category);
		}

		public Product GetById(Guid id)
		{
			return this.productRepository.GetById(id);
		}

		public void Delete(Guid id)
		{
			this.productRepository.Delete(id);
		}

		public async Task<bool> DeleteProductAsync(Guid id)
		{
			if (this.productRepository.Delete(id))
			{
				await this.productsHub.Clients.All.DeleteProductFromPage(id);

				return true;
			}

			return false;
		}

		public async Task Update(Product entity)
		{
			this.productRepository.Update(entity);
			await this.productsHub.Clients.All.PutProductToPage(entity);
		}
  }
}