﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTenicaFeb2023.Models
{
    public class Alumno
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string Genero { get; set; } = "";
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime FechaNac { get; set; } = new DateTime();
        public virtual ICollection<AlumnoGrado> AlumnoGrados { get; set; }
    }
}
