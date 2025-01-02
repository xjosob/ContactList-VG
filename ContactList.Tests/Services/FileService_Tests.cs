using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace ContactList.Tests.Services
{
    public class FileService_Tests
    {
        [Fact]
        public void SaveListToFile_ShouldSaveListToJsonFile()
        {
            // arrange
            var mockFileService = new Mock<IFileService>();
            var contactService = new ContactService(mockFileService.Object);

            var contacts = new List<ContactModel>
            {
                new()
                {
                    Id = GuidHelper.GenerateGuid(),
                    FirstName = "Bert",
                    LastName = "Johansson",
                    Email = "bert.johansson@domain.com",
                    PhoneNumber = "072-462 49 11",
                },
            };

            // act
            contactService.Add(contacts.First());

            // assert
            mockFileService.Verify(
                fs => fs.SaveListToFile(It.IsAny<List<ContactModel>>()),
                Times.Once
            );
        }

        [Fact]
        public void GetListFromFile_ShouldReturnListFromJsonFile()
        {
            // arrange
            var mockFileService = new Mock<IFileService>();
            var contactService = new ContactService(mockFileService.Object);

            var contacts = new List<ContactModel>
            {
                new ContactModel
                {
                    Id = GuidHelper.GenerateGuid(),
                    FirstName = "Bert",
                    LastName = "Johansson",
                    Email = "bert.johansson@domain.com",
                    PhoneNumber = "072-462 49 11",
                },
            };

            mockFileService.Setup(fs => fs.GetListFromFile()).Returns(contacts);

            // act
            var result = contactService.GetAll();

            // assert
            Assert.NotNull(result);
            Assert.Equal(contacts.Count, result.Count());
            Assert.Contains(result, c => c.FirstName == "Bert" && c.LastName == "Johansson");
            mockFileService.Verify(fs => fs.GetListFromFile(), Times.Once);
        }
    }
}
