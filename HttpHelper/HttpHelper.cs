using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace HttpHelper
{
    public class HttpHelper : IHttpHelper
    {

        const string TOKEN_CACHE_KEY = "TOKEN_CACHE";
        private ObjectCache cache = MemoryCache.Default;

        private AuthTypeEnum authType = AuthTypeEnum.Basic;

        public HttpHelper()
        {

        }

        public async Task<Response> Get<Response>(string endpoint, bool refreshToken = false)
        {
            using (HttpClient httpClient = GetClient(refreshToken))
            {
                var requestUri = GetUrl(endpoint);

                var httpResponseMessage = await httpClient.GetAsync(requestUri);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Response>(result);
                else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return default;
                else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    return await Get<Response>(endpoint, true);
                else
                    throw new Exception("API: Get error. URL: " + requestUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }

        public async Task<Response> Post<Request, Response>(Request request, string endpoint, bool refreshToken = false)
        {
            using (HttpClient httpClient = GetClient(refreshToken))
            {
                var requestUri = GetUrl(endpoint);

                var jsonToSend = JsonConvert.SerializeObject(request);
                var jsonContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                var httpResponseMessage = await httpClient.PostAsync(requestUri, jsonContent);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Response>(result);
                else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return default;
                else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    return await Post<Request, Response>(request, endpoint, true);
                else
                    throw new Exception("API: Post error. URL: " + requestUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }

        public async Task Post<Request>(Request request, string endpoint, bool refreshToken = false)
        {
            using (HttpClient httpClient = GetClient(refreshToken))
            {
                var requestUri = GetUrl(endpoint);

                var jsonToSend = JsonConvert.SerializeObject(request);
                var jsonContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                var httpResponseMessage = await httpClient.PostAsync(requestUri, jsonContent);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    await Post<Request>(request, endpoint, true);
                else
                    throw new Exception("API: Post error. URL: " + requestUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }
        public async Task<Response> Put<Request, Response>(Request request, string endpoint, bool refreshToken = false)
        {
            using (HttpClient httpClient = GetClient(refreshToken))
            {
                var requestUri = GetUrl(endpoint);

                var jsonToSend = JsonConvert.SerializeObject(request);
                var jsonContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                var httpResponseMessage = await httpClient.PutAsync(requestUri, jsonContent);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Response>(result);
                else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return default;
                else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    return await Put<Request, Response>(request, endpoint, true);
                else
                    throw new Exception("API: Put error. URL: " + requestUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }

        public async Task Put<Request>(Request request, string endpoint, bool refreshToken = false)
        {
            using (HttpClient httpClient = GetClient(refreshToken))
            {
                var requestUri = GetUrl(endpoint);

                var jsonToSend = JsonConvert.SerializeObject(request);
                var jsonContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                var httpResponseMessage = await httpClient.PutAsync(requestUri, jsonContent);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    await Put<Request>(request, endpoint, true);
                else
                    throw new Exception("API: Put error. URL: " + requestUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }

        public async Task<Response> Delete<Request, Response>(Request request, string endpoint, bool refreshToken = false)
        {
            using (HttpClient httpClient = GetClient(refreshToken))
            {
                var requestUri = GetUrl(endpoint);

                var jsonToSend = JsonConvert.SerializeObject(request);
                var jsonContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");

                var httpRequest = new HttpRequestMessage
                {
                    Content = jsonContent,
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(requestUri)
                };

                var httpResponseMessage = await httpClient.SendAsync(httpRequest);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Response>(result);
                else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return default;
                else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    return await Delete<Request, Response>(request, endpoint, true);
                else
                    throw new Exception("API: Delete error. URL: " + requestUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }


        public async Task<Response> Delete<Response>(string endpoint, bool refreshToken = false)
        {
            using (HttpClient httpClient = GetClient(refreshToken))
            {
                var requestUri = GetUrl(endpoint);

                var httpResponseMessage = await httpClient.DeleteAsync(requestUri);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Response>(result);
                else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return default;
                else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    return await Delete<Response>(endpoint, true);
                else
                    throw new Exception("API: Delete error. URL: " + requestUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }

        public async Task Delete(string endpoint, bool refreshToken = false)
        {
            using (HttpClient httpClient = GetClient(refreshToken))
            {
                var requestUri = GetUrl(endpoint);

                var httpResponseMessage = await httpClient.DeleteAsync(requestUri);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    await Delete(endpoint, true);
                else
                    throw new Exception("API: Delete error. URL: " + requestUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }

        private HttpClient GetClient(bool refreshToken)
        {
            HttpClient httpClient = new HttpClient();

            if (this.authType == AuthTypeEnum.Basic)
            {
                var authToken = Encoding.ASCII.GetBytes($"{Defaults.Username}:{Defaults.Password}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            }
            else
            {
                var bearer = (string)cache.Get(TOKEN_CACHE_KEY);

                if (bearer != null && !refreshToken)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
                else
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken().Result);
            }

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        private async Task<string> GetToken()
        {
            var tokenRequest = new TokenRequest(Defaults.Username, Defaults.Password);

            using (var httpClient = new HttpClient())
            {
                var authUri = $"{Defaults.BaseURL}Login";

                var jsonToSend = JsonConvert.SerializeObject(tokenRequest);
                var jsonContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                var httpResponseMessage = await httpClient.PostAsync(authUri, jsonContent);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<TokenResponse>(result);

                    cache.Add(TOKEN_CACHE_KEY, response.AccessToken, DateTime.Now.AddHours(1));

                    return response.AccessToken;
                }
                else
                    throw new Exception("API: Auth error. URL: " + authUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }

        private string GetUrl(string endpoint)
        {
            var url = string.Empty;

            url = $"{Defaults.BaseURL}{(endpoint.StartsWith("/") ? endpoint.Substring(1) : endpoint)}";

            return url;
        }

    }
}
