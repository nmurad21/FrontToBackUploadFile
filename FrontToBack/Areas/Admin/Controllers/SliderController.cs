using FrontToBack.DAL;
using FrontToBack.Extentions;
using FrontToBack.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Slider dbSlider = await _context.Sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();
            return View(dbSlider);

        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {

                return View();
            }

            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Faylin tipi img olmalidir");
                return View();
            }
            if (slider.Photo.ImageLength(10000))
            {
                ModelState.AddModelError("Photo", "!mb dan yuxari ola bilmez");
                return View();
            }
            //string path= @"C:\Users\User\Desktop\FrontToBack\FrontToBack\wwwroot\img\slider\";
            string fileName = await slider.Photo.SaveImage(_env, "img");
            Slider newSlider = new Slider();
            newSlider.ImageUrl = fileName;
            await _context.Sliders.AddAsync(newSlider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider dbSlider = await _context.Sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();
            Helpers.Helper.DeleteFile(_env, "img",dbSlider.ImageUrl);
            _context.Sliders.Remove(dbSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Slider dbSlider = await _context.Sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();
            return View(dbSlider);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {

                return View();
            }
            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Faylin tipi img olmalidir");
                return View();
            }
            if (slider.Photo.ImageLength(10000))
            {
                ModelState.AddModelError("Photo", "10mb dan yuxari ola bilmez");
                return View();
            }
            Slider dbSlider = await _context.Sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();
            string fileName = await slider.Photo.SaveImage(_env, "img");
            dbSlider.ImageUrl = fileName;
             await _context.SaveChangesAsync();
            return View();
        }
    }
}
