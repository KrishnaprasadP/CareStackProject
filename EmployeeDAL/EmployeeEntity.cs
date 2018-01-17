using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL
{
    public class EmployeeEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double PayRate { get; set; }

        public double HoursWorked { get; set; }        
    }
}
