using System;
using System.Collections.Generic;

namespace MVCCRUD.Models
{
    public partial class Materia
    {
        public Materia()
        {
            Alumnos = new HashSet<Alumno>();
        }

        public int IdMateria { get; set; }
        public string? NombreMateria { get; set; }

        public virtual ICollection<Alumno> Alumnos { get; set; }
    }
}
