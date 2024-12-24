using Business.Models;

namespace Business.Interfaces
{
    public interface IContactService
    {
        void Add(ContactModel contact);
        IEnumerable<ContactModel> GetAll();
    }
}
