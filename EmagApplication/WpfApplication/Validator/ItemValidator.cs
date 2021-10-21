using WpfApplication.Validator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Validators
{
    class ItemValidator : IItemValidator<ItemValidator>
    {
	    public string ValidationMessage;
        public bool IsValid;

        public ItemValidator()
        {
        }

        public ItemValidator ValidateItemName(string eventName)
        {
            IsValid = true;
            ValidationMessage = string.Empty;
            if (string.IsNullOrEmpty(eventName))
            {
                ValidationMessage = "The name must be filled in";
                IsValid = false;
                return this;
            }
            if (eventName.Count() < 3)
            {
                ValidationMessage = "The name must have at least 3 characters";
                IsValid = false;
            }
            return this;
        }

        public ItemValidator ValidateItemPrice(double price)
        {
	        IsValid = true;
	        ValidationMessage = string.Empty;
            if (price <= 0 )
	        {
		        ValidationMessage = "The price is not valid";
		        IsValid = false;
            }

	        return this;
        }

        public ItemValidator ValidateItemCategory(string category)
        {
	        IsValid = true;
	        ValidationMessage = string.Empty;
	        if (string.IsNullOrEmpty(category))
	        {
		        ValidationMessage = "The category must be selected";
		        IsValid = false;
		        return this;
	        }

	        return this;
        }
    }
}
