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
using System.IO;

namespace KioskoTIC.Pages.Proyectos
{
    public class CreateAlumnoModel : PageModel
    {
        private readonly KioskoTIC.Datos.ContextoBD _context;

        public CreateAlumnoModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CarreraId"] = new SelectList(_context.Carreras, "CarreraId", "NombreCarrera");
            return Page();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                Response.Redirect("../Login");
            }
        }

        [BindProperty]
        public Proyecto Proyecto { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile ImagenProyecto, IFormFile ImagenGaleria1, IFormFile ImagenGaleria2, IFormFile ImagenGaleria3)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            Proyecto.IamgenProyecto = SubirImagenProyectos("ImagenesDeProyectos", ImagenProyecto);
            Proyecto.Iamgen1 = SubirImagenProyectos("ImagenesDeProyectos", ImagenGaleria1);
            Proyecto.Imagen2 = SubirImagenProyectos("ImagenesDeProyectos", ImagenGaleria2);
            Proyecto.Imagen3 = SubirImagenProyectos("ImagenesDeProyectos", ImagenGaleria3);
            _context.Proyectos.Add(Proyecto);

            _context.Proyectos.Add(Proyecto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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
