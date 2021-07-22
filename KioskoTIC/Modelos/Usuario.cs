using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KioskoTIC.Modelos
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        public string User { get; set; }

        public string Contrasenia { get; set; }

        public string Nombre { get; set; }

        public int NivelId { get; set; }

        public Nivel Nivel { get; set; }
    }
}
