using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Dont empty"), MaxLength(10,ErrorMessage ="10dan yuxari ola bilmez")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Dont empty") ,MaxLength(50,ErrorMessage ="50den yuxari ola bilmez")]
        public string Desc { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
