using canvia.Entity.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace canvia.Entity.DataAccess
{
    public class ProductoDA
    {
        SqlCommand cmdSQL = new SqlCommand();
        String strCon = ConfigurationManager.AppSettings["conexion"].ToString();

        public ProductoBE createupdateProductoDA(ProductoBE objProducto)
        {
            DataSet dsProducto = new DataSet();
            ProductoBE resProducto = new ProductoBE();
            try
            {
                cmdSQL.Connection = new SqlConnection(strCon);
                cmdSQL.CommandText = "createUpdateProducto";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.AddWithValue("@nId", objProducto.nId);
                cmdSQL.Parameters.AddWithValue("@cNombre", objProducto.cNombre);
                cmdSQL.Parameters.AddWithValue("@cDescripcion", objProducto.cDescripcion);
                cmdSQL.Parameters.AddWithValue("@nPrecio", objProducto.nPrecio);
                cmdSQL.Parameters.AddWithValue("@cRucEmpresa", objProducto.cRucEmpresa);
                SqlDataAdapter da = new SqlDataAdapter(cmdSQL);
                da.Fill(dsProducto);
                if (dsProducto.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsProducto.Tables[0].Rows[0];
                    resProducto.nId = Convert.ToInt32(dr["nId"]);
                    resProducto.cNombre = dr["cNombre"].ToString();
                    resProducto.cDescripcion = dr["cDescripcion"].ToString();
                    resProducto.nPrecio = Convert.ToDouble(dr["nPrecio"]);
                    resProducto.cRucEmpresa = dr["cRucEmpresa"].ToString();
                    resProducto.bExiste = dr["cEstado"].ToString() == "1"? true: false;
                    resProducto.dtFechaCreacion = Convert.ToDateTime(dr["dtFechaCreacion"]);
                }
                else
                {
                    resProducto.nId = 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                if (cmdSQL.Connection.State == ConnectionState.Open)
                {
                    cmdSQL.Connection.Close();
                }
            }
            return resProducto;
        }

        public String deleteProductoDA(int nId, int nOperacion)
        {
            DataSet dsProducto = new DataSet();
            String res = "";
            try
            {
                cmdSQL.Connection = new SqlConnection(strCon);
                cmdSQL.CommandText = "deleteProducto";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.AddWithValue("@nId", nId);
                cmdSQL.Parameters.AddWithValue("@operacion", nOperacion); //1 eliminacion 2 eliminacion logica
                SqlDataAdapter da = new SqlDataAdapter(cmdSQL);
                da.Fill(dsProducto);
                if (dsProducto.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsProducto.Tables[0].Rows[0];
                    res = dr["response"].ToString();
                }
                else
                {
                    res = "error";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                if (cmdSQL.Connection.State == ConnectionState.Open)
                {
                    cmdSQL.Connection.Close();
                }
            }
            return res;
        }

        public List<ProductoBE> readProductoDA(int nId, int nRegistros, int nPagina)
        {
            DataSet dsProducto = new DataSet();
            List<ProductoBE> resProducto = new List<ProductoBE>();
            try
            {
                cmdSQL.Connection = new SqlConnection(strCon);
                cmdSQL.CommandText = "readProducto";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.AddWithValue("@nId", nId);
                cmdSQL.Parameters.AddWithValue("@nRegistros", nRegistros);
                cmdSQL.Parameters.AddWithValue("@nPagina", nPagina);
                SqlDataAdapter da = new SqlDataAdapter(cmdSQL);
                da.Fill(dsProducto);
                foreach(DataRow dr in dsProducto.Tables[0].Rows)
                {
                    ProductoBE objProducto = new ProductoBE();
                    objProducto.nId = Convert.ToInt32(dr["nId"]);
                    objProducto.cNombre = dr["cNombre"].ToString();
                    objProducto.cDescripcion = dr["cDescripcion"].ToString();
                    objProducto.nPrecio = Convert.ToDouble(dr["nPrecio"]);
                    objProducto.cRucEmpresa = dr["cRucEmpresa"].ToString();
                    objProducto.bExiste = dr["cEstado"].ToString() == "1" ? true : false;
                    objProducto.dtFechaCreacion = Convert.ToDateTime(dr["dtFechaCreacion"]);
                    resProducto.Add(objProducto);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                if (cmdSQL.Connection.State == ConnectionState.Open)
                {
                    cmdSQL.Connection.Close();
                }
            }
            return resProducto;
        }
        public List<ProductoBE> readProductoEmpresaDA()
        {
            DataSet dsProducto = new DataSet();
            List<ProductoBE> resProducto = new List<ProductoBE>();
            try
            {
                cmdSQL.Connection = new SqlConnection(strCon);
                cmdSQL.CommandText = "readProductoEmpresa";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmdSQL);
                da.Fill(dsProducto);
                foreach (DataRow dr in dsProducto.Tables[0].Rows)
                {
                    ProductoBE objProducto = new ProductoBE();
                    objProducto.nId = Convert.ToInt32(dr["nId"]);
                    objProducto.cNombre = dr["cNombre"].ToString();
                    objProducto.cDescripcion = dr["cDescripcion"].ToString();
                    objProducto.nPrecio = Convert.ToDouble(dr["nPrecio"]);
                    objProducto.bExiste = dr["cEstado"].ToString() == "1" ? true : false;
                    objProducto.dtFechaCreacion = Convert.ToDateTime(dr["dtFechaCreacion"]);
                    EmpresaBE objEmpresa = new EmpresaBE();
                    objEmpresa.cRuc = dr["cRucEmpresa"].ToString();
                    objEmpresa.cNombre = dr["cNombre"].ToString();
                    objEmpresa.cDireccion = dr["cDireccion"].ToString();
                    objEmpresa.dtFechaCreacion = Convert.ToDateTime(dr["dtFechaCreacionEmpresa"]);
                    objEmpresa.bExiste = dr["cEstadoEmpresa"].ToString() == "1" ? true : false;
                    objProducto.empresa = objEmpresa;
                    resProducto.Add(objProducto);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                if (cmdSQL.Connection.State == ConnectionState.Open)
                {
                    cmdSQL.Connection.Close();
                }
            }
            return resProducto;
        }
    }
}
