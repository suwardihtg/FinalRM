using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using ReimbursementSystemAPI.Models;

namespace ReimbursementSystemAPI.Models

{
    
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }

        [Required, Index(IsUnique = true)]
        public string NIK { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required, Index(IsUnique = true)]
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public float? Salary { get; set; }

        public string ManagerId { get; set; }

        [Required, Index(IsUnique = true)]
        public string Email { get; set; }
        public Gender Gender { get; set; }

        [JsonIgnore]
        public virtual Account Accounts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Expense> Expenses { get; set; }

     

        [JsonIgnore]
        public virtual Department Departments { get; set; }
        public int? DepartmentId { get; set; }

        [JsonIgnore]
        public virtual Job Jobs { get; set; }
        public int? JobId { get; set; }

    }

    public enum Gender
    {
        Male,
        Female
    }
}
