using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeCards.DbService
{
    public abstract class BaseService : IDisposable
    {
        protected AppDbContext db;

        public BaseService()
        {
            db = AppDbContext.GetInstance();
        }

        public BaseService(AppDbContext dbContext)
        {
            db = dbContext;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}