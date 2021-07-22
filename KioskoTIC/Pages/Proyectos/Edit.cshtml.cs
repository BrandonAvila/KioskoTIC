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

namespace KioskoTIC.Pages.Proyectos
{
    public class EditModel : PageModel
    {
        private readonly KioskoTIC.Datos.ContextoBD _context;

        public EditModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        [BindProperty]
        public Proyecto Proyecto { get; set; }

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

            Proyecto = await _context.Proyectos
                .Include(p => p.Carrera).FirstOrDefaultAsync(m => m.ProyectoId == id);

            if (Proyecto == null)
            {
                return NotFound();
            }
           ViewData["CarreraId"] = new SelectList(_context.Carreras, "CarreraId", "NombreCarrera");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile ImagenProyecto, IFormFile ImagenGaleria1, IFormFile ImagenGaleria2, IFormFile ImagenGaleria3, string NombrePro, string DescripcionPro, string URLPro, int CarreraPertenece, int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Proyecto = await _context.Proyectos.FirstOrDefaultAsync(m => m.ProyectoId == id);

            if (ImagenProyecto != null && ImagenGaleria1 != null && ImagenGaleria2 != null && ImagenGaleria3 != null)
            {
                BorrarImagenProyecto("ImagenesDeProyectos", Proyecto.IamgenProyecto);
                BorrarImagenProyecto("ImagenesDeProyectos", Proyecto.Iamgen1);
                BorrarImagenProyecto("ImagenesDeProyectos", Proyecto.Imagen2);
                BorrarImagenProyecto("ImagenesDeProyectos", Proyecto.Imagen3);
                Proyecto.IamgenProyecto = SubirImagenProyectos("ImagenesDeProyectos", ImagenProyecto);
                Proyecto.Iamgen1 = SubirImagenProyectos("ImagenesDeProyectos", ImagenGaleria1);
                Proyecto.Imagen2 = SubirImagenProyectos("ImagenesDeProyectos", ImagenGaleria2);
                Proyecto.Imagen3 = SubirImagenProyectos("ImagenesDeProyectos", ImagenGaleria3);
            }

            Proyecto.NombreProyecto = NombrePro;
            Proyecto.DescripcionProyecto = DescripcionPro;
            Proyecto.URLVideo = URLPro;

            _context.Attach(Proyecto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExists(Proyecto.ProyectoId))
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

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.ProyectoId == id);
        }
        public void BorrarImagenProyecto(string rutaCarpeta, string NombreArchivoBorrar)
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

        private string SubirImagenProyectos(string rutaCarpeta, IFormFile archivoAsubir)
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
