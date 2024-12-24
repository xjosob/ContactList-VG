using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public static class GuidHelper
    {
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
