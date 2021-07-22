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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace KioskoTIC.Pages.Divisiones
{
    public class EditModel : PageModel
    {
        private readonly KioskoTIC.Datos.ContextoBD _context;

        public EditModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        [BindProperty]
        public Division Division { get; set; }

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

            Division = await _context.Divisiones.FirstOrDefaultAsync(m => m.DivisionId == id);

            if (Division == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile archivos, int? id, string NombreDiv, string DescripcionDiv)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Division = await _context.Divisiones.FirstOrDefaultAsync(m => m.DivisionId == id);

            if (archivos != null)
            {
                borrarArchivo("img", Division.ProfilePicture);
                Division.ProfilePicture = subirImagen("img", archivos);
            }
            Division.NombreDivision = NombreDiv;
            Division.DescripcionDivision = DescripcionDiv;

            _context.Attach(Division).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DivisionExists(Division.DivisionId))
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

        private bool DivisionExists(int id)
        {
            return _context.Divisiones.Any(e => e.DivisionId == id);
        }

        public void borrarArchivo(string rutaCarpeta, string NombreArchivoBorrar)
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

        private string subirImagen(string rutaCarpeta, IFormFile archivoAsubir)
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
