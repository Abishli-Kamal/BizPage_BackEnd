using BizPage_Back_End.DAL;
using BizPage_Back_End.Models;
using BizPage_Back_End.View_models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizPage_Back_End.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM model = new HomeVM
            {
                Sliders = await _context.Sliders.ToListAsync(),
                Abouts = await _context.Abouts.ToListAsync(),
                Categories = await _context.Categories.ToListAsync(),
                Products = await _context.Products.ToListAsync(),
                
            };
            return View(model);
        }
    }
}
