using Microsoft.AspNetCore.Mvc;
using News.MVC.ClientsApi.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
namespace News.MVC.Models.ViewComponents
{
    public class FoterViewComponent : ViewComponent
    {
        private readonly IApiModels _api;

        public FoterViewComponent(IApiModels api)
        {
            _api = api;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<SectionData> apiModels = new List<SectionData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/section");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                apiModels = JsonConvert.DeserializeObject<List<SectionData>>(result);
            }
            return View(apiModels);
        }
    }
}
