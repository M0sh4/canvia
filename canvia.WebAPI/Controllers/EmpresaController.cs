using canvia.Entity.BusinessEntity;
using canvia.Entity.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace canvia.WebAPI.Controllers
{
    public class EmpresaController : ApiController
    {
        EmpresaBL blEmpresa = new EmpresaBL();

        [HttpPost]
        public EmpresaBE createUpdateEmpresa(EmpresaBE objProducto)
        {
            return blEmpresa.createupdateEmpresaBL(objProducto);
        }

        [HttpPost]
        public String deleteEmpresa(String cRuc, int operacion)
        {
            return blEmpresa.deleteEmpresaBL(cRuc, operacion);
        }

        [HttpGet]
        public List<EmpresaBE> readEmpresa(String cRuc = "", int nPagina = 0, int nRegistros = 0)
        {
            return blEmpresa.readEmpresaBL(cRuc, nPagina, nRegistros);
        }
    }
}