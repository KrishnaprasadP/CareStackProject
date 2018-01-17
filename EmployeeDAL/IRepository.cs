using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL
{
    public interface IRepository<T>
    {
        void Add(T t);

        IEnumerable<T> Get();
    }
}
