using System;
using System.Web.Http;
using PhoneBook.Business;
using PhoneBook.Domain;

namespace PhoneBook.Web.API.Controllers
{
    [RoutePrefix("contacts")]
    public class ContactsController : ApiController
    {
        private readonly ContactsBl _contactsBl;

        public ContactsController(ContactsBl contactsBl)
        {
            _contactsBl = contactsBl;
        }

        public Contact[] Get()
        {
            return _contactsBl.GetAll();
        }

        public void Post(Contact contact)
        {
            _contactsBl.Add(contact);
        }

        public void Delete(Guid id)
        {
            _contactsBl.Delete(id);
        }

        [HttpGet]
        [Route("{searchPattern}")]
        public Contact[] Search(string searchPattern)
        {
            return _contactsBl.Search(searchPattern);
        }
    }
}