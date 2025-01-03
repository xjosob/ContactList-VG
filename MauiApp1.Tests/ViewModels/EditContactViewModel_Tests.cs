using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using MauiApp1.Interfaces;
using MauiApp1.ViewModels;
using Moq;

namespace MauiApp1.Tests.ViewModels
{
    public class EditContactViewModel_Tests
    {
        private readonly Mock<IContactService> _contactServiceMock;
        private readonly Mock<IAlertService> _alertServiceMock;
        private readonly EditContactViewModel _viewModel;

        public EditContactViewModel_Tests()
        {
            _contactServiceMock = new Mock<IContactService>();
            _alertServiceMock = new Mock<IAlertService>();
            _viewModel = new EditContactViewModel(
                _contactServiceMock.Object,
                _alertServiceMock.Object
            );
        }

        [Fact]
        public async Task EditContact_ShouldEditContact()
        {
            // Arrange
            var mockContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john@hotmail.com",
            };

            var mockQuery = new Dictionary<string, object> { { "Contact", mockContact } };

            _viewModel.ApplyQueryAttributes(mockQuery);
            _contactServiceMock.Setup(service => service.Edit(mockContact));

            // Act
            await _viewModel.EditContact();

            // Assert
            Assert.NotNull(_viewModel.Contact);
            Assert.Equal(mockContact.FirstName, _viewModel.Contact.FirstName);
            Assert.Equal(mockContact.LastName, _viewModel.Contact.LastName);
            Assert.Equal(mockContact.PhoneNumber, _viewModel.Contact.PhoneNumber);
            Assert.Equal(mockContact.Email, _viewModel.Contact.Email);

            _contactServiceMock.Verify(
                service =>
                    service.Edit(
                        It.Is<ContactModel>(c =>
                            c.FirstName == mockContact.FirstName
                            && c.LastName == mockContact.LastName
                            && c.PhoneNumber == mockContact.PhoneNumber
                            && c.Email == mockContact.Email
                        )
                    ),
                Times.Once
            );
        }

        [Fact]
        public async Task EditContact_ShouldShowErrorOnException()
        {
            // Arrange
            var mockContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john@hotmail.com",
            };

            var mockQuery = new Dictionary<string, object> { { "Contact", mockContact } };

            _viewModel.ApplyQueryAttributes(mockQuery);
            _contactServiceMock
                .Setup(service => service.Edit(It.IsAny<ContactModel>()))
                .Throws(new Exception("Test exception"));

            _alertServiceMock
                .Setup(service =>
                    service.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
                )
                .Returns(Task.CompletedTask);

            // Act
            await _viewModel.EditContact();

            // Assert
            _alertServiceMock.Verify(
                service =>
                    service.DisplayAlert(
                        "Error",
                        It.Is<string>(msg => msg.Contains("An error occurred: Test exception")),
                        "OK"
                    ),
                Times.Once
            );
        }

        [Fact]
        public void ApplyQueryAttributes_ShouldUpdateContact()
        {
            // Arrange
            var mockContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john@hotmail.com",
            };
            var mockQuery = new Dictionary<string, object> { { "Contact", mockContact } };

            // Act
            _viewModel.ApplyQueryAttributes(mockQuery);

            // Assert
            Assert.NotNull(_viewModel.Contact);
            Assert.Equal(mockContact.FirstName, _viewModel.Contact.FirstName);
            Assert.Equal(mockContact.LastName, _viewModel.Contact.LastName);
            Assert.Equal(mockContact.PhoneNumber, _viewModel.Contact.PhoneNumber);
            Assert.Equal(mockContact.Email, _viewModel.Contact.Email);
        }

        [Fact]
        public void ApplyQueryAttributes_ShouldNotUpdateContact_WhenInvalidProvided()
        {
            // Arrange
            var mockQuery = new Dictionary<string, object> { { "Contact", null! } };
            // Act
            _viewModel.ApplyQueryAttributes(mockQuery);

            // Assert
            Assert.NotNull(_viewModel.Contact);
            Assert.Null(_viewModel.Contact.FirstName);
            Assert.Null(_viewModel.Contact.LastName);
        }
    }
}
