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

        public int NewForm(FormVM fromVM)
        {
            Employee_Attachment atc = new Employee_Attachment();
            {
                atc.FilePath = fromVM.Attachments;
            }
            context.Employee_Attachments.Add(atc);
            context.SaveChanges();
            Form form = new Form();
            {
                form.Receipt_Date = fromVM.Receipt_Date;
                form.Start_Date = fromVM.Start_Date;
                form.End_Date = fromVM.End_Date;
                form.Attachments = atc.Id;

                switch (fromVM.Category)
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
                form.Payee = fromVM.Payee;
                form.Description = fromVM.Description;
                form.Total = fromVM.Total;
                form.ExpenseId = fromVM.ExpenseId;
            }

            context.Forms.Add(form);

            context.SaveChanges();
           
            return 1;
        }

        public IEnumerable<FormVM> GetForm(int expenseid)
        {
            var register = from a in context.Expenses where a.ExpenseId == expenseid
                           join b in context.Forms on a.ExpenseId equals b.ExpenseId
                           join c in context.Employee_Attachments on b.Attachments equals c.Id
                           select new FormVM()
                           {
                               FormId = b.FormId,
                               Receipt_Date = b.Receipt_Date,
                               Total = b.Total,
                               Payee = b.Payee,
                               Type = b.Type,
                               Category = (int)b.Category,
                               Description = b.Description,
                               Attachments = c.FilePath
        };

            return register.ToList();
        }

        public TotalVM TotalExpenseForm(int expenseid)
        {
            var sum = (from a in context.Expenses
                       where a.ExpenseId == expenseid
                       join b in context.Forms on a.ExpenseId equals b.ExpenseId
                       select b.Total.Value).Sum();

            TotalVM total = new TotalVM();
            total.Total = sum;
            return total;
        }

        public int FormUpdate(FormVM fromVM)
        {
            var data = (from a in context.Forms where a.FormId == fromVM.FormId
                        select new { form = a}).Single();
            var form = data.form;
            form.Receipt_Date = fromVM.Receipt_Date;
            form.Start_Date = fromVM.Start_Date;
            form.End_Date = fromVM.End_Date;
            switch (fromVM.Category)
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
            form.Payee = fromVM.Payee;
            form.Description = fromVM.Description;
            form.Total = fromVM.Total;
            //form.Attachments = fromVM.Attachments;
            ////form.ExpenseId = fromVM.ExpenseId;

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
