using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReimbursementSystemAPI.Models;
using ReimbursementSystemAPI.ViewModel;
using ReimbursementSystemClient.Base.Urls;
using ReimbursementSystemClient.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReimbursementSystemClient.Repository.Data
{
    public class FormRepository : GeneralRepository<Form, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public FormRepository(Address address, string request = "Forms/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public async Task<List<FormVM>> GetForm(int expenseid)
        {
            List<FormVM> entities = new List<FormVM>();

            using (var response = await httpClient.GetAsync(request + "FormData/" + expenseid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<FormVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<AttachmentsVM> Getatc(int imgid)
        {
            AttachmentsVM entities = null;

            using (var response = await httpClient.GetAsync(request + "Getatc/" + imgid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<AttachmentsVM>(apiResponse);
            }
            return entities;
        }

        public HttpStatusCode InsertForm(FormVM entity, string expenseid)
        {
            entity.ExpenseId = Int32.Parse(expenseid);
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "InsertForm", content).Result;
            return result.StatusCode;
        }

        public async Task<TotalVM> TotalExpenseForm(int expenseid)
        {
            TotalVM entities = null;

            using (var response = await httpClient.GetAsync(request + "TotalExpense/" + expenseid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<TotalVM>(apiResponse);
            }
            return entities;
        }

        public HttpStatusCode PutEditFrom(FormVM entity, int formid)
        {
            entity.FormId = formid;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + "FormUpdate", content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode SingleUpload(string fileName, byte[] bytes)
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(bytes);
            multipartFormDataContent.Add(fileContent, "file", fileName);
            var result = httpClient.PostAsync("Attachments/" + "singleupload/", multipartFormDataContent).Result;
            return result.StatusCode;
        }

    }
}
