using System;
using System.Net.Http;
namespace MVCNews.Helper
{
    public class ApiModels : IApiModels
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            // Todo: move to appsettings
            client.BaseAddress = new Uri("http://localhost:5000/");
            return client;
        }
    }
}
