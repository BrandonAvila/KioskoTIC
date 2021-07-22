using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KioskoTIC.Datos;
using KioskoTIC.Modelos;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace KioskoTIC.Pages.Carreras
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

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                Response.Redirect("../Login");
            }

            ViewData["DivisionId"] = new SelectList(_context.Divisiones, "DivisionId", "NombreDivision");
            return Page();
        }

        [BindProperty]
        public Carrera Carrera { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile PDF)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Carrera.archivopdf = SubirPDF("Archivos", PDF);
            _context.Carreras.Add(Carrera);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private string SubirPDF(string rutaCarpeta, IFormFile archivoAsubir)
        {
            //Preparar la carpeta donde se copiara
            string Carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", rutaCarpeta);
            //Archivo unico para no repetir
            string NombreUnicoArchivo = Guid.NewGuid().ToString() + "_" + archivoAsubir.FileName;
            //Union con la carpeta con el archivo unico
            string RutaArchivoDeNombreUnico = Path.Combine(Carpeta, NombreUnicoArchivo);



            //Copiar al archivo una vez que ya tengo la ruta y el nombre del archivo
            using (var InfoArchivoACrear = new FileStream(RutaArchivoDeNombreUnico, FileMode.Create))
            {
                //Mis sueños hechos realidad
                archivoAsubir.CopyTo(InfoArchivoACrear);
            }

            return NombreUnicoArchivo;
        }
    }
}
