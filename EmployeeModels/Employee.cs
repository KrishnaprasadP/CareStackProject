using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeModels
{
    public class Employee : IEmployee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double PayRate { get; set; }

        public double HoursWorked { get; set; }

        public double TotalPay { get; set; }

        public virtual double CalculatePay()
        {
            return this.PayRate * this.HoursWorked * 0.5;
        }
    }
}
