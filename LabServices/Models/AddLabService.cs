using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
namespace LabServices.Models
{
    public class AddLabService
    {
      
        public string LabId { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public string TestName { get; set; }


        [Required(ErrorMessage = "Required Filed")]
        public string TestCode { get; set; }


        [Required(ErrorMessage = "Required Filed")]
        public string TestDescription { get; set; }


        public DateTime? StartTime { get; set; }

        [Required]
       // [RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")]
        public int TestDuration
        {
            get;

            //return StartTime.HasValue ? StartTime.Value.ToString("hh:mm tt") : string.Empty;


            set;
            //{
                //StartTime = DateTime.Parse(value);
           // }
        }


        [Required(ErrorMessage = "Required Filed")]
        public double CostOfTest { get; set; }


        [Required(ErrorMessage = "Required Filed")]
        public string LabHome { get; set; }

        public DataSet Data { get; set; }

        [Required(ErrorMessage = "Required Filed")]
        public DateTime Date { get; set; }

    }
}