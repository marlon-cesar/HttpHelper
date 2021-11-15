using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpHelper
{
    public interface IHttpHelper
    {
        Task<Response> Get<Response>(string endpoint, bool refreshToken = false);

        Task<Response> Post<Request, Response>(Request request, string endpoint, bool refreshToken = false);
        Task Post<Request>(Request request, string endpoint, bool refreshToken = false);

        Task<Response> Put<Request, Response>(Request request, string endpoint, bool refreshToken = false);
        Task Put<Request>(Request request, string endpoint, bool refreshToken = false);

        Task<Response> Delete<Request, Response>(Request request, string endpoint, bool refreshToken = false);
        Task<Response> Delete<Response>(string endpoint, bool refreshToken = false);
        Task Delete(string endpoint, bool refreshToken = false);

    }

}
