using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeModels
{
    public class PermEmployee : Employee, IPermEmployee
    {
        public override double CalculatePay()
        {
            return base.CalculatePay()*2;
        }
        public double Account { get; set; }
    }
}
