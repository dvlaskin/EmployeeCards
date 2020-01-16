using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EmployeeCards.Models.DbModels;

namespace EmployeeCards.DbService
{
    public class AppDbContext : DbContext
    {

        public AppDbContext() : base(GetConnectionString())
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<RegisterCard> RegisterCards { get; set; }

        static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["LocalDb"].ConnectionString;
        }

        public static AppDbContext GetInstance()
        {
            return new AppDbContext();
        }

    }
}