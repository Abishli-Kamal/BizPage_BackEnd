using BizPage_Back_End.Models;
using System.Collections.Generic;

namespace BizPage_Back_End.View_models
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<About> Abouts { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

    }
}
