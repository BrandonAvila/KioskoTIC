using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KioskoTIC.Datos;
using KioskoTIC.Modelos;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace KioskoTIC.Pages.Carreras
{
    public class EditModel : PageModel
    {
        private readonly KioskoTIC.Datos.ContextoBD _context;

        public EditModel(KioskoTIC.Datos.ContextoBD context)
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
           ViewData["DivisionId"] = new SelectList(_context.Divisiones, "DivisionId", "NombreDivision");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile PDF, int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Carrera = await _context.Carreras.FirstOrDefaultAsync(m => m.CarreraId == id);

            if (PDF != null)
            {
                BorrarPDF("Archivos", Carrera.archivopdf);
                Carrera.archivopdf = SubirPDF("Archivos", PDF);
            }

            _context.Attach(Carrera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(Carrera.CarreraId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CarreraExists(int id)
        {
            return _context.Carreras.Any(e => e.CarreraId == id);
        }

        public void BorrarPDF(string rutaCarpeta, string NombreArchivoBorrar)
        {
            //Preparar la carpeta donde se copiara
            string Carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", rutaCarpeta);
            string RutaArchivoDeNombreUnico = Path.Combine(Carpeta, NombreArchivoBorrar);

            FileInfo informacionArchivo = new FileInfo(RutaArchivoDeNombreUnico);
            if (informacionArchivo.Exists)
            {
                informacionArchivo.Delete();
            }

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
