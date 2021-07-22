using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KioskoTIC.Modelos
{
    public class Division
    {
        public int DivisionId { get; set; }

        public string ProfilePicture { get; set; }

        public string NombreDivision { get; set; }

        public string DescripcionDivision { get; set; }

        public List<Carrera> Carreras { get; set; }
    }
}
