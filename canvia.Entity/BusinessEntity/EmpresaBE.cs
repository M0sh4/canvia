using System;
using System.Collections.Generic;
using System.Text;

namespace canvia.Entity.BusinessEntity
{
    public class EmpresaBE
    {
        public String cRuc { get; set; }
        public String cNombre { get; set; }
        public String cDireccion { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public bool bExiste { get; set; }
    }
}
