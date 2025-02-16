using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Core.API.Framework.Models
{
    public class Response
    {
        public int StatusCode { get; }
        public string Content { get; }
        public Response(string content, int statusCode)
        {
            Content = content;
            StatusCode = statusCode;
        }
    }
}
