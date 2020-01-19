using EmployeeCards.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeCards.DbService
{
    public class PositionService : BaseService, ITableCrud<Position>
    {
        public PositionService() : base()
        {

        }

        public PositionService(AppDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Position> GetItems()
        {
            return db.Positions.ToList();
        }

        public Position GetItemById(int id)
        {
            return db.Positions.Where(w => w.PositionId == id).FirstOrDefault();
        }

        public Position Create(Position item)
        {
            if (isExists(item.Title))
                throw new ArgumentException("Данная должность уже существует!");

            item.PositionId = 0;
            db.Positions.Add(item);
            db.SaveChanges();
            return item;
        }

        public bool isExists(int id)
        {
            return db.Positions.Any(n => n.PositionId == id);
        }

        public bool isExists(string title)
        {
            return db.Positions.Any(a => a.Title == title);
        }

        public void Update(Position item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Position positionItem = db.Positions.Find(id);
            if (positionItem != null)
            {
                db.Positions.Remove(positionItem);
                db.SaveChanges();
            }
        }

    }
}