using canvia.Entity.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canvia.Entity.DataAccess
{
    public class EmpresaDA
    {

        SqlCommand cmdSQL = new SqlCommand();
        String strCon = ConfigurationManager.AppSettings["conexion"].ToString();
        public EmpresaBE createupdateEmpresaDA(EmpresaBE objEmpresa)
        {
            DataSet dsEmpresa = new DataSet();
            EmpresaBE resEmpresa = new EmpresaBE();
            try
            {
                cmdSQL.Connection = new SqlConnection(strCon);
                cmdSQL.CommandText = "createUpdateEmpresa";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.AddWithValue("@cRuc", objEmpresa.cRuc);
                cmdSQL.Parameters.AddWithValue("@cNombre", objEmpresa.cNombre);
                cmdSQL.Parameters.AddWithValue("@cDireccion", objEmpresa.cDireccion);
                SqlDataAdapter da = new SqlDataAdapter(cmdSQL);
                da.Fill(dsEmpresa);
                if (dsEmpresa.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsEmpresa.Tables[0].Rows[0];
                    resEmpresa.cRuc = dr["cRuc"].ToString();
                    resEmpresa.cNombre = dr["cNombre"].ToString();
                    resEmpresa.cDireccion = dr["cDireccion"].ToString();
                    resEmpresa.dtFechaCreacion = Convert.ToDateTime(dr["dtFechaCreacion"]);
                    resEmpresa.bExiste = dr["cEstado"].ToString() == "1"? true : false;
                }
                else
                {
                    resEmpresa.cRuc = "";
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
            return resEmpresa;
        }

        public String deleteEmpresaDA(String cRuc, int nOperacion)
        {
            DataSet dsEmpresa = new DataSet();
            String res = "";
            try
            {
                cmdSQL.Connection = new SqlConnection(strCon);
                cmdSQL.CommandText = "deleteEmpresa";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.AddWithValue("@cRuc", cRuc);
                cmdSQL.Parameters.AddWithValue("@operacion", nOperacion); //1 eliminacion 2 eliminacion logica
                SqlDataAdapter da = new SqlDataAdapter(cmdSQL);
                da.Fill(dsEmpresa);
                if (dsEmpresa.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsEmpresa.Tables[0].Rows[0];
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

        public List<EmpresaBE> readEmpresaDA(String cRuc, int nRegistros, int nPagina)
        {
            DataSet dsEmpresa = new DataSet();
            List<EmpresaBE> resEmpresa = new List<EmpresaBE>();
            try
            {
                cmdSQL.Connection = new SqlConnection(strCon);
                cmdSQL.CommandText = "readEmpresa";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.AddWithValue("@cRuc", cRuc);
                cmdSQL.Parameters.AddWithValue("@nRegistros", nRegistros);
                cmdSQL.Parameters.AddWithValue("@nPagina", nPagina);
                SqlDataAdapter da = new SqlDataAdapter(cmdSQL);
                da.Fill(dsEmpresa);
                foreach (DataRow dr in dsEmpresa.Tables[0].Rows)
                {
                    EmpresaBE objEmpresa = new EmpresaBE();
                    objEmpresa.cRuc = dr["cRuc"].ToString();
                    objEmpresa.cNombre = dr["cNombre"].ToString();
                    objEmpresa.cDireccion = dr["cDireccion"].ToString();
                    objEmpresa.dtFechaCreacion = Convert.ToDateTime(dr["dtFechaCreacion"]);
                    objEmpresa.bExiste = dr["cEstado"].ToString() == "1" ? true : false;
                    resEmpresa.Add(objEmpresa);
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
            return resEmpresa;
        }
    }
}
