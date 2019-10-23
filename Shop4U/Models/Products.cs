using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop4U.Models
{



    public enum Categories
    {
        [Display(Name = "Automotive")]
        Automotive,
        [Display(Name = "Electronics")]
        Electronics,
        [Display(Name = "Entertainment")]
        Entertainment,
        [Display(Name = "Clothing")]
        Clothing,
        [Display(Name = "furniture")]
        furniture,
        [Display(Name = "kitchen")]
        kitchen,
        [Display(Name = "Food")]
        Food,
        [Display(Name = "Computers")]
        Computers,
        [Display(Name = "Home")]
        Home,
        [Display(Name = "Office")]
        Office,
        [Display(Name = "Sports")]
        Sports,
        [Display(Name = "Software")]
        Software,
        [Display(Name = "Health")]
        Health,
        [Display(Name = "Beauty")]
        Beauty,
        [Display(Name = "Arts")]
        Arts
    }


    public class Products
    {
        [Key]
        public int product_Id { get; set; }


        //This Is the foreign key related to the Users Table
        public int? UID { get; set; }
        public Users Users { get; set; }

        public string product_branch { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "שם המוצר חובה")]
        [Display(Name = "Product Name")]
        public string product_name { get; set; }



        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "תיאור חובה")]
        [Display(Name = "Product Description")]
        public string product_description { get; set; }



        [Required(ErrorMessage = "קטגוריה חובה")]
        [Display(Name = "Product Category")]
        public Categories? product_category { get; set; }



        [Display(Name = "Product File")]
        public string product_image { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "צבע חובה")]
        [Display(Name = "Product Color")]
        public string product_color { get; set; }


        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }


        [Display(Name = "Taken")]
        public bool Taken { get; set; }


        [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$", ErrorMessage = "פורמט תאריך שגוי. DD/MM/YYYY")]
        public string TakenDate { get; set; }

        [Display(Name = "Buyer Name")]
        public string buyer_name { get; set; }


    }
}
