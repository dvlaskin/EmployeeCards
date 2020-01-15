using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeCards.Models.DbModels
{
    public class Register
    {
        public int Id { get; set; }
        public Employee EmployeeId { get; set; }
        public Position PositionId { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateFrom { get; set; }
        public decimal DateTo { get; set; }
    }
}