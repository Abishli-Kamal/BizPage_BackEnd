using BizPage_Back_End.DAL;
using BizPage_Back_End.Models;
using BizPage_Back_End.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BizPage_Back_End.Areas.BizPageAdmin.Controllers
{
    [Area("BizPageAdmin")]
    public class AboutController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<About> abouts = await _context.Abouts.ToListAsync();

            return View(abouts);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(About about)
        {
            if (!ModelState.IsValid) return View();
            if (about == null) return View();
            if (about.Photo != null)
            {
                if (about.Photo.Length > 1024 * 1024 && !about.Photo.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("", "melumatlari duzgun daxil edin");
                    return View();
                }
                else
                {
                    about.Image = await about.Photo.FileCreate(_env.WebRootPath, @"assets\img");
                    await _context.AddAsync(about);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ModelState.AddModelError("", "File Secin");
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            About about = await _context.Abouts.FirstOrDefaultAsync(s => s.Id == id);
            return View(about);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, About about)
        {
            About existedabout = await _context.Abouts.FirstOrDefaultAsync(s => s.Id == id);
            if (existedabout == null) return View();
            if (existedabout.Id != about.Id) return NotFound();

            existedabout.Title = about.Title;
            existedabout.SubTitle = about.SubTitle;
            existedabout.Icon = about.Icon;

            if (about.Photo != null)
            {
                if (about.Photo.ContentType.Contains("image") && about.Photo.Length < 1024 * 1024)
                {
                    string path = _env.WebRootPath + @"assets\img" + existedabout.Image;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    existedabout.Image = await about.Photo.FileCreate(_env.WebRootPath, @"assets\img");
                }
            }
            else
            {
                ModelState.AddModelError("Photo", "Selected image is not valid!");
                return View(about);
            }
           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }
}
