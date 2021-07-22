using KioskoTIC.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KioskoTIC.Datos
{
    public class ContextoBD : DbContext
    {
        public ContextoBD(DbContextOptions<ContextoBD> options) : base(options)
        {
        }

        public DbSet<Division> Divisiones { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Nivel> Niveles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
    }
}
