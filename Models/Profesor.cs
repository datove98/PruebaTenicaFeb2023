using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTenicaFeb2023.Models
{
    public class Profesor
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string Genero { get; set; } = "";
        public virtual ICollection<Grado> Grados { get; set; }
    }
}
