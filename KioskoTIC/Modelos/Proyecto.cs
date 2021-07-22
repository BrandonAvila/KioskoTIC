using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KioskoTIC.Modelos
{
    public class Proyecto
    {
        public int ProyectoId { get; set; }

        public string IamgenProyecto { get; set; }

        public string NombreProyecto { get; set; }

        public string DescripcionProyecto { get; set; }

        public string URLVideo { get; set; }

        public string Iamgen1 { get; set; }

        public string Imagen2 { get; set; }

        public string Imagen3 { get; set; }

        public int CarreraId { get; set; }

        public Carrera Carrera { get; set; }
    }
}
