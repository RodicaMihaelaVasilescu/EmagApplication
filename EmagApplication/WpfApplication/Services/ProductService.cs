using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

using System.Windows;
using DataAccess.Model;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;
using WebApplication.Hub;
using WebApplication.Helpers;
using WebApplication;

namespace WpfApplication.Services
{
  class ProductService
  {
    private readonly HttpClient httpClient;
    private string baseAddress;
    private readonly HubConnection conection;
    private readonly IHubProxy itemsHub;
    public event Action<Product> ItemReceived;
    public event Action<Product> ItemPut;
    public event Action<Guid> ItemDeleted;
    public event Action<string> EmptyItemReceived;

    public ProductService(HttpClient httpClient)
    {
      this.httpClient = httpClient;
      baseAddress = GetHubConnectionString.Instance.HubConnectionString;
      this.conection = new HubConnection(baseAddress);

      itemsHub = this.conection.CreateHubProxy("ItemHub");
      itemsHub.On<string>("AddProductToPage", item => EmptyItemReceived?.Invoke((item)));
      itemsHub.On<Product>("AddNewProductToPage", item => ItemReceived?.Invoke((item)));
      itemsHub.On<Product>("PutProductToPage", item => ItemPut?.Invoke((item)));
      itemsHub.On<Guid>("DeleteProductFromPage", item => ItemDeleted?.Invoke((item)));
      this.conection.Start().Wait();
    }

    public async Task PostItem(Product product)
    {
      await this.httpClient
    .PostAsync<Product>(this.baseAddress + "api/values",
    product,
    new JsonMediaTypeFormatter());
    }

    public async Task DeleteItem(Guid id)
    {
      var request = new HttpRequestMessage
      {
        Method = HttpMethod.Delete,
        RequestUri = new Uri(baseAddress + "api/values"),
        Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
      };
      var response = await httpClient.SendAsync(request);

    }

    public async Task PutItem(Product id)
    {
      var request = new HttpRequestMessage
      {
        Method = HttpMethod.Put,
        RequestUri = new Uri(baseAddress + "api/values"),
        Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
      };
      var response = await httpClient.SendAsync(request);

    }

    public async Task<List<Product>> GetAllItems()
    {
      string content = "";

      var allItem = await this.httpClient
        .GetAsync(this.baseAddress + "api/values");

      content = await allItem.Content.ReadAsStringAsync();
      var products = JsonConvert.DeserializeObject<List<Product>>(content);
      return products;
    }

    public async Task<IEnumerable<Product>> GetAllProductsByCategory(string category)
    {
      var response = await this.httpClient
        .GetAsync(this.baseAddress + "api/items/get/" + category);

      var content = await response.Content.ReadAsStringAsync();

      var products = JsonConvert.DeserializeObject<List<Product>>(content);

      return products;
    }

  }
}
