﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KioskoTIC.Datos;
using KioskoTIC.Modelos;
using Microsoft.AspNetCore.Http;

namespace KioskoTIC.Pages.Niveles
{
    public class DetailsModel : PageModel
    {
        private readonly KioskoTIC.Datos.ContextoBD _context;

        public DetailsModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        public Nivel Nivel { get; set; }

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

            Nivel = await _context.Niveles.FirstOrDefaultAsync(m => m.NivelId == id);

            if (Nivel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
