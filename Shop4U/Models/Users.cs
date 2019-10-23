using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop4U.Models
{
    public enum Gender
    {
        [Display(Name = "זכר")]
        זכר,
        [Display(Name = "נקבה")]
        נקבה
    }



    public class Users
    {
        [Key]
        public int UID { get; set; }



        [Required(ErrorMessage = "הכנס כתובת מייל")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string user_email { get; set; }




        [Required(ErrorMessage = "הכנס שם מלא")]
        [RegularExpression(@"^(([A-za-zא-ת]+[\s]{1}[A-za-zא-ת]+)|([A-Za-zא-ת]+))$", ErrorMessage = "Please enter valid name")]
        [Display(Name = "Full name")]
        public string user_fullname { get; set; }



        [Required(ErrorMessage = "הכנס סיסמא")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string user_password { get; set; }



        [StringLength(15, MinimumLength = 6)]
        [Required(ErrorMessage = "הכנס נייד")]
        [Display(Name = "Phone")]
        public string user_phone { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "הכנס עיר")]
        [Display(Name = "City")]
        public string user_city { get; set; }



        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }


        [Required(ErrorMessage = "הכנס מין")]
        [Display(Name = "Gender")]
        public Gender? user_gender { get; set; }


        [Range(1930,2019)]
        [Required(ErrorMessage = "הכנס שנת לידה")]
        [Display(Name = "Birth_Year")]
        public int birth_year { get; set; }





    }
}
