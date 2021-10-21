//using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Helpers;

namespace WebApplication.Hub
{
  public class GetHubConnectionString
  {

    public string HubConnectionString;
    private GetHubConnectionString()
    {
      HubConnectionString = string.Format("http://localhost:{0}/", AppConfigManager.Get("Port"));
    }
    private static GetHubConnectionString instance = null;
    public static GetHubConnectionString Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new GetHubConnectionString();
        }
        return instance;
      }
    }


  }
}
