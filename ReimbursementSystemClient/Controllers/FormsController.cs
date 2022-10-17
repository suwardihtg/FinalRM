using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using ReimbursementSystemClient.Base.Controllers;
using ReimbursementSystemClient.Repository.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Controllers
{
    public class FormsController : BaseController<Form, FormRepository, string>
    {

        private readonly FormRepository formRepository;
        public IConfiguration _iconfiguration;
        public FormsController(FormRepository repository,  IConfiguration configuration) : base(repository)
        {
            this.formRepository = repository;
            this._iconfiguration = configuration;
        }

        //public IActionResult Index()
        //{

        //    List<Category> CategoryList = context.Categories1.ToList();
        //    ViewBag.CategoryList = new SelectList(CategoryList, "CategoryId", "CategoryName");
        //    return View();
        //}
        //public JsonResult GetTypeList(int CategoryId)
        //{
        //    //context._iconfiguration.ProxyCreationEnabled = false;
        //    List<Type> TypeList = context.Types.Where(x => x.CategoryId == CategoryId).ToList();
        //    return Json(TypeList);

        //}

        [Route("~/forms/getform/{expenseid}")]
        public async Task<JsonResult> GetForm(int expenseid)
        {
            var result = await formRepository.GetForm(expenseid);
            return Json(result);
        }

        [HttpPost]
        public JsonResult InsertForm(FormVM formVM)
        {
            var sessionExpense = HttpContext.Session.GetString("ExpenseID");
            var result = formRepository.InsertForm(formVM, sessionExpense);
            return Json(result);
        }

        public IActionResult NewForm()
        { 
            var sessionData = HttpContext.Session.GetString("FormID");

            if (sessionData != null)
            {
                HttpContext.Session.Remove("FormID");
                return Json(Url.Action("Form", "Reimbusments"));
            }
            return Json(Url.Action("Form", "Reimbusments"));
        }

        [Route("~/forms/TotalExpenseForm/{expenseid}")]
        public async Task<JsonResult> TotalExpenseForm(int expenseid)
        {
            var result = await formRepository.TotalExpenseForm(expenseid);
            return Json(result);
        }

        [Route("~/Forms/EditForm/{formid}")]
        public JsonResult EditExpense(int formid)
        {
            HttpContext.Session.SetString("FormID", formid.ToString());
            return Json(formid);
        }

        [Route("~/Forms/FormCall/")]
        public JsonResult EditFormCall()
        {
            var formSession = HttpContext.Session.GetString("FormID");
            return Json(formSession);
        }

        [HttpPut]
        public JsonResult PutEditFrom(FormVM entity)
        {
            var formSession = Int32.Parse(HttpContext.Session.GetString("FormID"));
            var result = formRepository.PutEditFrom(entity, formSession);
            return Json(result);
        }

        [HttpPost]
        public JsonResult SingleUpload(FileVM files)
        {
            var file = files.Attachments;
            var bytes = new byte[file.OpenReadStream().Length + 1];
            file.OpenReadStream().Read(bytes, 0, bytes.Length);

            var result = formRepository.SingleUpload(file.FileName, bytes);
            return Json(result);
        }

        [HttpGet]
        [Route("/Forms/Getatc/{imgid}")]
        public async Task<JsonResult> Getatc(int imgid)
        {
            var result = await formRepository.Getatc(imgid);
            return Json(result);
        }

    }
}
