using MF.Core.API.Framework.Enums;
using MF.Core.API.Framework.Models;

namespace MF.Core.API.Framework.Interfaces
{
    public interface IApiClient
    {
        Task<Response> SendAsyc(Request request);
    }
}
