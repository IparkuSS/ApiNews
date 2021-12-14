using System;
using System.Net.Http;
namespace MVCNews.Helper
{
    public class ApiModels : IApiModels
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            return client;
        }
    }
}
