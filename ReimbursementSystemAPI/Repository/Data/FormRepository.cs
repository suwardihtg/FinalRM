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
                form.RequestDate = fromVM.RequestDate;
                form.StartDate = fromVM.StartDate;
                form.EndDate = fromVM.EndDate;
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
                form.Description = fromVM.Description;
                form.Total = fromVM.Total;
                form.ExpenseId = fromVM.ExpenseId;
                form.AccountNumber = fromVM.AccountNumber;
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
                               RequestDate = b.RequestDate,
                               Total = b.Total,
                               Category = (int)b.Category,
                               Description = b.Description,
                               AccountNumber = b.AccountNumber,
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
            form.RequestDate = fromVM.RequestDate;
            form.StartDate = fromVM.StartDate;
            form.EndDate = fromVM.EndDate;
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
            form.Description = fromVM.Description;
            form.Total = fromVM.Total;
            form.AccountNumber = fromVM.AccountNumber;
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
