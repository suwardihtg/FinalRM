using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class Employee_AttachmentRepository : GeneralRepository<MyContext, Employee_Attachment, string>
    {
        private readonly MyContext context;
        public Employee_AttachmentRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int Filepath(string filename)
        {
            Employee_Attachment atc = new Employee_Attachment();
            atc.FilePath = filename;
            context.Employee_Attachments.Add(atc);
            var a = context.SaveChanges();
            return a;
        }
    }
}
