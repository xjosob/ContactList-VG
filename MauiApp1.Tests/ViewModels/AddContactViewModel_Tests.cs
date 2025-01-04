using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using MauiApp1.Interfaces;
using MauiApp1.ViewModels;
using Moq;

namespace MauiApp1.Tests.ViewModels
{
    public class AddContactViewModel_Tests
    {
        private readonly Mock<IContactService> _contactServiceMock;
        private readonly AddContactViewModel _viewModel;
        private readonly Mock<IAlertService> _alertServiceMock;

        public AddContactViewModel_Tests()
        {
            _contactServiceMock = new Mock<IContactService>();
            _alertServiceMock = new Mock<IAlertService>();
            _viewModel = new AddContactViewModel(
                _contactServiceMock.Object,
                _alertServiceMock.Object
            );
        }

        [Fact]
        public async Task AddContact_WithValidContact_ShouldAddContact()
        {
            // Arrange
            var mockContact = ContactFactory.CreateContact(
                firstName: "John",
                lastName: "Doe",
                phoneNumber: "0334933744",
                email: "john@hotmail.com",
                address: "Testgatan",
                city: "TestStad",
                postalCode: "83043"
            );

            _contactServiceMock.Setup(service => service.Add(It.IsAny<ContactModel>()));

            _alertServiceMock
                .Setup(service =>
                    service.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
                )
                .Returns(Task.CompletedTask);

            // Act
            _viewModel.Contact = mockContact;

            await _viewModel.AddContact();

            // Assert
            _contactServiceMock.Verify(
                service =>
                    service.Add(
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
        public async Task AddContact_WithInvalidEmail_ShouldDisplayInvalidEmailAlert()
        {
            // Arrange
            var mockContact = ContactFactory.CreateContact(
                firstName: "John",
                lastName: "Doe",
                phoneNumber: "0334933744",
                email: "fsadadas", // Invalid email
                address: "Testgatan",
                city: "TestStad",
                postalCode: "83043"
            );
            _contactServiceMock.Setup(service => service.Add(It.IsAny<ContactModel>()));
            _alertServiceMock.Setup(service =>
                service.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
            );

            // Act
            _viewModel.Contact = mockContact;
            await _viewModel.AddContact();

            // Assert
            _contactServiceMock.Verify(
                service => service.Add(It.IsAny<ContactModel>()),
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

            _alertServiceMock.Verify(
                service =>
                    service.DisplayAlert(
                        "Invalid phone number",
                        "Please enter a valid phone number",
                        "OK"
                    ),
                Times.Never
            );
        }

        [Fact]
        public async Task AddContact_WithInvalidPhoneNumber_ShouldDisplayInvalidPhoneNumberAlert()
        {
            // Arrange
            var mockContact = ContactFactory.CreateContact(
                firstName: "John",
                lastName: "Doe",
                phoneNumber: "dasdas", // Invalid phone number
                email: "john@hotmail.com",
                address: "Testgatan",
                city: "TestStad",
                postalCode: "83043"
            );
            _contactServiceMock.Setup(service => service.Add(It.IsAny<ContactModel>()));
            _alertServiceMock.Setup(service =>
                service.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
            );

            // Act
            _viewModel.Contact = mockContact;
            await _viewModel.AddContact();

            // Assert
            _contactServiceMock.Verify(
                service => service.Add(It.IsAny<ContactModel>()),
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

            _alertServiceMock.Verify(
                service =>
                    service.DisplayAlert(
                        "Invalid email",
                        "Please enter a valid email address",
                        "OK"
                    ),
                Times.Never
            );
        }

        [Fact]
        public async Task AddContact_WithInvalidPostalCode_ShouldDisplayInvalidPostalCodeAlert()
        {
            // Arrange
            var mockContact = ContactFactory.CreateContact(
                firstName: "John",
                lastName: "Doe",
                phoneNumber: "0334933744",
                email: "john@hotmail.com",
                address: "Testgatan",
                city: "TestStad",
                postalCode: "dasdasdas" // Invalid postal code
            );
            _contactServiceMock.Setup(service => service.Add(It.IsAny<ContactModel>()));
            _alertServiceMock.Setup(service =>
                service.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
            );

            // Act
            _viewModel.Contact = mockContact;
            await _viewModel.AddContact();

            // Assert
            _contactServiceMock.Verify(
                service => service.Add(It.IsAny<ContactModel>()),
                Times.Never
            );

            _alertServiceMock.Verify(
                service =>
                    service.DisplayAlert(
                        "Invalid postal code",
                        "Please enter a valid postal code",
                        "OK"
                    ),
                Times.Once
            );

            _alertServiceMock.Verify(
                service =>
                    service.DisplayAlert(
                        "Invalid email",
                        "Please enter a valid email address",
                        "OK"
                    ),
                Times.Never
            );

            _alertServiceMock.Verify(
                service =>
                    service.DisplayAlert(
                        "Invalid phone number",
                        "Please enter a valid phone number",
                        "OK"
                    ),
                Times.Never
            );
        }
    }
}
