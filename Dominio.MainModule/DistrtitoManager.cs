using Dominio.Core.Entities;
using Infraestructura.Data.SqlServer;
using System.Collections.Generic;

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
