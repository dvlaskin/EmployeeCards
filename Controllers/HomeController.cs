using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeCards.DbService;
using EmployeeCards.Models.DbModels;

namespace EmployeeCards.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var regCards = new List<RegisterCard>();

            var positions = new Dictionary<int, string>();

            using (var db = AppDbContext.GetInstance())
            {
                List<RegisterCard> cardItems = db.RegisterCards.Include("Employee").Include("Position").ToList();
                regCards.AddRange(cardItems);

                positions = db.Positions.ToDictionary(t => t.PositionId, t => t.Title);
            }

            ViewBag.Positions = positions;

            return View(regCards);
        }

        public ActionResult EmployeeModal()
        {
            return PartialView();
        }

        public ActionResult PositionModal()
        {
            return PartialView();
        }
    }
}