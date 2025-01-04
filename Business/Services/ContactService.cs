using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;

namespace Business.Services
{
    public class ContactService(IFileService fileService) : IContactService
    {
        private List<ContactModel> _contacts = [];
        private readonly IFileService _fileService = fileService;

        public void Add(ContactModel contact)
        {
            try
            {
                _contacts.Add(contact);
                _fileService.SaveListToFile(_contacts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(ContactModel contact)
        {
            try
            {
                if (!_contacts.Contains(contact))
                {
                    throw new InvalidOperationException("Contact not found");
                }
                _contacts.Remove(contact);
                _fileService.SaveListToFile(_contacts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<ContactModel> GetAll()
        {
            try
            {
                _contacts = _fileService.GetListFromFile();
                return _contacts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }

        public void Edit(ContactModel updatedContact)
        {
            try
            {
                var contact =
                    _contacts.FirstOrDefault(c => c.Id == updatedContact.Id)
                    ?? throw new InvalidOperationException("Contact not found");
                contact.FirstName = updatedContact.FirstName;
                contact.LastName = updatedContact.LastName;
                contact.Email = updatedContact.Email;
                contact.PhoneNumber = updatedContact.PhoneNumber;
                contact.Address = updatedContact.Address;
                contact.City = updatedContact.City;
                contact.PostalCode = updatedContact.PostalCode;

                _fileService.SaveListToFile(_contacts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
