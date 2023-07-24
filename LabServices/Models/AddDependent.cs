using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
namespace LabServices.Models
{
    public class AddDependent
    {
        [Required(ErrorMessage = "Required Filed")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Required Filed")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Required Filed")]
        public string Realtionship { get; set; }


        [Required(ErrorMessage = "Required Filed")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Required Filed")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public string PatientId { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public long ContactNo { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public string InsuranceCover { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public int HealthInsuranceNo { get; set; }

        public DataSet data { get; set; }

        public int DepandantId { get; set; }
    }
}