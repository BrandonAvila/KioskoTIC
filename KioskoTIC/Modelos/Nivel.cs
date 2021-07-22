using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KioskoTIC.Modelos
{
    public class Nivel
    {
        public int NivelId { get; set; }

        public string NombreNivel { get; set; }

        public List<Usuario> Usuarios { get; set; }

    }
}
