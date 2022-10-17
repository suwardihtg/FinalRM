using API.Hash;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;
        public IConfiguration _configuration;

        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.context = myContext;
            this._configuration = configuration;
        }

        public int Register(RegisterVM registerVM)
        {
            Employee employee = new Employee();
            {
                employee.EmployeeId = registerVM.EmployeeId;
                employee.NIK = registerVM.NIK;
                employee.FirstName = registerVM.FirstName;
                employee.LastName = registerVM.LastName;
                employee.Gender = (registerVM.Gender == "Male") ? Gender.Male : Gender.Female;
                employee.BirthDate = registerVM.BirthDate;
                employee.Email = registerVM.Email;
                employee.Salary = registerVM.Salary;
                employee.Phone = registerVM.Phone;
                employee.ManagerId = registerVM.ManagerId;
                employee.DepartmentId = employee.DepartmentId;
                employee.JobId = registerVM.JobId;
            }

            Account account = new Account();
            {
                account.EmployeeId = registerVM.EmployeeId;
                account.Password = Hashing.HashPassword(registerVM.Password);
            }

            context.Employees.Add(employee);
            context.Accounts.Add(account);
            context.SaveChanges();
            return 1;
        }

        public int Login(LoginVM loginVM)
        {
            var dataPass = (from a in context.Employees where a.Email == loginVM.Email
                            join b in context.Accounts on a.EmployeeId equals b.EmployeeId
                            select new { Account = b, Employee = a }).FirstOrDefault();

            if (dataPass == null)
            {
                return 4;
            }
            else if (dataPass.Employee.Email != null)
            {
                var cekPassword = Hashing.ValidatePassword(loginVM.Password, dataPass.Account.Password);
                if (cekPassword == true)
                {
                    return 1;
                }
                return 2;
            }
            return 3;

        }
    }
}
