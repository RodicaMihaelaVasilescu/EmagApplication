using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DataAccess.Model;
using DataAccess.Constants;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace WebApplication
{

	public class ValuesController : ApiController
	{
		private IProductService productService;

		public ValuesController(IProductService productService)
		{
			this.productService = productService;
		}
		// GET api/values 
		public IEnumerable<Product> Get()
		{
			return productService.GetAll();
		}

		// GET api/values/5 
		public Product Get(Guid id)
		{
			return productService.GetById(id);
			//return ItemList.Instance.Items.FirstOrDefault(i => i.Name == name);
		}

		// POST api/values 
		public void Post([FromBody] Product product)
		{
			//ItemList.Instance.Items.Add(product);

			productService.AddProductAsync(product);
		}

		// PUT api/values/5 
		public void Put([FromBody] Product product)
		{
			productService.Update(product);
		}

		// DELETE api/values/5 
		public void Delete([FromBody] Guid id)
		{
			//ItemList.Instance.Items.RemoveAll(p=>p.Id == id);
			productService.DeleteProductAsync(id);
		}
	}
}
