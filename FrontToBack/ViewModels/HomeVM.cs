using FrontToBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public PageIntro PageIntro { get; set; }
        public IEnumerable<Category> categories { get; set; }
    }
}
