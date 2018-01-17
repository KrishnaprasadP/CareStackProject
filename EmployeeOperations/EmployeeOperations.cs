using EmployeeDAL;
using EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeOperations
{
    public interface IEmpOperations
    {
        bool AddEmployee(IEmployee emp);

        bool AddPermEmployee(IPermEmployee permEmp);

        IEnumerable<IPermEmployee> GetPermEmployees(bool withPay);

        IEnumerable<IEmployee> GetTempEmployees(bool withPay);
    }

    public class EmpOperations : IEmpOperations
    {   
        IRepository<EmployeeEntity> empRep;
        IRepository<PermEmployeeEntity> permEmpRep;
        IUnitOfWork _uow;
        EmployeeFactory empFactory;

        public EmpOperations(IUnitOfWork uow)
        {
            _uow = uow;
            this.empRep = uow.EmpRepository;
            this.permEmpRep = uow.PermEmpRepository;
            this.empFactory = new EmployeeFactory();
        }

        public bool AddEmployee(IEmployee emp)
        {
            try
            {
                var empEntity = this.empFactory.GetEntityFromEmployee(emp);
                empRep.Add(empEntity);
                _uow.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }            
        }

        public bool AddPermEmployee(IPermEmployee permEmp)
        {
            try
            {
                var permEmpEntity = empFactory.GetEntityFromEmployee(permEmp);                
                permEmpRep.Add(permEmpEntity);
                _uow.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<IPermEmployee> GetPermEmployees(bool withPay)
        {            
            var empEntity = _uow.PermEmpRepository.Get();
            var emp = new List<IPermEmployee>();

            empEntity.ToList().ForEach(x =>
            {
                emp.Add(empFactory.GetEmployeeFromEntity(x));
            });

            if (withPay)
                emp.ForEach(x => x.TotalPay = x.CalculatePay());

            return emp;
        }

        public IEnumerable<IEmployee> GetTempEmployees(bool withPay)
        {
            var empEntity = _uow.EmpRepository.Get();
            var emp = new List<IEmployee>();

            empEntity.ToList().ForEach(x =>
            {
                emp.Add(empFactory.GetEmployeeFromEntity(x));
            });

            if (withPay)
                emp.ForEach(x => x.TotalPay = x.CalculatePay());

            return emp;
        }


    }

    
}
