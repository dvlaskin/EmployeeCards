using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeCards.Models.ViewModels.Home
{
    public class RegCardsViewModel
    {
        public int RegisterId { get; set; }
        public string Title { get; set; }
        public string Fio { get; set; }
        public decimal Salary { get; set; }
        public string DateHired { get; set; }
        public string DateFired { get; set; }
    }
}