using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.ViewModel
{
    public class FileVM
    {
        public IFormFile Attachments { get; set; }
    }
}
