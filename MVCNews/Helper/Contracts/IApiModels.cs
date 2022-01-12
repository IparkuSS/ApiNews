using System.Net.Http;
namespace News.MVC.Helper //chang namespace 
{
    public interface IApiModels
    {
        HttpClient Initial();
    }
}
