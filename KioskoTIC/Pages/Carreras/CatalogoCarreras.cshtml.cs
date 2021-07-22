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

namespace KioskoTIC.Pages
{
    public class CatalogoCarrerasModel : PageModel
    {

        private readonly KioskoTIC.Datos.ContextoBD _context;

        public CatalogoCarrerasModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        public IList<Carrera> Carrera { get;set; }

        public async Task OnGetAsync(int? id)
        {
            Carrera = await _context.Carreras.Where(c => c.DivisionId == id && id == c.DivisionId)
                .Include(c => c.Division).ToListAsync();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                Response.Redirect("../Login");
            }
        }
    }
}
