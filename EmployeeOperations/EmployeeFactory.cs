using EmployeeDAL;
using EmployeeModels;
using EmployeeOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeOperations
{
    public class EmployeeFactory
    {
        public IEmployee GetEmployeeFromEntity(EmployeeEntity empEntity)
        {
            return new Employee
            {
                Id = empEntity.Id,
                HoursWorked = empEntity.HoursWorked,
                Name = empEntity.Name,
                PayRate = empEntity.PayRate
            };
        }

        public IPermEmployee GetEmployeeFromEntity(PermEmployeeEntity empEntity)
        {
            return new PermEmployee
            {
                Id = empEntity.Id,
                HoursWorked = empEntity.HoursWorked,
                Name = empEntity.Name,
                PayRate = empEntity.PayRate,
                Account = empEntity.Account
            };
        }

        public EmployeeEntity GetEntityFromEmployee(IEmployee empEntity)
        {
            return new EmployeeEntity
            {
                Id = empEntity.Id,
                HoursWorked = empEntity.HoursWorked,
                Name = empEntity.Name,
                PayRate = empEntity.PayRate
            };
        }

        public PermEmployeeEntity GetEntityFromEmployee(IPermEmployee empEntity)
        {
            return new PermEmployeeEntity
            {
                Id = empEntity.Id,
                HoursWorked = empEntity.HoursWorked,
                Name = empEntity.Name,
                PayRate = empEntity.PayRate,
                Account = empEntity.Account
            };
        }

        public IPermEmployee GetEmployeeFromAddVM(AddEmployeeViewModel empVM)
        {
            return new PermEmployee
            {                
                HoursWorked = empVM.HoursWorked,
                Name = empVM.Name,
                PayRate = empVM.PayRate
            };
        }

        public GetEmployeeWithoutPayViewModel GetEmployeeWithoutPayVMFromEmployee(IEmployee emp, bool isPermanent = false)
        {
            return new GetEmployeeWithoutPayViewModel
            {
                HoursWorked = emp.HoursWorked,
                Name = emp.Name,
                PayRate = emp.PayRate,
                IsPermanent = isPermanent
            };
        }

        public GetEmployeeWithPayViewModel GetEmployeeWithPayVMFromEmployee(IEmployee emp, bool isPermanent = false)
        {
            return new GetEmployeeWithPayViewModel
            {
                HoursWorked = emp.HoursWorked,
                Name = emp.Name,
                PayRate = emp.PayRate,
                IsPermanent = isPermanent,
                TotalPay = emp.TotalPay
            };
        }

    }
}
