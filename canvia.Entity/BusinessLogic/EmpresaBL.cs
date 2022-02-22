using canvia.Entity.BusinessEntity;
using canvia.Entity.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canvia.Entity.BusinessLogic
{
    public class EmpresaBL
    {

        EmpresaDA DAEmpresa = new EmpresaDA();

        public EmpresaBE createupdateEmpresaBL(EmpresaBE objProducto)
        {
            return DAEmpresa.createupdateEmpresaDA(objProducto);
        }
        public String deleteEmpresaBL(String cRuc, int nOperacion)
        {
            return DAEmpresa.deleteEmpresaDA(cRuc, nOperacion);
        }
        public List<EmpresaBE> readEmpresaBL(String cRuc, int nRegistros, int nPagina)
        {
            return DAEmpresa.readEmpresaDA(cRuc, nRegistros, nPagina);
        }
    }
}
