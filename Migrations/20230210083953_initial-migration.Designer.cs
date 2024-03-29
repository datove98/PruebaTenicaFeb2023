﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PruebaTenicaFeb2023;

namespace PruebaTenicaFeb2023.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230210083953_initial-migration")]
    partial class initialmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("PruebaTenicaFeb2023.Models.Alumno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("FechaNac")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Alumnos");
                });

            modelBuilder.Entity("PruebaTenicaFeb2023.Models.AlumnoGrado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AlumnoId")
                        .HasColumnType("int");

                    b.Property<int>("GradoId")
                        .HasColumnType("int");

                    b.Property<string>("Seccion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("GradoId");

                    b.ToTable("AlumnosGrados");
                });

            modelBuilder.Entity("PruebaTenicaFeb2023.Models.Grado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfesorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfesorId");

                    b.ToTable("Grados");
                });

            modelBuilder.Entity("PruebaTenicaFeb2023.Models.Profesor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Profesores");
                });

            modelBuilder.Entity("PruebaTenicaFeb2023.Models.AlumnoGrado", b =>
                {
                    b.HasOne("PruebaTenicaFeb2023.Models.Alumno", "Alumno")
                        .WithMany("AlumnoGrados")
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PruebaTenicaFeb2023.Models.Grado", "Grado")
                        .WithMany("Alumnos")
                        .HasForeignKey("GradoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alumno");

                    b.Navigation("Grado");
                });

            modelBuilder.Entity("PruebaTenicaFeb2023.Models.Grado", b =>
                {
                    b.HasOne("PruebaTenicaFeb2023.Models.Profesor", "Profesor")
                        .WithMany("Grados")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("PruebaTenicaFeb2023.Models.Alumno", b =>
                {
                    b.Navigation("AlumnoGrados");
                });

            modelBuilder.Entity("PruebaTenicaFeb2023.Models.Grado", b =>
                {
                    b.Navigation("Alumnos");
                });

            modelBuilder.Entity("PruebaTenicaFeb2023.Models.Profesor", b =>
                {
                    b.Navigation("Grados");
                });
#pragma warning restore 612, 618
        }
    }
}
