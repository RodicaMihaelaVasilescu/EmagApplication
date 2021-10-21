using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Validator.Interfaces
{
    interface IItemValidator<T>
    {
        T ValidateItemName(string name);
        T ValidateItemPrice(double price);
        T ValidateItemCategory(string category);

    }
}
