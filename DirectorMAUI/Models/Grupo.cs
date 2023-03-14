using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorMAUI.Models
{
    public class Grupo
    {
        public int Id { get; set; }

        public string Grado { get; set; } = null!;

        public string Seccion { get; set; } = null!;

        public virtual ICollection<Alumno> Alumno { get; } = new List<Alumno>();

        public virtual ICollection<DocenteGrupo> DocenteGrupo { get; } = new List<DocenteGrupo>();
    }
}
