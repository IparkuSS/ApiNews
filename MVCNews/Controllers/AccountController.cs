using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCNews.Helper;
using MVCNews.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MVCNews.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        ApiModels _api = new ApiModels();
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new UserForAuthentication());
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserForAuthentication model)
        {
            var token = string.Empty;
            if (ModelState.IsValid)
            {
                HttpClient client = _api.Initial();
                var postTask = client.PostAsJsonAsync("api/authentication", model);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultMessage = result.Content.ReadAsStringAsync().Result;
                    JWT jwt = JsonConvert.DeserializeObject<JWT>(resultMessage);
                    HttpContext.Session.SetString("Token", jwt.Token);
                    return View();
                }
                ModelState.AddModelError(nameof(UserForAuthentication.UserName), "Неверный логин или пароль");
            }
            return View(model);
        }




    }
}
