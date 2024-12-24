using Business.Models;

namespace Business.Interfaces
{
    public interface IFileService
    {
        List<ContactModel> GetListFromFile();
        void SaveListToFile(List<ContactModel> list);
    }
}
