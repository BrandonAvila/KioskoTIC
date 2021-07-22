using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KioskoTIC.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KioskoTIC.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string usuario { get; set; }

        [BindProperty]
        public string Contrasenia { get; set; }



        private readonly KioskoTIC.Datos.ContextoBD _context;

        public LoginModel(KioskoTIC.Datos.ContextoBD context)
        {
            _context = context;
        }

        public Usuario Usuario { get; set; }


        public void OnGet()
        {

        }

        public void OnGetCerrar()
        {
            HttpContext.Session.Clear(); //Destruir la sesion para que me vuelva a pedir el login
            Response.Redirect("Login"); //Redirecciono al OnGet o principal
        }
        public void OnPost()
        {

            Usuario = _context.Usuarios
                    .Where(p => p.User == usuario && p.Contrasenia == Contrasenia).FirstOrDefault<Usuario>();

            //Aqui pondias los datos de tu base de datos
            if (Usuario != null)
            {
                //Crear una variable de sesion que se llame usuario
                HttpContext.Session.SetString("usuario", Usuario.User);

                //Crear una variable de sesion que se llame Contraseña
                HttpContext.Session.SetString("contraseña", Contrasenia);

                //Crear una variable de sesion que se llame Admin
                HttpContext.Session.SetString("Nivel", Usuario.NivelId.ToString());

                //Redireccionar a una pagina en especifico
                Response.Redirect("Index");
            }

        }
    }
}

