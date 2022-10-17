using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using ReimbursementSystemClient.Base.Urls;
using ReimbursementSystemClient.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Repository.Data
{
    public class ExpenseRepository : GeneralRepository<Expense, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public ExpenseRepository(Address address, string request = "Expenses/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<ExpenseVM>> GetExpense(string employeeid)
        {  
            List<ExpenseVM> entities = new List<ExpenseVM>();

            using (var response = await httpClient.GetAsync(request + "ExpenseData/" + employeeid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ExpenseVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<ExpenseIDVM> GetID(string email)
        {
            ExpenseIDVM entities = null;

            using (var response = await httpClient.GetAsync(request + "GetID/" + email))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<ExpenseIDVM>(apiResponse);
            }
            return entities;
        }

        public HttpStatusCode NewExpense(ExpenseVM entity, string employeeId)
        {
            entity.EmployeeId = employeeId;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "ExpenseInsert", content).Result;

            return result.StatusCode;
        }

        public HttpStatusCode Submit(ExpenseVM entity, string employeeId, int code)
        {
            entity.EmployeeId = employeeId;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + "ExpenseUpdate/" + code, content).Result;
            return result.StatusCode;
        }

        public async Task<List<ExpenseHistory>> GetHistory(int expenseId)
        {
            List<ExpenseHistory> entitiesNew = new List<ExpenseHistory>();

            using (var response = await httpClient.GetAsync("expenseHistories/" + "History/" + expenseId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entitiesNew = JsonConvert.DeserializeObject<List<ExpenseHistory>>(apiResponse);

            }
            return entitiesNew;
        }

        //<!----------------- Finances ------------------->
        public async Task<List<ExpenseManager>> GetExpenseFinance()
        {
            List<ExpenseManager> entitiesNew = new List<ExpenseManager>();

            using (var response = await httpClient.GetAsync(request + "ExpenseDataFinances/" ))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entitiesNew = JsonConvert.DeserializeObject<List<ExpenseManager>>(apiResponse);

            }
            return entitiesNew;
        }

        public async Task<List<ExpenseManager>> GetExpenseFinanceReject()
        {
            List<ExpenseManager> entitiesNew = new List<ExpenseManager>();

            using (var response = await httpClient.GetAsync(request + "ExpenseDataFinancesReject/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entitiesNew = JsonConvert.DeserializeObject<List<ExpenseManager>>(apiResponse);

            }
            return entitiesNew;
        }

        public HttpStatusCode ApprovalFinance(ExpenseVM entity, string email, int code)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + "Approval/" + email + "/" + code, content).Result;
            return result.StatusCode;
        }


        //<!----------------- Manager ------------------->

        public async Task<List<ExpenseManager>> GetExpenseManager()
        {
            List<ExpenseManager> entitiesNew = new List<ExpenseManager>();

            using (var response = await httpClient.GetAsync(request + "ExpenseDataManager/" ))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entitiesNew = JsonConvert.DeserializeObject<List<ExpenseManager>>(apiResponse);
            }
            return entitiesNew;
        }

        public async Task<List<ExpenseManager>> GetExpenseSManager()
        {
            List<ExpenseManager> entitiesNew = new List<ExpenseManager>();

            using (var response = await httpClient.GetAsync(request + "ExpenseDataSManager/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entitiesNew = JsonConvert.DeserializeObject<List<ExpenseManager>>(apiResponse);
            }
            return entitiesNew;
        }

        public async Task<List<ExpenseManager>> GetExpenseDirector()
        {
            List<ExpenseManager> entitiesNew = new List<ExpenseManager>();

            using (var response = await httpClient.GetAsync(request + "ExpenseDataDirector/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entitiesNew = JsonConvert.DeserializeObject<List<ExpenseManager>>(apiResponse);
            }
            return entitiesNew;
        }

        public async Task<List<ExpenseManager>> GetExpenseManagerReject()
        {
            List<ExpenseManager> entitiesNew = new List<ExpenseManager>();

            using (var response = await httpClient.GetAsync(request + "GetExpenseManagerReject"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entitiesNew = JsonConvert.DeserializeObject<List<ExpenseManager>>(apiResponse);

            }
            return entitiesNew;
        }

        public HttpStatusCode Approval(ExpenseVM entity, int code)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + "Approval/" + code, content).Result;
            return result.StatusCode;
        }


        //<!----------------- Manager & Finances -------------------> 

        public async Task<List<ExpenseManager>> GetExpensePosted()
        {
            List<ExpenseManager> entitiesNew = new List<ExpenseManager>();

            using (var response = await httpClient.GetAsync(request + "GetExpensePosted/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entitiesNew = JsonConvert.DeserializeObject<List<ExpenseManager>>(apiResponse);

            }
            return entitiesNew;
        }


    }
}
