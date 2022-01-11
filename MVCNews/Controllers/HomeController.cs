using Microsoft.AspNetCore.Mvc;
using News.MVC.Services.Contracts;
using System;
using System.Threading.Tasks;
namespace News.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISectionServices _sectionServices;
        private readonly IArticleServices _articleServices;
        private readonly ISubsectionServices _subsectionServices;
        public HomeController(ISectionServices sectionServices, IArticleServices articleServices, ISubsectionServices subsectionServices)
        {
            _sectionServices = sectionServices;
            _articleServices = articleServices;
            _subsectionServices = subsectionServices;

        }
        public async Task<IActionResult> Index()
        {
            var section = await _sectionServices.GetSections();
            return View(section);
        }
        public async Task<IActionResult> Articles(Guid sectionId, Guid subsectionId)
        {
            var article = await _articleServices.GetArticles(sectionId, subsectionId);
            ViewBag.idsection = sectionId;
            return View(article);
        }
        public async Task<IActionResult> Subsections(Guid id)
        {
            var subsection = await _subsectionServices.GetSubsections(id);
            return View(subsection);
        }
        public async Task<IActionResult> Article(Guid subsectionId, Guid id, Guid sectionId)
        {
            var article = await _articleServices.GetArticle(sectionId, subsectionId, id);
            return View(article);
        }
    }
}
