using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace WebApplication
{
	using System.Runtime.CompilerServices;
	using System.Web.Http.Dependencies;

	using Microsoft.Practices.Unity;

	public class UnityDependencyResolver : IDependencyResolver
	{
		private readonly IUnityContainer container;

		public UnityDependencyResolver(IUnityContainer container)
		{
			this.container = container;
		}

		public void Dispose()
		{
			this.container.Dispose();
		}

		public object GetService(Type serviceType)
		{
			try
			{
				return this.container.Resolve(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return this.container.ResolveAll(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return new List<object>();
			}
		}

		public IDependencyScope BeginScope()
		{
			var child = this.container.CreateChildContainer();
			return new UnityDependencyResolver(child);
		}
	}
}