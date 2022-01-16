using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using News.MVC.Helper.Contracts;
using News.MVC.Models;
using News.MVC.Services.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace News.MVC.Services
{
    public class AuthorSerives : IAuthorSerives
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IClientAuthor _clientAuthor;
        public AuthorSerives(IWebHostEnvironment hostingEnvironment, IClientAuthor clientAuthor)
        {
            this.hostingEnvironment = hostingEnvironment;
            _clientAuthor = clientAuthor;
        }
        public async Task<bool> CreateAuthor(AuthorData authorData, IFormFile PdfFile)
        {
            if (PdfFile != null)
            {
                authorData.Document = PdfFile.FileName;
                using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "Doc/", PdfFile.FileName), FileMode.Create))
                {
                    PdfFile.CopyTo(stream);
                }
            }
            else return false;
            var res = await _clientAuthor.CreateAuthorApi(authorData);
            if (res == true) return true;
            return false;
        }
        public async Task<bool> DeleteAuthor(Guid id)
        {
            var res = await _clientAuthor.DeleteAuthorApi(id);
            if (res == true) return true;
            return false;
        }
        public async Task<IEnumerable<AuthorData>> GetAuthors()
        {
            var author = new List<AuthorData>();
            var res = await _clientAuthor.GetAuthorsApi();
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<List<AuthorData>>(result);
            }
            return author;
        }
    }
}
