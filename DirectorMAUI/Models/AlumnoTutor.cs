using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorMAUI.Models
{
    public class AlumnoTutor
    {
        public int Id { get; set; }

        public int IdTutor { get; set; }

        public int IdAlumno { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; } = null!;

        public virtual Tutor IdTutorNavigation { get; set; } = null!;
    }
}
