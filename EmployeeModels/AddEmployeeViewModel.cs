using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeModels
{
    public class AddEmployeeViewModel
    {        
        public string Name { get; set; }

        public double PayRate { get; set; }

        public double HoursWorked { get; set; }
        
        public bool IsPermanent { get; set; }
    }
}
