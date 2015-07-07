using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInformation.Models
{
   public class DetailModels
    {
        public int Id { get; set; }
        [Display(Name = "Brand")]
        public string Brand { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Year")]
        public int Year { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
