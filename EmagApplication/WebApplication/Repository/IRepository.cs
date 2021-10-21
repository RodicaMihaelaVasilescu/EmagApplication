using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication
{
	public interface IRepository<T> where T : class
	{
		bool Add(T entity);

		IEnumerable<T> GetAll();

		T GetById(Guid id);

		bool Delete(Guid id);

		void Update(T entity);
	}
}