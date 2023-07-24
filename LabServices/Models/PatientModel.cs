using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LabServices.Models
{
    public class PatientModel
    {
        [Required(ErrorMessage = "Required Filed")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public string DependantId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required Filed")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Required Filed")]
        public string LastName { get; set; }

        
        [Required(ErrorMessage = "Required Filed")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Required Filed")]
        public int Age { get; set; }


        [Required(ErrorMessage = "Required Filed")]
        public string Address { get; set; }

        [Display(Name = "Contact No.")]
        [Required(ErrorMessage = "Required Filed")]
        public long ContactNo { get; set; }

        [Display(Name = "Medical Complaint")]
        [Required(ErrorMessage = "Required Filed")]
        public string MedicalComplaint { get; set; }

        [Display(Name = "Referred Doctor")]
        [Required(ErrorMessage = "Required Filed")]
        public string ReferredDoctor { get; set; }

        [Display(Name = "Insurance Cover")]
        [Required(ErrorMessage = "Required Filed")]
        public int InsuranceCover { get; set; }

        [Required(ErrorMessage = "**")]
        public string Passsword { get; set; }

        [Required(ErrorMessage = "**")]
        public string PatientId { get; set; }

        [Required(ErrorMessage = "**")]
        public string Answer1 { get; set; }


        [Required(ErrorMessage = "**")]
        public string Answer2 { get; set; }


        

        [Required(ErrorMessage = "**")]
        public string Answer3 { get; set; }


        [Required(ErrorMessage = "**")]
        public string ParentId { get; set; }

        public DataSet Data { get; set; }

    }
}