using System;
using PhoneBook.Domain;

namespace PhoneBook.DAL.Abstract
{
    public interface IContactsRepository
    {
        void Add(Contact contact);
        Contact[] GetAll();

        int GetCount();

        void Remove(Guid id);

        /// <summary>
        /// Will search by <see cref="Contact.FirstName"/> or <see cref="Contact.LastName"/>
        /// </summary>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        Contact[] Search(string searchPattern);
    }
}