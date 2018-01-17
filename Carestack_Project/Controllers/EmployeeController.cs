using EmployeeDAL;
using EmployeeModels;
using EmployeeOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace Carestack_Project.Controllers
{
    public class EmployeeController : Controller
    {
        IEmpOperations _empOps;
        EmployeeFactory empFactory;
                
        public EmployeeController(IEmpOperations empOps)
        {
            _empOps = empOps;
            empFactory = new EmployeeFactory();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddEmployeeViewModel employee)
        {
            bool isAddedSuccessfully = false;
            var emp = empFactory.GetEmployeeFromAddVM(employee);
            if (employee.IsPermanent)
            {                
                isAddedSuccessfully = _empOps.AddPermEmployee(emp);
            }
            else
            {
                isAddedSuccessfully = _empOps.AddEmployee(emp);
            }

            return View();
        }

        [HttpGet]
        public ActionResult GetWithoutPay()
        {
            IEnumerable<IPermEmployee> permEmp = _empOps.GetPermEmployees(false);
            IEnumerable<IEmployee> tempEmp = _empOps.GetTempEmployees(false);

            var allEmp = new List<GetEmployeeWithoutPayViewModel>();
            permEmp.ToList().ForEach(x =>
            {
                allEmp.Add(empFactory.GetEmployeeWithoutPayVMFromEmployee(x, true));
            });

            tempEmp.ToList().ForEach(x =>
            {
                allEmp.Add(empFactory.GetEmployeeWithoutPayVMFromEmployee(x));
            });
            
            return View(allEmp);
        }

        [HttpGet]
        public ActionResult GetWithPay()
        {
            IEnumerable<IPermEmployee> permEmp = _empOps.GetPermEmployees(true);
            IEnumerable<IEmployee> tempEmp = _empOps.GetTempEmployees(true);

            var allEmp = new List<GetEmployeeWithPayViewModel>();
            permEmp.ToList().ForEach(x =>
            {
                x.TotalPay = x.CalculatePay();
                allEmp.Add(empFactory.GetEmployeeWithPayVMFromEmployee(x, true));
            });

            tempEmp.ToList().ForEach(x =>
            {
                x.TotalPay = x.CalculatePay();
                allEmp.Add(empFactory.GetEmployeeWithPayVMFromEmployee(x));
            });

            return View(allEmp);
        }
    }
}