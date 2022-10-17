using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    public class MyContext : DbContext
    {
        public IConfiguration _iconfiguration;
        public MyContext(DbContextOptions<MyContext> options, IConfiguration configuration) : base(options)
        {
        
            this._iconfiguration = configuration;
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Department> Departements { get; set; }
        public DbSet<Employee_Attachment> Employee_Attachments { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ExpenseHistory> ExpenseHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to One
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Accounts)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.EmployeeId);

            ////One to many
            modelBuilder.Entity<Employee>()
                .HasMany(c => c.Expenses)
                .WithOne(c => c.Employees);

            ////One to many
            modelBuilder.Entity<Expense>()
                .HasMany(c => c.ExpenseHistories)
                .WithOne(c => c.Expenses);

            //One to many
            modelBuilder.Entity<Expense>()
                .HasMany(c => c.Forms)
                .WithOne(c => c.Expenses);

            ////One to many
            modelBuilder.Entity<Department>()
                .HasMany(c => c.Employees)
                .WithOne(c => c.Departments);

            //One to many
            modelBuilder.Entity<Job>()
                .HasMany(c => c.Employees)
                .WithOne(c => c.Jobs);
        }
    }

    class CustomResolver : DefaultContractResolver
    {
        private readonly List<string> _namesOfVirtualPropsToKeep = new List<string>(new String[] { });

        public CustomResolver() { }

        public CustomResolver(IEnumerable<string> namesOfVirtualPropsToKeep)
        {
            this._namesOfVirtualPropsToKeep = namesOfVirtualPropsToKeep.Select(x => x.ToLower()).ToList();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            var propInfo = member as PropertyInfo;
            if (propInfo != null)
            {
                if (propInfo.GetMethod.IsVirtual && !propInfo.GetMethod.IsFinal
                    && !_namesOfVirtualPropsToKeep.Contains(propInfo.Name.ToLower()))
                {
                    prop.ShouldSerialize = obj => false;
                }
            }
            return prop;
        }


    }
}

