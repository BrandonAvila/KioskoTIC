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

namespace KioskoTIC.Pages.Carreras
{
    public class IndexModel : PageModel
    {
        private readonly KioskoTIC.Datos.ContextoBD _context;

        public IndexModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        public IList<Carrera> Carrera { get;set; }

        public async Task OnGetAsync()
        {
            Carrera = await _context.Carreras
                .Include(c => c.Division).ToListAsync();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                Response.Redirect("../Login");
            }
        }
    }
}
