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
    public class ProductoController : ApiController
    {

        ProductoBL blProducto = new ProductoBL();

        [HttpPost]
        public HttpResponseMessage createUpdateProducto(ProductoBE objProducto)
        {
            ProductoBE resProducto = blProducto.createupdateProductoBL(objProducto);
            if (resProducto.nId == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "la empresa no existe");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, resProducto);
            }
        }

        [HttpPost]
        public String deleteProducto(int nId, int operacion)
        {
            return blProducto.deleteProductoBL(nId, operacion);
        }

        [HttpGet]
        public List<ProductoBE> readProducto(int nId = 0, int nPagina = 0, int nRegistros = 0)
        {
            return blProducto.readProductoBL(nId, nPagina, nRegistros);
        }

        [HttpGet]
        public List<ProductoBE> readEmpresaProducto()
        {
            return blProducto.readProductoEmpresaBL();
        }
    }
}