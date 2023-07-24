using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LabServices.Models
{
    public class HCPModel
    {
        [Required(ErrorMessage = "**")]
        public string HCPId { get; set; }

        [Required(ErrorMessage = "**")]
        public string Name { get; set; }

        [Display(Name = "License No. ")]
        [Required(ErrorMessage = "**")]
        public string License_no { get; set; }

        [Required(ErrorMessage = "**")]
        public string Address { get; set; }

        [Display(Name = "Zip Code ")]
        [Required(ErrorMessage = "**")]
        public int ZipCode { get; set; }


        [Required(ErrorMessage = "**")]
        public string City { get; set; }


        [Required(ErrorMessage = "**")]
        public string State { get; set; }

        [Display(Name = "Contact No. ")]
        [Required(ErrorMessage = "**")]
        public long ContactNo { get; set; }

        [Display(Name = "Alternate Contact No. ")]
        [Required(ErrorMessage = "Required Filed")]
        public long AltContactNo { get; set; }

        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "**")]
        public string Email { get; set; }

        [Display(Name = "Question ")]
        [Required(ErrorMessage = "**")]
        public string Question1 { get; set; }

        [Display(Name = "Answer ")]
        [Required(ErrorMessage = "**")]
        public string Answer1 { get; set; }

        [Display(Name = "Question ")]
        [Required(ErrorMessage = "**")]
        public string Question2 { get; set; }

        [Display(Name = "Answer ")]
        [Required(ErrorMessage = "**")]
        public string Answer2 { get; set; }

        [Display(Name = "Question ")]
        [Required(ErrorMessage = "**")]
        public string Question3 { get; set; }

        [Display(Name = "Answer ")]
        [Required(ErrorMessage = "**")]
        public string Answer3 { get; set; }

        [Required(ErrorMessage = "**")]
        public string Passsword { get; set; }

     

       
    }
}