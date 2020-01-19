using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCards.DbService
{
    interface ITableCrud<T> : IDisposable where T : class
    {
        IEnumerable<T> GetItems();
        T GetItemById(int id);

        T Create(T item);
        void Update(T item);
        void Delete(int id);
        bool isExists(int id);
    }
}
