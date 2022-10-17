using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReimbursementSystemAPI.Base;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.Repository.Data;
using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : BaseController<Form, FormRepository, int>
    {
        private FormRepository formRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;
        public FormsController(FormRepository repository, IConfiguration configuration, MyContext context) : base(repository)
        {
            this.formRepository = repository;
            this._configuration = configuration;
            this.context = context;
        }

        [HttpPost("InsertForm")]
        public ActionResult NewForm(FormVM fromVM)
        {
            var result = formRepository.NewForm(fromVM);
            switch (result)
            {
                case 1:
                    return Ok();
                default:
                    return BadRequest();
            }
        }

        [HttpGet("FormData/{expenseid}")]
        public ActionResult GetForm(int expenseid)
        {
            var result = formRepository.GetForm(expenseid);

            if (result.Count() != 0)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("TotalExpense/{expenseid}")]
        public ActionResult TotalExpenseForm(int expenseid)
        {
            var result = formRepository.TotalExpenseForm(expenseid);

            if (result != null)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpPut("FormUpdate")]
        public ActionResult FormUpdate(FormVM fromVM)
        {
            var result = formRepository.FormUpdate(fromVM);
            switch (result)
            {
                case 1:
                    return Ok();
                default:
                    return BadRequest();
            }
        }

        [HttpGet("Getatc/{imgid}")]
        public ActionResult Getatc(int imgid)
        {
            var result = formRepository.Getatc(imgid);

            if (result != null)
            {
                return Ok(result);
            }
            return Ok(result);
        }
    }

}
