using Dominio.Core.Entities;
using Infraestructura.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.MainModule
{
    public class DistrtitoManager
    {
        DistritoDAL objDAL = new DistritoDAL();
        public List<Distrito> listDistrito()
        {
            return objDAL.listDistrtito();
        }

    }
}
