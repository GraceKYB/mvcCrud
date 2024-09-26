using System;
using System.Collections.Generic;

namespace MVCCRUD.Models
{
    public partial class Alumno
    {
        public int IdAlumno { get; set; }
        public string? NombreAlumno { get; set; }
        public int? IdMateria { get; set; }
        public int? IdProfesor { get; set; }

        public virtual Materia? IdMateriaNavigation { get; set; }
        public virtual Profesore? IdProfesorNavigation { get; set; }
    }
}
