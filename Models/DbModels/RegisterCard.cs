using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeCards.Models.DbModels
{
    public class RegisterCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegisterId { get; set; }
        public decimal Salary { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public int? PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}