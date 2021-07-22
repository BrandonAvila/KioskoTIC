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

namespace KioskoTIC.Pages.Proyectos
{
    public class InfoProyectosModel : PageModel
    {
        private readonly KioskoTIC.Datos.ContextoBD _context;

        public InfoProyectosModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        public IList<Proyecto> Proyecto { get;set; }

        public async Task OnGetAsync(int? id)
        {
            Proyecto = await _context.Proyectos.Where(p => p.ProyectoId == id && id == p.ProyectoId)
                .Include(p => p.Carrera).ToListAsync();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                Response.Redirect("../Login");
            }
        }
    }
}
