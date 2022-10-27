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

        //Get Form
        [Route("~/forms/getform/{expenseid}")]
        public async Task<JsonResult> GetForm(int expenseid)
        {
            var result = await formRepository.GetForm(expenseid);
            return Json(result);
        }


        //Add New Form
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


        //Edit Form
        [Route("~/Forms/EditForm/{formid}")]
        public JsonResult EditExpense(int formid)
        {
            HttpContext.Session.SetString("FormID", formid.ToString());
            return Json(formid);
        }

        //Form Call for Edit
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


        //Upload Attachment
        [HttpPost]
        public JsonResult SingleUpload(FileVM files)
        {
            var file = files.Attachments;
            var bytes = new byte[file.OpenReadStream().Length + 1];
            file.OpenReadStream().Read(bytes, 0, bytes.Length);

            var result = formRepository.SingleUpload(file.FileName, bytes);
            return Json(result);
        }

        //Get Attachment
        [HttpGet]
        [Route("/Forms/Getatc/{imgid}")]
        public async Task<JsonResult> Getatc(int imgid)
        {
            var result = await formRepository.Getatc(imgid);
            return Json(result);
        }

    }
}
