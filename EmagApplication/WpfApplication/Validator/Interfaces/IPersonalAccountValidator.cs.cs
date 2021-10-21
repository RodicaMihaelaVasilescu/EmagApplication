using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Validator.Interfaces
{
    interface IPersonalAccountValidator<T>
    {
        T ValidateFirstName(string firstName);
        T ValidateLastName(string lastName);
        T ValidateUsername(string username);
        T ValidatePassword(string password);
        T ValidateEmail(string email);
    }
}
