using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorMAUI.Models
{
    public class Asignatura
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int TipoAsignatura { get; set; }

        public virtual ICollection<Calificacion> Calificacion { get; } = new List<Calificacion>();

        public virtual ICollection<DocenteAsignatura> DocenteAsignatura { get; } = new List<DocenteAsignatura>();
    }
}
