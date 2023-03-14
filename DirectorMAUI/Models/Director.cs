using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorMAUI.Models
{
   public class Director
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public int Idusuario { get; set; }

        public virtual Usuario IdusuarioNavigation { get; set; } = null!;
    }
}
