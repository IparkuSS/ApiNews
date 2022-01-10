using System.Net.Http;
namespace News.MVC.Helper
{
    public interface IApiModels
    {
        HttpClient Initial();
    }
}
