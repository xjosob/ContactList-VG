using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Factories;

namespace ContactList.Tests.Factories
{
    public class ContactFactory_Tests
    {
        [Fact]
        public void Create_ShouldReturnContact()
        {
            // act
            var result = ContactFactory.CreateContact(
                "Bert",
                "Johansson",
                "bert.johansson@domain.com",
                "072-462 49 11",
                "Testgatan 1",
                "Teststad",
                "123 45"
            );

            //assert
            Assert.NotNull(result);
            Assert.Equal("Bert", result.FirstName);
            Assert.Equal("Johansson", result.LastName);
            Assert.Equal("bert.johansson@domain.com", result.Email);
            Assert.Equal("072-462 49 11", result.PhoneNumber);
            Assert.Equal("Testgatan 1", result.Address);
            Assert.Equal("Teststad", result.City);
            Assert.Equal("123 45", result.PostalCode);
        }
    }
}
