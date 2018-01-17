using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeDAL
{
    public class EmployeeRepository : IRepository<EmployeeEntity>
    {        
        private List<EmployeeEntity> employees;

        public EmployeeRepository(List<EmployeeEntity> employees)
        {
            this.employees = employees;
        }

        public void Add(EmployeeEntity t)
        {
            int id = 0;


            if (employees.Any())
            {
                id = employees.Max(x => x.Id);
            }

            t.Id = id + 1;

            employees.Add(t);
            
        }
        
        public IEnumerable<EmployeeEntity> Get()
        {
            return employees;
        }
        
    }
}
