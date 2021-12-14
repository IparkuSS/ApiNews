using System.Net.Http;
namespace MVCNews.Helper
{
    public interface IApiModels
    {
        HttpClient Initial();
    }
}
