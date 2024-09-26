using System;
using System.Collections.Generic;

namespace MVCCRUD.Models
{
    public partial class Profesore
    {
        public Profesore()
        {
            Alumnos = new HashSet<Alumno>();
        }

        public int IdProfesor { get; set; }
        public string? NombreProfesor { get; set; }

        public virtual ICollection<Alumno> Alumnos { get; set; }
    }
}
