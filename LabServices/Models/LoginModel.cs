using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LabServices.Models
{
    public class LoginModel
    {  
        [Display (Name ="UserId")]
        [Required (ErrorMessage ="Required Filed")]
        public string UserID { get; set; }

        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Required Filed")]
        public string Password { get; set; }

        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Required Filed")]
        public string HashKey { get; set; }

        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Required Filed")]
        public string UserType { get; set; }

        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Required Filed")]
        public string FavActor { get; set; }

        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Required Filed")]
        public string FavFood { get; set; }

        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Required Filed")]
        public string BirthCity { get; set; }

        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Required Filed")]
        public string NewPassword { get; set; }
    }
}