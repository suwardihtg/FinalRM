using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using ReimbursementSystemClient.Base.Controllers;
using ReimbursementSystemClient.Repository.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.accountRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await accountRepository.Auth(login);
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken.Token);
            var email = token.Claims.First(c => c.Type == "email").Value;
            var id = token.Claims.First(c => c.Type == "nameid").Value;
            var role = token.Claims.First(c => c.Type == "unique_name").Value;

            if (jwtToken.Token == null)
            {
                return Json(Url.Action("login", "Home"));
            }


            HttpContext.Session.SetString("JWToken", jwtToken.Token);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("EmployeeId", id);
            HttpContext.Session.SetString("Role", role);

            return Json(Url.Action("Reimbusment", "Reimbusments"));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public JsonResult GetRole()
        {
            var sessionRole = HttpContext.Session.GetString("Role");
            return Json(sessionRole);
        }

    }
}
