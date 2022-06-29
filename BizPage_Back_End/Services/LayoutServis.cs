using BizPage_Back_End.DAL;
using BizPage_Back_End.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizPage_Back_End.Services
{
    public class LayoutServis
    {
        private readonly AppDbContext _context;

        public LayoutServis(AppDbContext context)
        {
            _context = context;
        }
        public  async Task<List<Setting>> GetDatas()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();
            return settings;
        }
    }
}
