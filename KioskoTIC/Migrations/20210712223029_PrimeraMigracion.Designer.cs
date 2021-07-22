﻿// <auto-generated />
using KioskoTIC.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KioskoTIC.Migrations
{
    [DbContext(typeof(ContextoBD))]
    [Migration("20210712223029_PrimeraMigracion")]
    partial class PrimeraMigracion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KioskoTIC.Modelos.Carrera", b =>
                {
                    b.Property<int>("CarreraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DescripcionCarrera")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DivisionId")
                        .HasColumnType("int");

                    b.Property<string>("NombreCarrera")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarreraId");

                    b.HasIndex("DivisionId");

                    b.ToTable("Carreras");
                });

            modelBuilder.Entity("KioskoTIC.Modelos.Division", b =>
                {
                    b.Property<int>("DivisionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DescripcionDivision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreDivision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DivisionId");

                    b.ToTable("Divisiones");
                });

            modelBuilder.Entity("KioskoTIC.Modelos.Nivel", b =>
                {
                    b.Property<int>("NivelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NombreNivel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NivelId");

                    b.ToTable("Niveles");
                });

            modelBuilder.Entity("KioskoTIC.Modelos.Proyecto", b =>
                {
                    b.Property<int>("ProyectoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarreraId")
                        .HasColumnType("int");

                    b.Property<string>("DescripcionProyecto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreProyecto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProyectoId");

                    b.HasIndex("CarreraId");

                    b.ToTable("Proyectos");
                });

            modelBuilder.Entity("KioskoTIC.Modelos.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contrasenia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NivelId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.HasIndex("NivelId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("KioskoTIC.Modelos.Carrera", b =>
                {
                    b.HasOne("KioskoTIC.Modelos.Division", "Division")
                        .WithMany("Carreras")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KioskoTIC.Modelos.Proyecto", b =>
                {
                    b.HasOne("KioskoTIC.Modelos.Carrera", "Carrera")
                        .WithMany("Proyectos")
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KioskoTIC.Modelos.Usuario", b =>
                {
                    b.HasOne("KioskoTIC.Modelos.Nivel", "Nivel")
                        .WithMany("Usuarios")
                        .HasForeignKey("NivelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
