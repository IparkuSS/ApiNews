using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.MVC.Models;
using News.MVC.Services.Contracts;
using System;
using System.Threading.Tasks;
namespace News.MVC.Areas.Redactor.Controllers
{
    [Area("Redactor")]
    public class ArticlesController : Controller
    {
        private readonly ISectionServices _sectionServices;
        private readonly ISubsectionServices _subsectionServices;
        private readonly IArticleServices _articleServices;

        public ArticlesController(ISectionServices sectionServices, ISubsectionServices subsectionServices, IArticleServices articleServices)
        {
            _sectionServices = sectionServices;
            _subsectionServices = subsectionServices;
            _articleServices = articleServices;
        }
        public async Task<IActionResult> GetArticles(Guid sectionId, Guid subsectionId)
        {
            var article = await _articleServices.GetArticles(sectionId, subsectionId);
            ViewBag.idsection = sectionId;
            ViewBag.idsubsection = subsectionId;
            return View(article);
        }
        public async Task<IActionResult> Subsections(Guid id)
        {
            var subsection = await _subsectionServices.GetSubsections(id);
            return View(subsection);
        }
        public async Task<IActionResult> Index()
        {
            var section = await _sectionServices.GetSections();
            return View(section);
        }
        [HttpPost]
        public async Task<IActionResult> CreateArticle(Guid sectionId, Guid subsectionId, ArticleData article, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                var articleServicesResult = await _articleServices.CreateAricle(article, sectionId, subsectionId, titleImageFile);
                if (articleServicesResult == true) return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult CreateArticle(Guid sectionId, Guid subsectionId)
        {
            ViewBag.idsection = sectionId;
            ViewBag.idsubsection = subsectionId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PutArticle(Guid sectionId, Guid subsectionId, ArticleData article, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                var articleServicesResult = await _articleServices.UpdateArticle(article, sectionId, subsectionId, article.Id, titleImageFile);
                if (articleServicesResult == true) return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> PutArticle(Guid id, Guid sectionId, Guid subsectionId)
        {
            var article = await _articleServices.GetArticle(sectionId, subsectionId, id);
            ViewBag.idsection = sectionId;
            ViewBag.idsubsection = subsectionId;
            return View(article);
        }
        public async Task<IActionResult> DeleteArticle(Guid id, Guid sectionId, Guid subsectionId)
        {
            var articleServicesResult = await _articleServices.DeleteArticle(sectionId, subsectionId, id);
            if (articleServicesResult == true) return RedirectToAction("Index");
            return RedirectToAction("GetArticles");
        }
    }
}
