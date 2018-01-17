using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeDAL
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        IRepository<EmployeeEntity> EmpRepository { get; }
        IRepository<PermEmployeeEntity> PermEmpRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<EmployeeEntity> _empRepository;
        private IRepository<PermEmployeeEntity> _permEmpRepository;
        private string filePath;

        private List<EmployeeEntity> Employees;
        private List<PermEmployeeEntity> PermEmployees;

        public UnitOfWork()
        {
            //Hardcoding file path.
            string appPath = HttpRuntime.BinDirectory;

            // check if DB fle exists. Else create file.
            if (!Directory.Exists(appPath + "\\DBFiles"))
            {
                Directory.CreateDirectory(appPath + "\\DBFiles");
            }

            filePath = appPath + "\\DBFiles\\Employee.json";
            if (!File.Exists(filePath))
            {
                var stream = File.Create(filePath);
                stream.Close();
            }

            var responseString = File.ReadAllText(filePath);

            if (!string.IsNullOrEmpty(responseString))
            {
                var asd = (JObject)JsonConvert.DeserializeObject(responseString);
                
                Employees = asd["Employees"].ToObject<List<EmployeeEntity>>();
                PermEmployees = asd["PermEmployees"].ToObject<List<PermEmployeeEntity>>();
            }

            if(Employees == null)
                Employees = new List<EmployeeEntity>();

            if (PermEmployees == null)
                PermEmployees = new List<PermEmployeeEntity>();
        }

        public IRepository<EmployeeEntity> EmpRepository
        {
            get            
            {
                if (_empRepository == null)
                    _empRepository = new EmployeeRepository(Employees);

                return _empRepository;

            }
            private set { }
        }

        public IRepository<PermEmployeeEntity> PermEmpRepository
        {
            get
            {
                if (_permEmpRepository == null)
                    _permEmpRepository = new PermEmployeeRepository(PermEmployees);

                return _permEmpRepository;

            }
            private set { }
        }


        public void SaveChanges()
        {
            using (Stream file = File.Create(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();

                using (var textWriter = new StreamWriter(file))
                {
                    serializer.Serialize(textWriter, new { Employees = Employees, PermEmployees = PermEmployees });
                }

            }
        }

    }
}
