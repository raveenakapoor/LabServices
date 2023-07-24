using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace LabServices.Models
{
    public class LSPModel
    {
        [Required(ErrorMessage = "**")]
        public string Name { get; set; }

       
        [Required(ErrorMessage = "**")]
        public string Address { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "**")]
        public int ZipCode { get; set; }

       
        [Required(ErrorMessage = "**")]
        public string City { get; set; }

      
        [Required(ErrorMessage = "**")]
        public string State { get; set; }

        [Display(Name = "Mobile No.")]
        [Required(ErrorMessage = "**")]
        public long  ContactNo { get; set; }

        [Display(Name = "Alternate No.")]
        [Required(ErrorMessage = "Required Filed")]
        public long AltContactNo { get; set; }

        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "Email ")]
        [Required(ErrorMessage = "**")]
        public string Email { get; set; }

        [Required(ErrorMessage = "**")]
        public string Category { get; set; }

        [Display(Name = "Question")]
        [Required(ErrorMessage = "Required Filed")]
        public string Question1 { get; set; }

        [Display(Name = "Answer")]
        [Required(ErrorMessage = "**")]
        public string Answer1 { get; set; }

        [Display(Name = "Question")]
        [Required(ErrorMessage = "Required Filed")]
        public string Question2 { get; set; }

        [Display(Name = "Answer")]
        [Required(ErrorMessage = "**")]
        public string Answer2 { get; set; }

        [Display(Name = "Question")]
        [Required(ErrorMessage = "Required Filed")]
        public string Question3 { get; set; }

        [Display(Name = "Answer")]
        [Required(ErrorMessage = "**")]
        public string Answer3 { get; set; }

            [Required(ErrorMessage = "**")]
            public string Passsword { get; set; }

        [Required(ErrorMessage = "**")]
        public string LabId { get; set; }

        [Required(ErrorMessage = "**")]
        public string ParentId { get; set; }

        public DataSet Data { get; set; }
    }

    public class LabTest
    {
        [Required(ErrorMessage = "Required Filed")]
        public string LabID { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public string Cost { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public string Home { get; set; }

    
    }

}