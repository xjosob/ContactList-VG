using Business.Models;

namespace Business.Interfaces
{
    public interface IContactService
    {
        void Add(ContactModel contact);

        void Delete(ContactModel contact);

        void Edit(ContactModel updatedContact);
        IEnumerable<ContactModel> GetAll();
    }
}
