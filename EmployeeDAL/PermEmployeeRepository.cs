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
    public class PermEmployeeRepository : IRepository<PermEmployeeEntity>
    {
        private List<PermEmployeeEntity> PermEmployees;
        
        public PermEmployeeRepository(List<PermEmployeeEntity> permEmployees)
        {
            PermEmployees = permEmployees;
        }

        public void Add(PermEmployeeEntity t)
        {
            int id = 0;


            if (PermEmployees.Any())
            {
                id = PermEmployees.Max(x => x.Id);
            }

            t.Id = id + 1;

            PermEmployees.Add(t);

        }

        public IEnumerable<PermEmployeeEntity> Get()
        {
            return PermEmployees;
        }
    }
}
