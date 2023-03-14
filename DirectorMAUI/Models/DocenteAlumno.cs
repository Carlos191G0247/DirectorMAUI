using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorMAUI.Models
{
    public class DocenteAlumno
    {
        public int Id { get; set; }

        public int IdDocente { get; set; }

        public int IdAlumno { get; set; }

        public int IdPeriodo { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; } = null!;

        public virtual Docente IdDocenteNavigation { get; set; } = null!;

        public virtual Periodo IdPeriodoNavigation { get; set; } = null!;
    }
}
