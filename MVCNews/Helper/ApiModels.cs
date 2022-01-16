using News.MVC.Helper.Contracts;
using System;
using System.Net.Http;
namespace News.MVC.Helper
{
    public class ApiModels : IApiModels
    {
        private readonly ClientConfig _clientConfig;

        public ApiModels(ClientConfig clientConfig)
        {
            _clientConfig = clientConfig;
        }

        public HttpClient Initial()
        {
            string uriApi = _clientConfig.BaseAddress;
            var client = new HttpClient();
            client.BaseAddress = new Uri(uriApi);
            return client;
        }
    }
}
