using Business.Factories;
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
            // Arrange
            var mockFileService = new Mock<IFileService>();
            var contactService = new ContactService(mockFileService.Object);

            var contacts = new List<ContactModel>
            {
                ContactFactory.CreateContact(
                    "Bert",
                    "Johansson",
                    "bert.johansson@domain.com",
                    "0724624911",
                    "Testgatan",
                    "Teststad",
                    "70349"
                ),
            };

            // Act
            contactService.Add(contacts.First());

            // Assert
            mockFileService.Verify(
                fs =>
                    fs.SaveListToFile(
                        It.Is<List<ContactModel>>(List =>
                            List.Count == contacts.Count && List.First().FirstName == "Bert"
                        )
                    ),
                Times.Once
            );
        }

        [Fact]
        public void GetListFromFile_ShouldReturnListFromJsonFile()
        {
            // Arrange
            var mockFileService = new Mock<IFileService>();
            var contactService = new ContactService(mockFileService.Object);

            var contacts = new List<ContactModel>
            {
                ContactFactory.CreateContact(
                    "Bert",
                    "Johansson",
                    "bert.johansson@domain.com",
                    "0724624911",
                    "Testgatan",
                    "Teststad",
                    "70349"
                ),
            };

            mockFileService.Setup(fs => fs.GetListFromFile()).Returns(contacts);

            // Act
            var result = contactService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contacts.Count, result.Count());
            Assert.Contains(result, c => c.FirstName == "Bert" && c.LastName == "Johansson");
            mockFileService.Verify(fs => fs.GetListFromFile(), Times.Once);
        }
    }
}
