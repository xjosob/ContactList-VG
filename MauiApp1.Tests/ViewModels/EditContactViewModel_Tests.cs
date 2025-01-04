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
                PhoneNumber = "0334933744",
                Email = "john@hotmail.com",
                Address = "123 Main St",
                City = "Toronto",
                PostalCode = "M1M1M1",
            };

            var mockQuery = new Dictionary<string, object> { { "Contact", mockContact } };

            _viewModel.ApplyQueryAttributes(mockQuery);
            _contactServiceMock.Setup(service => service.Edit(It.IsAny<ContactModel>()));

            // Act
            await _viewModel.EditContact();

            // Assert
            Assert.NotNull(_viewModel.Contact);
            Assert.Equal(mockContact.FirstName, _viewModel.Contact.FirstName);
            Assert.Equal(mockContact.LastName, _viewModel.Contact.LastName);
            Assert.Equal(mockContact.PhoneNumber, _viewModel.Contact.PhoneNumber);
            Assert.Equal(mockContact.Email, _viewModel.Contact.Email);
            Assert.Equal(mockContact.Address, _viewModel.Contact.Address);
            Assert.Equal(mockContact.City, _viewModel.Contact.City);
            Assert.Equal(mockContact.PostalCode, _viewModel.Contact.PostalCode);

            _contactServiceMock.Verify(
                service =>
                    service.Edit(
                        It.Is<ContactModel>(c =>
                            c.FirstName == mockContact.FirstName
                            && c.LastName == mockContact.LastName
                            && c.PhoneNumber == mockContact.PhoneNumber
                            && c.Email == mockContact.Email
                            && c.Address == mockContact.Address
                            && c.City == mockContact.City
                            && c.PostalCode == mockContact.PostalCode
                        )
                    ),
                Times.Once
            );

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

        [Fact]
        public async Task EditContact_WithError_ShouldDisplayAlert()
        {
            // Arrange
            var mockContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "0334933744",
                Email = "john@hotmail.com",
                Address = "123 Main St",
                City = "Toronto",
                PostalCode = "M1M1M1",
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
                service => service.DisplayAlert("Error", "An error occurred: Test exception", "OK"),
                Times.Once
            );
        }

        [Fact]
        public async Task EditContact_WithMissingInformation_ShouldDisplayAlert()
        {
            // Arrange
            var mockContact = new ContactModel
            {
                FirstName = "",
                LastName = "Doe",
                PhoneNumber = "0334933744",
                Email = "john@hotmail.com",
                Address = "123 Main St",
                City = "Toronto",
                PostalCode = "M1M1M1",
            };
            var mockQuery = new Dictionary<string, object> { { "Contact", mockContact } };
            _viewModel.ApplyQueryAttributes(mockQuery);

            // Act
            await _viewModel.EditContact();

            // Assert
            _contactServiceMock.Verify(
                service => service.Edit(It.IsAny<ContactModel>()),
                Times.Never
            );
            _alertServiceMock.Verify(
                service =>
                    service.DisplayAlert("Missing information", "Please fill in all fields", "OK"),
                Times.Once
            );
        }

        [Fact]
        public async Task EditContact_WithInvalidEmail_ShouldDisplayAlert()
        {
            // Arrange
            var mockContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "0334933744",
                Email = "johnhotmail.com", // missing @
                Address = "123 Main St",
                City = "Toronto",
                PostalCode = "M1M1M1",
            };
            var mockQuery = new Dictionary<string, object> { { "Contact", mockContact } };

            _viewModel.ApplyQueryAttributes(mockQuery);

            // Act
            await _viewModel.EditContact();

            // Assert
            _contactServiceMock.Verify(
                service => service.Edit(It.IsAny<ContactModel>()),
                Times.Never
            );
            _alertServiceMock.Verify(
                service =>
                    service.DisplayAlert(
                        "Invalid email",
                        "Please enter a valid email address",
                        "OK"
                    ),
                Times.Once
            );
        }

        [Fact]
        public async Task EditContact_WithInvalidPhoneNumber_ShouldDisplayAlert()
        {
            // Arrange
            var mockContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "dasdasd", // invalid phone number
                Email = "john@hotmail.com",
                Address = "123 Main St",
                City = "Toronto",
                PostalCode = "M1M1M1",
            };

            var mockQuery = new Dictionary<string, object> { { "Contact", mockContact } };

            _viewModel.ApplyQueryAttributes(mockQuery);

            // Act
            await _viewModel.EditContact();

            // Assert
            _contactServiceMock.Verify(
                service => service.Edit(It.IsAny<ContactModel>()),
                Times.Never
            );
            _alertServiceMock.Verify(
                service =>
                    service.DisplayAlert(
                        "Invalid phone number",
                        "Please enter a valid phone number",
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
            Assert.Equal(mockContact.Address, _viewModel.Contact.Address);
            Assert.Equal(mockContact.City, _viewModel.Contact.City);
            Assert.Equal(mockContact.PostalCode, _viewModel.Contact.PostalCode);
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
