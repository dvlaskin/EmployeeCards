using EmployeeCards.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeCards.DbService
{
    public class EmployeeService : BaseService, ITableCrud<Employee>
    {
        public EmployeeService() : base()
        {

        }

        public EmployeeService(AppDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Employee> GetItems()
        {
            return db.Employees.ToList();
        }

        public Employee GetItemById(int id)
        {
            return db.Employees.Where(w => w.EmployeeId == id).FirstOrDefault();
        }

        public Employee GetItemByName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return null;

            return db.Employees.Where(w => w.FirstName == firstName && w.LastName == lastName).FirstOrDefault();
        }

        public Employee Create(Employee item)
        {
            if (isExists(item.FirstName, item.LastName))
                throw new ArgumentException("Данный сотрудник уже существует!");

            item.EmployeeId = 0;
            db.Employees.Add(item);
            db.SaveChanges();
            return item;
        }

        public void Update(Employee item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Employee employeeItem = db.Employees.Find(id);
            if (employeeItem != null)
            {
                db.Employees.Remove(employeeItem);
                db.SaveChanges();
            }
        }

        public bool isExists(int id)
        {
            return db.Employees.Any(n => n.EmployeeId == id);
        }

        public bool isExists(string firstName, string lastName)
        {
            return db.Employees.Any(a => a.FirstName == firstName && a.LastName == lastName);
        }

    }
}