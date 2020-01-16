using EmployeeCards.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeCards.DbService
{
    public class DbInitializerDefault : DropCreateDatabaseIfModelChanges<AppDbContext>
    {

        protected override void Seed(AppDbContext db)
        {
            //base.Seed(context);

            Employee[] employees =
            {
                new Employee() { FirstName = "Sam", LastName = "Rockwell" },
                new Employee() { FirstName = "Rooney", LastName = "Mara" }
            };

            for (int i = 0; i < employees.Length; i++)
            {
                db.Employees.Add(employees[i]);
            }

            Position[] positions =
            {
                new Position() { Title = "Business Analyst" },
                new Position() { Title = "Q&A Engineer" }
            };

            for (int i = 0; i < positions.Length; i++)
            {
                db.Positions.Add(positions[i]);
            }

            db.SaveChanges();

            for (int i = 1; i < employees.Length+1; i++)
            {
                Employee employeeItem = db.Employees.Where(w => w.EmployeeId == i).FirstOrDefault();
                Position positionItem = db.Positions.Where(w => w.PositionId == i).FirstOrDefault();
                var registerCard = new RegisterCard()
                {
                    EmployeeId = employeeItem.EmployeeId,
                    PositionId = positionItem.PositionId,
                    DateFrom = DateTime.Now.AddDays(-10),
                    Salary = 1500.00m
                };

                db.RegisterCards.Add(registerCard);
            }

            db.SaveChanges();
        }

    }

    public class DbInitializerClean : DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext db)
        {
            //base.Seed(context);

            Employee[] employees =
            {
                new Employee() { FirstName = "Sam", LastName = "Rockwell" },
                new Employee() { FirstName = "Rooney", LastName = "Mara" }
            };

            for (int i = 0; i < employees.Length; i++)
            {
                db.Employees.Add(employees[i]);
            }

            Position[] positions =
            {
                new Position() { Title = "Business Analyst" },
                new Position() { Title = "Q&A Engineer" }
            };

            for (int i = 0; i < positions.Length; i++)
            {
                db.Positions.Add(positions[i]);
            }

            db.SaveChanges();

            for (int i = 1; i < employees.Length + 1; i++)
            {
                Employee employeeItem = db.Employees.Where(w => w.EmployeeId == i).FirstOrDefault();
                Position positionItem = db.Positions.Where(w => w.PositionId == i).FirstOrDefault();
                var registerCard = new RegisterCard()
                {
                    EmployeeId = employeeItem.EmployeeId,
                    PositionId = positionItem.PositionId,
                    DateFrom = DateTime.Now.AddDays(-10),
                    Salary = 1500.00m
                };

                db.RegisterCards.Add(registerCard);
            }

            db.SaveChanges();
        }
    }
}