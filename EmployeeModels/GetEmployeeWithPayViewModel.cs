using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeModels
{
    public class GetEmployeeWithPayViewModel : GetEmployeeWithoutPayViewModel
    {        
        public double TotalPay { get; set; }
    }
}
