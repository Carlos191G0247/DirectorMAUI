using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorMAUI.Models
{
    public class DocenteAsignatura
    {
        public int Id { get; set; }

        public int IdDocente { get; set; }

        public int IdAsignatura { get; set; }

        public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;

        public virtual Docente IdDocenteNavigation { get; set; } = null!;
    }
}
