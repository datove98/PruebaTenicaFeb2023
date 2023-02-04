using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTenicaFeb2023.Models
{
    public class Grado
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public int ProfesorId { get; set; } = 0;
        public virtual Profesor Profesor { get; set; }
        public virtual ICollection<AlumnoGrado> Alumnos {get;set;}
    }
}
