using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.MVC.Models;
using News.MVC.Services.Contracts;
using System;
using System.Threading.Tasks;
namespace News.MVC.Areas.Redactor.Controllers
{
    [Area("Redactor")]
    public class SectionController : Controller
    {
        private readonly ISectionServices _sectionServices;
        public SectionController(ISectionServices sectionServices)
        {
            _sectionServices = sectionServices;
        }
        public async Task<IActionResult> Index()
        {
            var section = await _sectionServices.GetSections();
            return View(section);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSection(SectionData section, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                var resultSectionServices = await _sectionServices.CreateSection(section, titleImageFile);
                if (resultSectionServices == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PutSection(SectionData sectionData, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                var resultSectionServices = await _sectionServices.UpdateSection(sectionData, sectionData.Id, titleImageFile);
                if (resultSectionServices == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> PutSection(Guid id)
        {
            var section = await _sectionServices.GetSection(id);
            return View(section);
        }
        public IActionResult CreateSection()
        {
            return View();
        }
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            var resultSectionServices = await _sectionServices.DeleteSection(id);
            if (resultSectionServices == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
