using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeModels
{
    public interface IEmployee
    {
        int Id { get; set; }

        string Name { get; set; }

        double PayRate { get; set; }

        double HoursWorked { get; set; }

        double TotalPay { get; set; }

        double CalculatePay();
    }
}
