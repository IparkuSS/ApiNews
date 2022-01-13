using System.Net.Http;
namespace News.MVC.Helper //chang namespace 
{
    public interface IApiModels
    {
        public HttpClient Initial();
    }
}
