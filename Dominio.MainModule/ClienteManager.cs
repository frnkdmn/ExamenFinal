﻿using Dominio.Core.Entities;
using Infraestructura.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.MainModule
{
    public class ClienteManager
    {
        ClientesDAL objDAL = new ClientesDAL();
        public List<Cliente> ListClientes()
        {
            return objDAL.listClientes();
        }
        public List<Cliente> ListClientesxDistrtito(int dis)
        {
            return objDAL.listClientesxDistrtito(dis);
        }
        public string nuevoCliente(ClienteO objO)
        {
            return objDAL.nuevoCliente(objO);
        }
    }
}