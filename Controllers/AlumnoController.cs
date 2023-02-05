using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTenicaFeb2023.Models;
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
        [Route("Agregar")]
        public async Task<IActionResult> Add()
        {
            return View("AddAlumno", new Alumno());
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Add(Alumno alumno)
        {
            await context.Alumnos.AddAsync(alumno);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
            //return View("AddAlumno", new Alumno());
        }

        [HttpGet]
        [Route("Actualizar/{Id:int}")]
        public async Task<IActionResult> Edit(int? Id)
        {
            Alumno getAlumno = new Alumno();
            if (Id != null)
            {
                getAlumno = await context.Alumnos.FindAsync(Id);
            }
            else
            {
                return RedirectToAction("Alumnos");
            }
            return View("AddAlumno", getAlumno);
        }
    }
}
