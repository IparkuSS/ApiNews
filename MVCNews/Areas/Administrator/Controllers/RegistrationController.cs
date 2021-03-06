using Microsoft.AspNetCore.Mvc;
using News.MVC.ClientsApi.Contracts;
using News.MVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
namespace News.MVC.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class RegistrationController : Controller
    {
        private readonly IApiModels _api;
        public RegistrationController(IApiModels api)
        {
            _api = api;
        }
        public IActionResult AddUsers()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUsers(UserForRigestration userForRigestration)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _api.Initial();
                var postTask = client.PostAsJsonAsync("api/registration", userForRigestration);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return View();
                }
                ModelState.AddModelError(nameof(UserForAuthentication.UserName), "incorrect input");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
