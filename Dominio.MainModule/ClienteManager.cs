using Dominio.Core.Entities;
using Infraestructura.Data.SqlServer;
using System.Collections.Generic;

namespace Dominio.MainModule
{
    public class ClienteManager
    {
        ClientesDAL objDAL = new ClientesDAL();
        public List<Cliente> ListClientes()
        {
            return objDAL.listClientes();
        }
        public List<ClienteO> ListClientesO()
        {
            return objDAL.listClientesO();
        }
        public List<Cliente> ListClientesxDistrtito(int dis)
        {
            return objDAL.listClientesxDistrtito(dis);
        }
        public string nuevoCliente(ClienteO objO)
        {
            return objDAL.nuevoCliente(objO);
        }
        public string modificaCliente(ClienteO objO)
        {
            return objDAL.modificarCliente(objO);
        }
        public string eliminaCliente(int id)
        {
            return objDAL.eliminaCliente(id);
        }
    }
}
