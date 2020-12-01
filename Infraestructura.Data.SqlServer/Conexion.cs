using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Infraestructura.Data.SqlServer
{
    public class Conexion
    {
        public SqlConnection getConecta()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.
                                ConnectionStrings["cn1"].ConnectionString);
            return cn;
        }
    }
}
