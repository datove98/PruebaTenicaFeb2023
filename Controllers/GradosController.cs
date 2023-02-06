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
    public class GradosController : Controller
    {
        private readonly DataContext context;
        public GradosController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("[Controller]/Grados")]
        public async Task<IActionResult> Index()
        {
            List<Grado> grados = await context.Grados.Include(g => g.Profesor).ToListAsync();
            return View("IndexGrados",grados);
        }

        [HttpGet]
        [Route("[Controller]/Crear")]
        public async Task<IActionResult> Add()
        {
            ViewBag.Profesores = new SelectList(await context.Profesores.ToListAsync(), "Id", "Nombre");
            return View("AddGrado", new Grado());
        }

        [HttpPost]
        [Route("[Controller]/Crear")]
        public async Task<IActionResult> Add(Grado grado)
        {
            ModelState.Clear();
            ViewBag.Profesores = new SelectList(await context.Profesores.ToListAsync(), "Id", "Nombre");
            if (ModelState.IsValid)
            {
                context.Grados.Add(grado);
                int numRows = await context.SaveChangesAsync();
                if (numRows > 0)
                {
                    RedirectToAction("Index");
                }
            }
            return View("AddGrado", grado);
        }

        [HttpGet]
        [Route("[Controller]/Editar/{Id:int}")]
        public async Task<IActionResult> Edit(int? Id)
        {
            Grado getGrado = new Grado();
            ViewBag.Profesores = new SelectList(await context.Profesores.ToListAsync(), "Id", "Nombre");
            if (Id != null)
            {
                getGrado = await context.Grados.Include(g => g.Profesor).FirstOrDefaultAsync(g => g.Id == Id);
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View("UpdateGrado", getGrado);
        }

        [HttpPost]
        [Route("[Controller]/Editar")]
        public async Task<IActionResult> EditGrado(Grado grado)
        {
            ModelState.Clear();
            ViewBag.Profesores = new SelectList(await context.Profesores.ToListAsync(), "Id", "Nombre");
            if (ModelState.IsValid) {
                var getGrado = await context.Grados.Include(g => g.Profesor).FirstOrDefaultAsync(g => g.Id == grado.Id);
                if (getGrado != null) {
                    getGrado.Nombre = grado.Nombre;
                    getGrado.ProfesorId = grado.ProfesorId;
                    context.Entry<Grado>(getGrado).State = EntityState.Modified;
                    int rowsAffected = await context.SaveChangesAsync();
                    if (rowsAffected > 0) { return RedirectToAction("Index"); }
                }
            }
            return View("UpdateGrado", grado);
        }

    }
}
