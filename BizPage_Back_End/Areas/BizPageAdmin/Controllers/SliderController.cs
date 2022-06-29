using BizPage_Back_End.DAL;
using BizPage_Back_End.Models;
using BizPage_Back_End.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizPage_Back_End.Areas.BizPageAdmin.Controllers
{
    [Area("BizPageAdmin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            if (slider.Photo != null)
            {
                if (slider.Photo.Length > 1024 * 1024 & !slider.Photo.ContentType.Contains("image"))
                {
                    return View();
                }
            }
            slider.Image = await slider.Photo.FileCreate(_env.WebRootPath, @"assets\img");
            await _context.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            return View(slider);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(int id, Slider slider)
        {
            Slider existedslider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (existedslider == null) return View();
            if (slider.Id != existedslider.Id) return NotFound();

            if (existedslider.Photo != null)
            {
                if (slider.Photo.Length < 1024 * 1024 && slider.Photo.ContentType.Contains("image"))
                {
                    existedslider.SubTitle = slider.SubTitle;
                    existedslider.Title = slider.Title;
                    existedslider.Image = await slider.Photo.FileCreate(_env.WebRootPath, @"assets\img");
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ModelState.AddModelError("", "Duzgun melumatlar daxil edin");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "File secin");
                return View();
            }

        }
    }
}
