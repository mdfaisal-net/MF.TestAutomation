using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.TestAutomation.Test.Utilities
{
    public static class ApiHelper
    {
        public static IDictionary<string, string> GetDefaultHeaders()
        {
            return new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Accept", "application/json" }
            };
        }
    }
}
