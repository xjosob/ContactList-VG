using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Helpers;

namespace ContactList.Tests.Helpers
{
    public class GuidHelper_Tests
    {
        [Fact]
        public void GenerateGuid_ShouldReturnGuidAsString()
        {
            // act
            var result = GuidHelper.GenerateGuid();

            // assert
            Assert.NotNull(result);
            Assert.True(Guid.TryParse(result, out _));
        }
    }
}
