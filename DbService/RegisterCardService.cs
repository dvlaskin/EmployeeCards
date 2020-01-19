using EmployeeCards.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeCards.DbService
{
    public class RegisterCardService : BaseService, ITableCrud<RegisterCard>
    {

        public RegisterCardService() : base()
        {

        }

        public RegisterCardService(AppDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<RegisterCard> GetItems()
        {
            return db.RegisterCards.Include("Employee").Include("Position").ToList();
        }

        public RegisterCard GetItemById(int id)
        {
            return db.RegisterCards.Include("Employee").Include("Position").Where(w => w.RegisterId == id).FirstOrDefault();
        }

        public RegisterCard[] GetItemsByEmployeeName(string firstName, string lastName)
        {
            var employeeDb = new EmployeeService(db);
            Employee employee = employeeDb.GetItemByName(firstName, lastName);

            if (employee == null)
                return new RegisterCard[0];

            return db.RegisterCards
                .Include("Employee")
                .Include("Position")
                .Where(w => w.EmployeeId == employee.EmployeeId)
                .ToArray();
        }

        public RegisterCard Create(RegisterCard item)
        {
            var employeeDb = new EmployeeService(db);

            item.RegisterId = 0;
            db.RegisterCards.Add(item);
            db.SaveChanges();

            SetDateFired(item);

            return item;
        }

        public void Update(RegisterCard item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }        
        
        public void Delete(int id)
        {
            RegisterCard registegCardItem = db.RegisterCards.Find(id);
            if (registegCardItem != null)
            {
                db.RegisterCards.Remove(registegCardItem);
                db.SaveChanges();
            }
        }

        public bool isExists(int id)
        {
            return db.RegisterCards.Any(n => n.RegisterId == id);
        }

        private void SetDateFired(RegisterCard regCard)
        {
            if (regCard.EmployeeId == null && regCard.DateHired != null)
                return;

            RegisterCard[] notFiredCards = db.RegisterCards
                .Where(w => w.DateFired == null && w.EmployeeId == regCard.EmployeeId && w.RegisterId != regCard.RegisterId)
                .ToArray();

            foreach (var item in notFiredCards)
            {
                item.DateFired = Convert.ToDateTime(regCard.DateHired).AddDays(-1);
                Update(item);
            }
        }
    }
}