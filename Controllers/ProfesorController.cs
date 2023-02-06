﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTenicaFeb2023.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaTenicaFeb2023.Controllers
{
    public class ProfesorController : Controller
    {
        private DataContext context;
        public ProfesorController (DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("Profesores")]
        public async Task<IActionResult> Index()
        {
            List<Profesor> profesores = await context.Profesores.ToListAsync();

            return View("IndexProfesor", profesores);
        }

        [HttpGet]
        [Route("[controller]/Agregar")]
        public async Task<IActionResult> Add()
        {
            return View("SaveProfesor", new Profesor());
        }

        [HttpPost]
        [Route("[controller]/Agregar")]
        public async Task<IActionResult> Add(Profesor profesor)
        {
            await context.Profesores.AddAsync(profesor);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
            //return View("AddAlumno", new Alumno());
        }

        [HttpGet]
        [Route("[controller]/Actualizar/{Id:int}")]
        public async Task<IActionResult> Edit(int? Id)
        {
            Profesor getProfesor = new Profesor();
            if (Id != null)
            {
                getProfesor = await context.Profesores.FindAsync(Id);
            }
            else
            {
                return RedirectToAction("Profesores");
            }
            return View("UpdateProfesor", getProfesor);
        }

        [HttpPost]
        [Route("[controller]/Actualizar")]
        public async Task<IActionResult> UpdateProfesor(Profesor profesor)
        {
            var GetProfesor = await context.Profesores.FindAsync(profesor.Id);
            if (GetProfesor != null) {
                GetProfesor.Nombre = profesor.Nombre;
                GetProfesor.Genero = profesor.Genero;
                context.Entry(GetProfesor).State = EntityState.Modified;
                int rowsAffected = await context.SaveChangesAsync();
                if (rowsAffected != 0)
                    return RedirectToAction("Index");
            }
            return View("UpdateProfesor", profesor);
        }
    }
}