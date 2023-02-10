using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTenicaFeb2023.Models;
using PruebaTenicaFeb2023.Utilities;
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
            ViewBag.Generos = Recursos.GetGeneros();
            return View("SaveProfesor", new Profesor());
        }

        [HttpPost]
        [Route("[controller]/Agregar")]
        public async Task<IActionResult> Add(Profesor profesor)
        {
            ViewBag.Generos = Recursos.GetGeneros();
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
            ViewBag.Generos = Recursos.GetGeneros();
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
            try
            {
                var GetProfesor = await context.Profesores.FindAsync(profesor.Id);
                ViewBag.Generos = Recursos.GetGeneros();
                if (GetProfesor != null)
                {
                    GetProfesor.Nombre = profesor.Nombre;
                    GetProfesor.Genero = profesor.Genero;
                    context.Entry(GetProfesor).State = EntityState.Modified;
                    int rowsAffected = await context.SaveChangesAsync();
                    if (rowsAffected != 0)
                        return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return View("UpdateProfesor", profesor);
        }

        [Route("[controller]/Eliminar/{Id:int}")]
        [HttpGet]
        public async Task<IActionResult> DeleteProfesor(int? Id)
        {
            Profesor getProfesor = new Profesor();
            if (Id != null)
            {
                if (getProfesor != null)
                {
                    getProfesor = await context.Profesores.FirstOrDefaultAsync(ag => ag.Id == Id);
                    return View("DeleteProfesor", getProfesor);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("[controller]/Eliminar/{Id:int}")]
        public async Task<IActionResult> DeleteProfesor(Profesor profesor)
        {
            Profesor getProfesor = new Profesor();
            try
            {
                if (profesor != null)
                {
                    getProfesor = await context.Profesores.FirstOrDefaultAsync(ag => ag.Id == profesor.Id);
                    if (getProfesor != null)
                    {
                        context.Profesores.Remove(getProfesor);
                        await context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return View("DeleteProfesor", profesor);
            //return RedirectToAction("Index");            
        }
    }
}
