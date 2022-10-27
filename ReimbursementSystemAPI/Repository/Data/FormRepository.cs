using API.Hash;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Repository.Data
{
    public class FormRepository : GeneralRepository<MyContext, Form, int>
    {
        private readonly MyContext context;
        public IConfiguration _configuration;
        public FormRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.context = myContext;
            this._configuration = configuration;
        }

        //Add New Form
        public int NewForm(FormVM formVM)
        {
            Employee_Attachment atc = new Employee_Attachment();
            {
                atc.FilePath = formVM.Attachments;
            }
            context.Employee_Attachments.Add(atc);
            context.SaveChanges();
            Form form = new Form();
            {
                form.RequestDate = formVM.RequestDate;
                form.StartDate = formVM.StartDate;
                form.EndDate = formVM.EndDate;
                form.Attachments = atc.Id;

                switch (formVM.Category)
                {
                    case 0:
                        form.Category = Category.Transportation;
                        break;
                    case 1:
                        form.Category = Category.Parking;
                        break;
                    case 2:
                        form.Category = Category.Medical;
                        break;
                    case 3:
                        form.Category = Category.Lodging;
                        break;
                    default:
                        break;
                }
                form.Description = formVM.Description;
                form.Total = formVM.Total;
                form.ExpenseId = formVM.ExpenseId;
                form.AccountNumber = formVM.AccountNumber;
                form.BankName = formVM.BankName;
            }
            context.Forms.Add(form);
            context.SaveChanges();
            return 1;
        }

        //Get Form
        public IEnumerable<FormVM> GetForm(int expenseid)
        {
            var register = from a in context.Expenses where a.ExpenseId == expenseid
                           join b in context.Forms on a.ExpenseId equals b.ExpenseId
                           join c in context.Employee_Attachments on b.Attachments equals c.Id
                           select new FormVM()
                           {
                               FormId = b.FormId,
                               RequestDate = b.RequestDate,
                               Total = b.Total,
                               Category = (int)b.Category,
                               Description = b.Description,
                               AccountNumber = b.AccountNumber,
                               BankName = b.BankName,
                               Attachments = c.FilePath
                           };
            return register.ToList();
        }

        //Edit Form
        public int FormUpdate(FormVM formVM)
        {
            var data = (from a in context.Forms where a.FormId == formVM.FormId
                        select new { form = a}).Single();
            var form = data.form;
            form.RequestDate = formVM.RequestDate;
            form.StartDate = formVM.StartDate;
            form.EndDate = formVM.EndDate;
            switch (formVM.Category)
            {
                case 0:
                    form.Category = Category.Transportation;
                    break;
                case 1:
                    form.Category = Category.Parking;
                    break;
                case 2:
                    form.Category = Category.Medical;
                    break;
                case 3:
                    form.Category = Category.Lodging;
                    break;
                default:
                    break;
            }
            form.Description = formVM.Description;
            form.Total = formVM.Total;
            form.AccountNumber = formVM.AccountNumber;
            form.BankName = formVM.BankName;

            context.SaveChanges();
            return 1;
        }

        public AttachmentsVM Getatc(int imgid)
        {
            var imgPath = (from a in context.Employee_Attachments where a.Id == imgid select a.FilePath).ToList();
            
            AttachmentsVM path = new AttachmentsVM();
            path.Name = imgPath[0].ToString();
            return path;
        }
    }
}
