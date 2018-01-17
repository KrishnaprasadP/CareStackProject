using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeModels
{
    public interface IPermEmployee : IEmployee
    {
        double Account { get; set; }
    }
}