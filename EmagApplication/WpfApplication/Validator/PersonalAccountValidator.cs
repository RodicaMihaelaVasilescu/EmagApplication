using WpfApplication.Validator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Validators
{
    class PersonalAccountValidator : IPersonalAccountValidator<PersonalAccountValidator>
    {
        //ClubulPasionatilorDeArtaEntities _context;
        public string ValidationMessage;
        public bool IsValid;

        public PersonalAccountValidator(/*ClubulPasionatilorDeArtaEntities context*/)
        {
            //_context = context;
        }

        public PersonalAccountValidator ValidateEmail(string email)
        {
            IsValid = true;
            ValidationMessage = string.Empty;
            if (string.IsNullOrEmpty(email))
            {
                ValidationMessage = "The email field must be filled in";
                IsValid = false;
                return this;
            }

            if (!email.Any(letter => letter == '@') || !email.Any(letter => letter == '.') || email.Count() < 5)
            {
                ValidationMessage = "Invalid email";
                IsValid = false;
            }
            return this;
        }

        public PersonalAccountValidator ValidateFirstName(string firstName)
        {
            IsValid = true;
            ValidationMessage = string.Empty;
            if (string.IsNullOrEmpty(firstName))
            {
                ValidationMessage = "The first name must be filled in";
                IsValid = false;
                return this;
            }
            if (firstName.Any(letter => !char.IsLetter(letter)))
            {
                ValidationMessage = "The first name is not valid";
                IsValid = false;
            }
            if (firstName.Count() < 3)
            {
                ValidationMessage = "The first name must have at least 3 characters";
                IsValid = false;
            }

            return this;
        }

        public PersonalAccountValidator ValidateLastName(string lastName)
        {
            IsValid = true;
            ValidationMessage = string.Empty;
            if (string.IsNullOrEmpty(lastName))
            {
                ValidationMessage = "The last name must be filled in";
                IsValid = false;
                return this;
            }
            if (lastName.Any(letter => !char.IsLetter(letter)))
            {
                ValidationMessage = "The last name is not valid";
                IsValid = false;
            }
            if (lastName.Count() < 3)
            {
                ValidationMessage = "The last name must have at least 3 characters";
                IsValid = false;
            }

            return this;
        }
        public PersonalAccountValidator ValidatePassword(string password)
        {
            IsValid = true;
            ValidationMessage = string.Empty;

            if (String.IsNullOrEmpty(password))
            {
                ValidationMessage = "Password cannot be null";
                IsValid = false;
                return this;
            }
            if (password.Count() < 8)
            {
                ValidationMessage = "Password should have at least 8 characters";
                IsValid = false;
            }
            return this;
        }

        public PersonalAccountValidator ValidateUsername(string username)
        {
            ValidationMessage = string.Empty;
            IsValid = true;
            if (String.IsNullOrEmpty(username))
            {
                ValidationMessage = "Username cannot be null";
                IsValid = false;
                return this;
            }
            if (username.Count() < 3)
            {
                ValidationMessage = "Username must have at least 3 characters";
                IsValid = false;
            }
            if (username.Any(ch => ch == ' '))
            {
                ValidationMessage = "Username cannot contain white spaces";
                IsValid = false;
            }
            //if (_context.Users.Any(user => user.Username == username))
            //{
            //    ValidationMessage = "Username already exists. Please choose another username";
            //    IsValid = false;
            //}
            return this;
        }
    }
}
