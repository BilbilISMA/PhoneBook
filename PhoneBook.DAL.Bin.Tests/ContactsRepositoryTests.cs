using System;
using System.IO;
using System.Linq;
using AGrynCo.Lib.ResourcesUtils;
using Moq;
using NUnit.Framework;
using PhoneBook.Domain;

namespace PhoneBook.DAL.Bin.Tests
{
    [TestFixture]
    public class ContactsRepositoryTests
    {
        private Stream GetStream()
        {
            return ResourceReader.GetStream(GetType(),
                "PhoneBook.DAL.Bin.Tests.contacts.bin");
        }

        [Test]
        public void Add()
        {
            //Arrange
            var streamCreator = new Mock<IContactsStreamCreator>();
            streamCreator.Setup(creator => creator.OpenForRead()).Returns(new MemoryStream());
            var contactsRepository = new ContactsRepository(streamCreator.Object);

            //Act
            contactsRepository.Add(new Contact());

            //Asserts
            Assert.AreEqual(1, contactsRepository.GetCount());
        }

        [Test]
        public void CreateWitData()
        {
            //Arrange
            var streamCreator = new Mock<IContactsStreamCreator>();
            streamCreator.Setup(creator => creator.OpenForRead()).Returns(GetStream);

            //Act
            var contactsRepository = new ContactsRepository(streamCreator.Object);

            //Asserts
            Assert.AreEqual(2, contactsRepository.GetCount());
        }

        [Test]
        public void CreateWithNoData()
        {
            //Arrange
            var streamCreator = new Mock<IContactsStreamCreator>();
            streamCreator.Setup(creator => creator.OpenForRead()).Returns(new MemoryStream());

            //Act
            var contactsRepository = new ContactsRepository(streamCreator.Object);


            //Asserts
            Assert.AreEqual(0, contactsRepository.GetCount());
        }

        [Test]
        public void Dispose()
        {
            //Arrange
            var streamCreator = new Mock<IContactsStreamCreator>();
            streamCreator.Setup(creator => creator.OpenForRead()).Returns(GetStream);

            MemoryStream stream = new MemoryStream();

            streamCreator.Setup(creator => creator.Create()).Returns(stream);

            //Act
            using (new ContactsRepository(streamCreator.Object))
            {
            }

            //Asserts
        }

        [Test]
        public void GetAll()
        {
            //Arrange
            var streamCreator = new Mock<IContactsStreamCreator>();
            streamCreator.Setup(creator => creator.OpenForRead()).Returns(GetStream);
            var contactsRepository = new ContactsRepository(streamCreator.Object);

            //Act
            Contact[] contacts = contactsRepository.GetAll();

            //Asserts
            Assert.AreEqual(2, contacts.Length);
        }

        [Test]
        public void Remove()
        {
            //Arrange
            var streamCreator = new Mock<IContactsStreamCreator>();
            streamCreator.Setup(creator => creator.OpenForRead()).Returns(GetStream());
            var contactsRepository = new ContactsRepository(streamCreator.Object);

            //Act
            contactsRepository.Remove(Guid.Parse("{a7b531c7-dc5e-4bd2-b794-adf66ef83fd2}"));

            //Asserts
            Assert.AreEqual(1, contactsRepository.GetCount());
        }

        [Test]
        public void Search()
        {
            //Arrange
            var streamCreator = new Mock<IContactsStreamCreator>();
            streamCreator.Setup(creator => creator.OpenForRead()).Returns(GetStream);
            var contactsRepository = new ContactsRepository(streamCreator.Object);

            //Act
            Contact[] contacts = contactsRepository.Search("ana");

            //Asserts
            Assert.AreEqual("Anatolii", contacts.Single().FirstName);
        }
    }
}