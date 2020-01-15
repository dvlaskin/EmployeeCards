﻿using System;
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

            using (var db = AppDbContext.GetInstance())
            {
                regCards = db.RegisterCards.ToList();
            }

            return View(regCards);
        }
    }
}