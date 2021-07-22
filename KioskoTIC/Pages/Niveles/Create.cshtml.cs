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

namespace KioskoTIC.Pages.Niveles
{
    public class CreateModel : PageModel
    {
        private readonly KioskoTIC.Datos.ContextoBD _context;

        public CreateModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Nivel Nivel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                Response.Redirect("../Login");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Niveles.Add(Nivel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
