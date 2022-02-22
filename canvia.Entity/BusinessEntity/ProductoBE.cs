using System;
using System.Collections.Generic;
using System.Text;

namespace canvia.Entity.BusinessEntity
{
    public class ProductoBE
    {
        public int nId { get; set; }
        public String cNombre { get; set; }
        public String cDescripcion { get; set; }
        public double nPrecio { get; set; }
        public String cRucEmpresa { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public bool bExiste { get; set; }

        public EmpresaBE empresa;
    }
}
