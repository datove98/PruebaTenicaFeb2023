using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTenicaFeb2023.Models
{
    public class AlumnoGrado
    {
        [Key]
        public int Id { get; set; } = 0;
        public int AlumnoId { get; set; } = 0;
        public int GradoId { get; set; } = 0;
        public string Seccion { get; set; } = "";

        public virtual Alumno Alumno { get; set; }
        public virtual Grado Grado { get; set; }
    }
}
