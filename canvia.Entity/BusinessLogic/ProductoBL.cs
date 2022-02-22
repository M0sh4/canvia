using canvia.Entity.BusinessEntity;
using canvia.Entity.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace canvia.Entity.BusinessLogic
{
    public class ProductoBL
    {

        ProductoDA DAProducto = new ProductoDA();

        public ProductoBE createupdateProductoBL(ProductoBE objProducto)
        {
            return DAProducto.createupdateProductoDA(objProducto);
        }
        public String deleteProductoBL(int nId, int nOperacion)
        {
            return DAProducto.deleteProductoDA( nId, nOperacion);
        }
        public List<ProductoBE> readProductoBL(int nId, int nRegistros, int nPagina)
        {
            return DAProducto.readProductoDA(nId, nRegistros, nPagina);
        }
        public List<ProductoBE> readProductoEmpresaBL()
        {
            return DAProducto.readProductoEmpresaDA();
        }
    }
}
