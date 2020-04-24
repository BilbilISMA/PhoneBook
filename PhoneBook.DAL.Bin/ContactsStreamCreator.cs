using System.IO;

namespace PhoneBook.DAL.Bin
{
    public class ContactsStreamCreator : IContactsStreamCreator
    {
        private readonly string _fileName;

        public ContactsStreamCreator(string fileName)
        {
            _fileName = fileName;
        }

        public Stream OpenForRead()
        {
            return new FileStream(_fileName, FileMode.Open);
        }

        public Stream Create()
        {
            return new FileStream(_fileName, FileMode.Create);
        }
    }
}