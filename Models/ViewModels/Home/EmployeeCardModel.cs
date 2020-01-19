using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeCards.Models.ViewModels.Home
{
    public class EmployeeCardModel
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public int PositionId { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public DateTime DateHired { get; set; }
        public DateTime? DateFired { get; set; }
    }
}