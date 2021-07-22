using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KioskoTIC.Datos;
using KioskoTIC.Modelos;
using Microsoft.AspNetCore.Http;

namespace KioskoTIC.Pages.Divisiones
{
    public class IndexModel : PageModel
    {
        private readonly KioskoTIC.Datos.ContextoBD _context;

        public IndexModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        public IList<Division> Division { get;set; }

        public async Task OnGetAsync()
        {
            Division = await _context.Divisiones.ToListAsync();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                Response.Redirect("../Login");
            }
        }
    }
}
