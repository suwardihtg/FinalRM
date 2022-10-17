using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.ViewModel
{
    public class RegisterVM
    {
        public string EmployeeId { get; set; }
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public float Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ManagerId { get; set; }
        public string Degree { get; set; }
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public int ReligionId { get; set; }
    }

}
