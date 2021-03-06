using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KioskoTIC.Datos;
using KioskoTIC.Modelos;
using Microsoft.AspNetCore.Http;

namespace KioskoTIC.Pages
{
    public class RegistroModel : PageModel
    {
        private readonly KioskoTIC.Datos.ContextoBD _context;

        public RegistroModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            ViewData["NivelId"] = new SelectList(_context.Niveles, "NivelId", "NombreNivel");
            return Page();
        }

        [BindProperty]
        public Usuario Usuario { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Login");
        }
    }
}
