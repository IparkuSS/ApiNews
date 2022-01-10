using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.MVC.Helper;
using News.MVC.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace News.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IApiModels _api;
        public AccountController(IApiModels api)
        {
            _api = api;
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new UserForAuthentication());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserForAuthentication model)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _api.Initial();
                var postTask = await client.PostAsJsonAsync("api/authentication", model);
                if (postTask.IsSuccessStatusCode)
                {
                    var resultMessage = postTask.Content.ReadAsStringAsync().Result;
                    JWT jwt = JsonConvert.DeserializeObject<JWT>(resultMessage);
                    HttpContext.Session.SetString("Token", jwt.Token);
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                ModelState.AddModelError(nameof(UserForAuthentication.UserName), "Неверный логин или пароль");
            }
            return View(model);
        }
    }
}
