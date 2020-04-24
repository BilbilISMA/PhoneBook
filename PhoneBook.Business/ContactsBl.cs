using System;
using System.Web.Http;
using PhoneBook.DAL.Abstract;
using PhoneBook.Domain;

namespace PhoneBook.Business
{
    public class ContactsBl
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsBl(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        public void Add(Contact contact)
        {
            _contactsRepository.Add(contact);
        }

        public Contact[] GetAll()
        {
            return _contactsRepository.GetAll();
        }

        public void Delete(Guid id)
        {
            _contactsRepository.Remove(id);
        }

        [HttpGet]
        public Contact[] Search(string searchPattern)
        {
            return _contactsRepository.Search(searchPattern);
        }
    }
}