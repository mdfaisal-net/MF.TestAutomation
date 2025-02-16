using MF.Core.API.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Core.API.Framework.Models
{
    public class Request
    {
        public required string Url { get; set; }
        public string Endpoint { get; set; }
        public HttpMethodType Method { get; set; }
        public string ContentType { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public IDictionary<string, string> QueryParameters { get; set; }
        public object Body { get; set; }
    }

}
