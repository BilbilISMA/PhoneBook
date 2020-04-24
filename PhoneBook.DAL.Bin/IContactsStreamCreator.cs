using System.IO;

namespace PhoneBook.DAL.Bin
{
    public interface IContactsStreamCreator
    {
        Stream Create();
        Stream OpenForRead();
    }
}