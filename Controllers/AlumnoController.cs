using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTenicaFeb2023.Models;
using PruebaTenicaFeb2023.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTenicaFeb2023.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly DataContext context;
        public AlumnoController(DataContext dataContext)
        {
            this.context = dataContext;
        }

        [HttpGet]
        [Route("Alumnos")]
        public async Task<IActionResult> Index()
        {
            List<Alumno> alumnos = await context.Alumnos.ToListAsync();

            return View("IndexAlumno",alumnos);
        }

        [HttpGet]
        [Route("[controller]/Agregar")]
        public async Task<IActionResult> Add()
        {
            Alumno alumno = new Alumno() { FechaNac = DateTime.Now };
            ViewBag.Generos = Recursos.GetGeneros();
            return View("AddAlumno", alumno);
        }

        [HttpPost]
        [Route("[controller]/Agregar")]
        public async Task<IActionResult> Add(Alumno alumno)
        {
            ModelState.Clear();
            try
            {
                await context.Alumnos.AddAsync(alumno);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                
            }
            return RedirectToAction("Index");
            //return View("AddAlumno", new Alumno());
        }

        [HttpGet]
        [Route("[controller]/Actualizar/{Id:int}")]
        public async Task<IActionResult> Edit(int? Id)
        {
            Alumno getAlumno = new Alumno();
            ViewBag.Generos = Recursos.GetGeneros();
            if (Id != null)
            {
                getAlumno = await context.Alumnos.FindAsync(Id);
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View("UpdateAlumno", getAlumno);
        }

        [HttpPost]
        [Route("[controller]/Actualizar")]
        public async Task<IActionResult> SaveAlumno(Alumno alumno)
        {
            ModelState.Clear();
            ViewBag.Generos = Recursos.GetGeneros();
            try
            {
                var GetAlumno = await context.Alumnos.FindAsync(alumno.Id);
                if (GetAlumno != null)
                {
                    GetAlumno.FechaNac = alumno.FechaNac;
                    GetAlumno.Nombre = alumno.Nombre;
                    GetAlumno.Genero = alumno.Genero;
                    context.Entry(GetAlumno).State = EntityState.Modified;
                    int rowsAffected = await context.SaveChangesAsync();
                    if (rowsAffected != 0)
                        return RedirectToAction("Index");
                }
                return View("UpdateAlumno", alumno);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");
        }

        [Route("[controller]/Eliminar/{Id:int}")]
        [HttpGet]
        public async Task<IActionResult> DeleteAlumno(int? Id)
        {
            Alumno getAlumno = new Alumno();
            if (Id != null)
            {
                getAlumno = await context.Alumnos.FirstOrDefaultAsync(ag => ag.Id == Id);
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View("DeleteAlumno", getAlumno);
        }

        [HttpPost]
        [Route("[controller]/Eliminar/{Id:int}")]
        public async Task<IActionResult> DeleteAlumno(Alumno alumno)
        {
            ModelState.Clear();
            Alumno getAlumno = new Alumno();
            if (alumno != null)
            {
                getAlumno = await context.Alumnos.FirstOrDefaultAsync(ag => ag.Id == alumno.Id);
                if (getAlumno != null)
                {
                    context.Alumnos.Remove(getAlumno);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View("DeleteAlumno", alumno);
            //return RedirectToAction("Index");            
        }
    }
}
