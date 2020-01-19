﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeCards.DbService;
using EmployeeCards.Models.DbModels;
using EmployeeCards.Models.ViewModels.Home;

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
                var regCardDb = new RegisterCardService(db);
                IEnumerable<RegisterCard> cardItems = regCardDb.GetItems();

                regCards.AddRange(cardItems);

                positions = db.Positions.ToDictionary(t => t.PositionId, t => t.Title);
            }

            ViewBag.Positions = positions;

            return View(regCards);
        }

        ActionResult EmployeeModal()
        {
            return PartialView();
        }

        ActionResult PositionModal()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddPosition(string positionTitle)
        {
            if (string.IsNullOrEmpty(positionTitle))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { error = "Некорректное название должности." });
            }
            else
            {
                var positionItem = new Position()
                {
                    Title = positionTitle.Trim()
                };

                try
                {
                    var dbPosition = new PositionService();
                    positionItem = dbPosition.Create(positionItem);
                }
                catch (Exception ex)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { error = ex.Message });
                }

                return Json(positionItem);
            }
        }

        [HttpPost]
        public ActionResult AddEmployeeCard(EmployeeCardModel cardModel)
        {
            if (ModelState.IsValid == false)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { error = "Заполните все обязательные поля!" });
            }

            if (cardModel.DateHired != null && isValidDateHired(cardModel) == false)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { error = "Текущая дата \"Нанят\" не може быть ранее или равна предыдущим датам \"Нанят\"!" });
            }

            var regCard = new RegisterCard();

            using (var db = AppDbContext.GetInstance())
            {
                var regCardObject = new RegisterCard()
                {
                    EmployeeId = GetEmployeeId(db, cardModel),
                    PositionId = cardModel.PositionId,
                    Salary = cardModel.Salary,
                    DateHired = cardModel.DateHired,
                    DateFired = cardModel.DateFired
                };

                var regCardDb = new RegisterCardService(db);
                regCard = regCardDb.Create(regCardObject);

                var posinionDb = new PositionService(db);
                regCard.Position = posinionDb.GetItemById((int)regCard.PositionId);
            }

            return Json(new { RegisterId = regCard.RegisterId });
        }

        private bool isValidDateHired(EmployeeCardModel emplCardModel)
        {
            bool result = true;

            using (var db = AppDbContext.GetInstance())
            {
                var regCardDb = new RegisterCardService(db);
                var regCards = regCardDb.GetItemsByEmployeeName(emplCardModel.Firstname, emplCardModel.Lastname);
                
                if (regCards.Length > 0)
                {
                    result = !regCards.Any(n => n.DateHired >= emplCardModel.DateHired);
                }
            }

            return result;
        }

        private int GetEmployeeId(AppDbContext db, EmployeeCardModel cardModel)
        {
            var employeeDb = new EmployeeService(db);

            if (employeeDb.isExists(cardModel.Firstname, cardModel.Lastname))
            {
                return employeeDb.GetItemByName(cardModel.Firstname, cardModel.Lastname).EmployeeId;
            }
            else
            {
                var employee = new Employee() { FirstName = cardModel.Firstname, LastName = cardModel.Lastname };
                employeeDb.Create(employee);
                return employee.EmployeeId;
            }
        }
    }
}