using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementSystemAPI.Base;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.Repository.Data;
using System.Text.RegularExpressions;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Controllers
{
    [Route("api/Attachments")]
    [ApiController]
    public class Employee_AttachmentsController : BaseController<Employee_Attachment, Employee_AttachmentRepository, string>
    {
        private Employee_AttachmentRepository employee_attachmentRepository;
        public Employee_AttachmentsController(Employee_AttachmentRepository repository) : base(repository)
        {
            this.employee_attachmentRepository = repository;
        }

        [HttpPost("singleupload")]
        public ActionResult SingleUpload(IFormFile file)
        {
            try
            {
                string newString = "";
                newString = Regex.Replace(file.FileName, @"\s+", "_");
                var filePath = Path.Combine("C:/Users/Gigabyte/source/repos/ReimbursementSystem/ReimbursementSystemAPI/wwwroot/Images/", newString);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
