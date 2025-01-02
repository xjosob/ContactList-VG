using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using MauiApp1.ViewModels;
using Moq;

namespace MauiApp1.Tests
{
    public class MainViewModel_Tests
    {
        private readonly Mock<IContactService> _contactServiceMock;
        private readonly MainViewModel _viewModel;

        public MainViewModel_Tests()
        {
            _contactServiceMock = new Mock<IContactService>();
            _viewModel = new MainViewModel(_contactServiceMock.Object);
        }

        [Fact]
        public void UpdateContacts_ShouldPopulateContacts()
        {
            // Arrange
            var mockContacts = new List<ContactModel>
            {
                new()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                    Email = "john@hotmail.com",
                },
            };

            _contactServiceMock.Setup(service => service.GetAll()).Returns(mockContacts);

            _contactServiceMock.Invocations.Clear();

            // Act
            _viewModel.UpdateContacts();

            // Assert
            Assert.NotNull(_viewModel.Contacts);
            Assert.NotEmpty(_viewModel.Contacts);
            Assert.Equal(mockContacts, _viewModel.Contacts);
            Assert.Equal(mockContacts.Count, _viewModel.Contacts.Count);
            Assert.Equal(mockContacts[0].FirstName, _viewModel.Contacts[0].FirstName);
            Assert.Equal(mockContacts[0].LastName, _viewModel.Contacts[0].LastName);
            Assert.Equal(mockContacts[0].PhoneNumber, _viewModel.Contacts[0].PhoneNumber);
            Assert.Equal(mockContacts[0].Email, _viewModel.Contacts[0].Email);
            _contactServiceMock.Verify(service => service.GetAll(), Times.Once);
        }

        [Fact]
        public async Task DeleteContact_ShouldRemoveContact()
        {
            // Arrange
            var mockContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john@hotmail.com",
            };
            var mockContacts = new List<ContactModel> { mockContact };
            _contactServiceMock.Setup(service => service.GetAll()).Returns(mockContacts);
            _contactServiceMock.Setup(service => service.Delete(mockContact));
            _viewModel.UpdateContacts();

            _contactServiceMock.Invocations.Clear();

            // Act
            await _viewModel.DeleteContact(mockContact);

            //Assert
            _contactServiceMock.Verify(service => service.Delete(mockContact), Times.Once);
            Assert.Empty(_viewModel.Contacts);
            Assert.DoesNotContain(mockContact, _viewModel.Contacts);
        }
    }
}
