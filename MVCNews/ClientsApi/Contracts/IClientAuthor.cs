using News.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace News.MVC.ClientsApi.Contracts
{
    public interface IClientAuthor
    {
        Task<HttpResponseMessage> GetAuthorsApi();

        Task<bool> CreateAuthorApi(AuthorData authorData);

        Task<bool> DeleteAuthorApi(Guid id);
    }
}
