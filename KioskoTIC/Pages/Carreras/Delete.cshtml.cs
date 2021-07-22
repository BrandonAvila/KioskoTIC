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
    public class DeleteModel : PageModel
    {
        private readonly KioskoTIC.Datos.ContextoBD _context;

        public DeleteModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        [BindProperty]
        public Carrera Carrera { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                Response.Redirect("../Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            Carrera = await _context.Carreras
                .Include(c => c.Division).FirstOrDefaultAsync(m => m.CarreraId == id);

            if (Carrera == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Carrera = await _context.Carreras.FindAsync(id);

            if (Carrera != null)
            {
                _context.Carreras.Remove(Carrera);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
