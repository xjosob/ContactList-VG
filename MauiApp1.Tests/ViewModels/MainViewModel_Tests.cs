﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using MauiApp1.Interfaces;
using MauiApp1.ViewModels;
using Moq;

namespace MauiApp1.Tests.ViewModels
{
    public class MainViewModel_Tests
    {
        private readonly Mock<IContactService> _contactServiceMock;
        private readonly MainViewModel _viewModel;
        private readonly Mock<IAlertService> _alertServiceMock;

        public MainViewModel_Tests()
        {
            _contactServiceMock = new Mock<IContactService>();
            _alertServiceMock = new Mock<IAlertService>();
            _viewModel = new MainViewModel(_contactServiceMock.Object, _alertServiceMock.Object);
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

            #region Mock Setup written by ChatGPT
            // 'GetAll' dynamically fetches the list pf contacts except the one to be deleted.
            // The "Returns" method uses a lambda expression to filter out "mockContact" from the list.
            _contactServiceMock
                .Setup(service => service.GetAll())
                .Returns(() => mockContacts.Where(c => c != mockContact).ToList());

            // 'Delete' removes the contact from the list by removing it from 'mockContacts'.
            // The 'Callback' method is triggered when 'Delete' is called, removing the contact from the list.
            _contactServiceMock
                .Setup(service => service.Delete(It.IsAny<ContactModel>()))
                .Callback<ContactModel>(contact => mockContacts.Remove(contact));

            // Mock for 'DisplayAlert': Ensures no alert is displayed during this test execution.
            // This also prevents unwanted errors caused by UI interactions in tests.
            // 'It.IsAny<string>()' matches any string argument passed to 'DisplayAlert'.
            _alertServiceMock
                .Setup(service =>
                    service.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
                )
                .Returns(Task.CompletedTask);
            #endregion

            _viewModel.UpdateContacts();

            // Act
            await _viewModel.DeleteContact(mockContact);

            //Assert
            _contactServiceMock.Verify(
                service =>
                    service.Delete(
                        It.Is<ContactModel>(c => c.FirstName == "John" && c.LastName == "Doe")
                    ),
                Times.Once
            );
            Assert.Empty(_viewModel.Contacts);
            Assert.DoesNotContain(mockContact, _viewModel.Contacts);
            _alertServiceMock.Verify(
                service =>
                    service.DisplayAlert(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()
                    ),
                Times.Never
            );
        }
    }
}
