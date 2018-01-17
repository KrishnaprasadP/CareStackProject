
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity;
using Unity.AspNet.Mvc;
using EmployeeDAL;
using EmployeeOperations;

namespace DITest
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            
            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IRepository<EmployeeEntity>, EmployeeRepository>();
            container.RegisterType<IRepository<PermEmployeeEntity>, PermEmployeeRepository>();
            container.RegisterType<IEmpOperations, EmpOperations>();
        }
    }
}