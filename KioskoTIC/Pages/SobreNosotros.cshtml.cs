using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KioskoTIC.Pages
{
    public class SobreNosotrosModel : PageModel
    {
        public void OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                Response.Redirect("Login");
            }
        }
    }
}
