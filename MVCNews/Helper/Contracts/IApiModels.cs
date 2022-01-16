using System.Net.Http;
namespace News.MVC.Helper.Contracts
{
    public interface IApiModels
    {
        public HttpClient Initial();
    }
}
