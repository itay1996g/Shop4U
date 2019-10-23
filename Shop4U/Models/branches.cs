

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Shop4U.Models
{
    public class Branches
    {
        [Key]
        public int branch_Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "שם סניף חובה")]
        [Display(Name = "Branch Name")]
        public string branch_name { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "עיר הסניף חובה")]
        [Display(Name = "Branch City")]
        public string branch_address { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "מנהל הסניף הינו חובה")]
        [Display(Name = "Branch Owner")]
        public string branch_owner { get; set; }



        [Required(ErrorMessage = "קו רוחב חובה")]
        [Display(Name = "Latitude")]
        public double branch_lat { get; set; }



        [Required(ErrorMessage = "קו אורך חובה")]
        [Display(Name = "Longitude")]
        public double branch_lng { get; set; }

    }
}
