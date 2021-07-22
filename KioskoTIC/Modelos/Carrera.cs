using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KioskoTIC.Modelos
{
    public class Carrera
    {
        public int CarreraId { get; set; }

        public string NombreCarrera { get; set; }

        public string DescripcionCarrera { get; set; }

        public int DivisionId { get; set; }

        public Division Division { get; set; }

        public List<Proyecto> Proyectos { get; set; }

        public string archivopdf { get; set; }

    }
}
