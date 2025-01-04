using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Helpers;
using Business.Models;

namespace Business.Factories
{
    public static class ContactFactory
    {
        public static ContactModel CreateContact(
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string address,
            string city,
            string postalCode
        )
        {
            return new ContactModel
            {
                Id = GuidHelper.GenerateGuid(),
                FirstName = firstName,
                Email = email,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Address = address,
                City = city,
                PostalCode = postalCode,
            };
        }
    }
}
