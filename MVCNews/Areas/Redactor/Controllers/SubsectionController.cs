using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.MVC.Models;
using News.MVC.Services.Contracts;
using System;
using System.Threading.Tasks;
namespace News.MVC.Areas.Redactor.Controllers
{
    [Area("Redactor")]
    public class SubsectionController : Controller
    {
        private readonly ISectionServices _sectionServices;
        private readonly ISubsectionServices _subsectionServices;
        public SubsectionController(ISectionServices sectionServices, ISubsectionServices subsectionServices)
        {
            _sectionServices = sectionServices;
            _subsectionServices = subsectionServices;
        }
        public async Task<IActionResult> Index()
        {
            var section = await _sectionServices.GetSections();
            return View(section);
        }
        public async Task<IActionResult> GetSubsections(Guid id)
        {
            ViewBag.sectionId = id;
            var subsection = await _subsectionServices.GetSubsections(id);
            return View(subsection);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubsection(Guid Id, SubsectionData subsection, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                var resultSectionServices = await _subsectionServices.CreateSubsection(subsection, Id, titleImageFile);
                if (resultSectionServices == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public IActionResult CreateSubsection()
        {
            return View();
        }
        public async Task<IActionResult> PutSubsection(Guid id, Guid sectionId)
        {
            var subsection = await _subsectionServices.GetSubsection(id, sectionId);
            ViewBag.idsection = sectionId;
            return View(subsection);
        }
        [HttpPost]
        public async Task<IActionResult> PutSubsection(Guid idsection, SubsectionData sectionData, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                var resultSubsectionServices = await _subsectionServices.UpdateSubsection(sectionData, sectionData.Id, sectionData.IdSection, titleImageFile);
                if (resultSubsectionServices == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> DeleteSubsection(Guid id, Guid idsection)
        {
            var resultSubsectionServices = await _subsectionServices.DeleteSubsection(id, idsection);
            return RedirectToAction("Index");
        }
    }
}
