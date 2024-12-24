using System;
using System.Collections.Generic;
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
    }
}
