using System.Net.Http;
namespace News.MVC.ClientsApi.Contracts
{
    public interface IApiModels
    {
        public HttpClient Initial();
    }
}
