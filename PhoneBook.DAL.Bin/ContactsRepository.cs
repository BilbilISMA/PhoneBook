using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using PhoneBook.DAL.Abstract;
using PhoneBook.Domain;

namespace PhoneBook.DAL.Bin
{
    public class ContactsRepository : IContactsRepository, IDisposable
    {
        private readonly ConcurrentDictionary<Guid, Contact> _contacts;
        private readonly IContactsStreamCreator _contactsStreamCreator;


        public ContactsRepository(IContactsStreamCreator contactsStreamCreator)
        {
            _contactsStreamCreator = contactsStreamCreator;

            _contacts = new ConcurrentDictionary<Guid, Contact>();

            Load();
        }

        public void Add(Contact contact)
        {
            contact.Id = Guid.NewGuid();
            var test = _contacts.TryAdd(contact.Id, contact);
        }

        public Contact[] GetAll()
        {
            var test = _contacts.Values.ToArray();
            return _contacts.Values.ToArray();
        }

        public int GetCount()
        {
            var test = _contacts.Count;
            return _contacts.Count;
        }

        public Contact[] Search(string searchPattern)
        {
            return _contacts.Values.Where(contact => contact.FirstName.ToLower().Contains(searchPattern) ||
                                                     contact.FirstName.ToLower().Contains(searchPattern)).ToArray();
        }

        public void Remove(Guid id)
        {
            Contact removedContact;
            _contacts.TryRemove(id, out removedContact);
        }

        public void Dispose()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream stream = _contactsStreamCreator.Create())
            {
                formatter.Serialize(stream, GetAll());
            }
        }

        private void Load()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream stream = _contactsStreamCreator.OpenForRead())
            {
                if (stream.Length == 0)
                {
                    return;
                }

                var contacts = (Contact[]) formatter.Deserialize(stream);
                foreach (Contact contact in contacts)
                {
                    _contacts.TryAdd(contact.Id, contact);
                }
            }
        }
    }
}