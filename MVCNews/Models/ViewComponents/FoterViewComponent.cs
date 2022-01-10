using Microsoft.AspNetCore.Mvc;
using News.MVC.Helper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
namespace News.MVC.Models.ViewComponents
{
    public class FoterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ApiModels _api = new ApiModels();
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
