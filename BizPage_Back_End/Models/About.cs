using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizPage_Back_End.Models
{
    public class About
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Icon { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
