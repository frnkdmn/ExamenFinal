using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dominio.Core.Entities;

namespace Infraestructura.Data.SqlServer
{
    public class DistritoDAL
    {
        SqlConnection cn = null;
        Conexion objCon = new Conexion();
        public List<Distrito> listDistrtito()
        {
            cn = objCon.getConecta();
            cn.Open();
            List<Distrito> aDistritos = new List<Distrito>();
            SqlCommand cmd = new SqlCommand("SP_LISTADISTRITOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Distrito objD = new Distrito()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                };
                aDistritos.Add(objD);
            }
            dr.Close();
            cn.Close();
            return aDistritos;
        }
    }
}
