using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTenicaFeb2023.Models;
using PruebaTenicaFeb2023.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTenicaFeb2023.Controllers
{
    public class AsignacionesController : Controller
    {
        private readonly DataContext context;
        public AsignacionesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("[Controller]/Asignaciones")]
        public IActionResult Index()
        {
            List<AlumnoGrado> asignaciones = context.AlumnosGrados.Include(ag => ag.Grado).Include(ag => ag.Alumno).ToList();
            return View("IndexAlumnoGrados",asignaciones);
        }

        [HttpGet]
        [Route("[controller]/Agregar")]
        public async Task<IActionResult> Add()
        {
            try
            {

                AlumnoGrado alumnoGrado = new AlumnoGrado();
                ViewBag.Alumnos = new SelectList(await context.Alumnos.ToListAsync(), "Id", "Nombre");
                ViewBag.Grados = new SelectList(await context.Grados.ToListAsync(), "Id", "Nombre");
                return View("AddAlumnoGrados", alumnoGrado);
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [Route("[controller]/Agregar")]
        public async Task<IActionResult> Add(AlumnoGrado alumnoGrado)
        {
            ModelState.Clear();
            try
            {
                await context.AlumnosGrados.AddAsync(alumnoGrado);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        [Route("[controller]/Actualizar/{Id:int}")]
        public async Task<IActionResult> Edit(int? Id)
        {
            ViewBag.Alumnos = new SelectList(await context.Alumnos.ToListAsync(), "Id", "Nombre");
            ViewBag.Grados = new SelectList(await context.Grados.ToListAsync(), "Id", "Nombre");
            
            AlumnoGrado getAlumnoGrado = new AlumnoGrado();
            if (Id != null)
            {
                getAlumnoGrado = await context.AlumnosGrados.Include(ag => ag.Grado).Include(ag=>ag.Alumno).FirstOrDefaultAsync(ag => ag.Id == Id);
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View("UpdateAlumnoGrados", getAlumnoGrado);
        }

        [HttpPost]
        [Route("[controller]/Actualizar")]
        public async Task<IActionResult> SaveAsignacion(AlumnoGrado alumnoGrado)
        {
            ModelState.Clear();
            ViewBag.Alumnos = new SelectList(await context.Alumnos.ToListAsync(), "Id", "Nombre");
            ViewBag.Grados = new SelectList(await context.Grados.ToListAsync(), "Id", "Nombre");
            try
            {
                var GetAlumnoGrado = await context.AlumnosGrados.FindAsync(alumnoGrado.Id);
                if (GetAlumnoGrado != null)
                {
                    GetAlumnoGrado.AlumnoId = alumnoGrado.AlumnoId;
                    GetAlumnoGrado.GradoId = alumnoGrado.GradoId;
                    GetAlumnoGrado.Seccion = alumnoGrado.Seccion;
                    context.Entry(GetAlumnoGrado).State = EntityState.Modified;
                    int rowsAffected = await context.SaveChangesAsync();
                    if (rowsAffected != 0)
                        return RedirectToAction("Index");
                }
                return View("UpdateAlumnoGrados", alumnoGrado);
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index");
            }
        }

        [Route("[controller]/Eliminar/{Id:int}")]
        [HttpGet]
        public async Task<IActionResult> DeleteAsignacion(int? Id)
        {
            AlumnoGrado getAlumnoGrado = new AlumnoGrado();
            if (Id != null)
            {
                getAlumnoGrado = await context.AlumnosGrados.Include(ag => ag.Grado).Include(ag => ag.Alumno).FirstOrDefaultAsync(ag => ag.Id == Id);
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View("DeleteAlumnoGrados", getAlumnoGrado);
        }

        [HttpPost]
        [Route("[controller]/Eliminar/{Id:int}")]
        public async Task<IActionResult> DeleteAsignacion(AlumnoGrado alumnoGrado)
        {
            ModelState.Clear();
            try
            {
                AlumnoGrado getAlumnoGrado = new AlumnoGrado();
                if (alumnoGrado != null)
                {
                    getAlumnoGrado = await context.AlumnosGrados.Include(ag => ag.Grado).Include(ag => ag.Alumno).FirstOrDefaultAsync(ag => ag.Id == alumnoGrado.Id);
                    if (getAlumnoGrado != null)
                    {
                        context.AlumnosGrados.Remove(getAlumnoGrado);
                        await context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                return View("DeleteAlumnoGrados", alumnoGrado);

            }
            catch (System.Exception)
            {
                return RedirectToAction("Index");
            }
            //return RedirectToAction("Index");            
        }
    }
}
