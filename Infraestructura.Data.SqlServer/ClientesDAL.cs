using Dominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infraestructura.Data.SqlServer
{
    public class ClientesDAL
    {
        SqlConnection cn = null;
        Conexion objCon = new Conexion();

        //1. Listado de clientes
        public List<Cliente> listClientes()
        {
            cn = objCon.getConecta();
            cn.Open();
            List<Cliente> aClientes = new List<Cliente>();
            SqlCommand cmd = new SqlCommand("SP_LISTADOCLIENTES", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cliente objC = new Cliente()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    dni = dr[2].ToString(),
                    telefono = dr[3].ToString(),
                    distrito = dr[4].ToString()
                };
                aClientes.Add(objC);
            }
            dr.Close();
            cn.Close();
            return aClientes;
        }
        //1. Listado de clientes x Distrito
        public List<Cliente> listClientesxDistrtito(int dis)
        {
            cn = objCon.getConecta();
            cn.Open();
            List<Cliente> aClientes = new List<Cliente>();
            SqlCommand cmd = new SqlCommand("SP_LISTACLIENTESxDISTRITO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DIS", dis);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cliente objC = new Cliente()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    dni = dr[2].ToString(),
                    telefono = dr[3].ToString(),
                    distrito = dr[4].ToString()
                };
                aClientes.Add(objC);
            }
            dr.Close();
            cn.Close();
            return aClientes;
        }
        // Listado de Clientes con datos originales
        public List<ClienteO> listClientesO()
        {
            cn = objCon.getConecta();
            cn.Open();
            List<ClienteO> aClientesO = new List<ClienteO>();
            SqlCommand cmd = new SqlCommand("SP_LISTACLIENTES", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ClienteO objO = new ClienteO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    apellido = dr[1].ToString(),
                    nombre = dr[2].ToString(),
                    dni = dr[3].ToString(),
                    telefono = dr[4].ToString(),
                    distrito = int.Parse(dr[5].ToString())
                };
                aClientesO.Add(objO);
            }
            dr.Close();
            cn.Close();
            return aClientesO;
        }
        //Nuevo Cliente
        public string nuevoCliente(ClienteO objC)
        {
            cn = objCon.getConecta();
            cn.Open();
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SP_NUENVOCLIENTE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@APE", objC.apellido);
                cmd.Parameters.AddWithValue("@NOM", objC.nombre);
                cmd.Parameters.AddWithValue("@DNI", objC.dni);
                cmd.Parameters.AddWithValue("@TEL", objC.telefono);
                cmd.Parameters.AddWithValue("@DIST", objC.distrito);
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString()+" Cliente registrado!!!";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                throw;
            }
            cn.Close();
            return mensaje;
        }
        //Modifica Cliente
        public string modificarCliente(ClienteO objC)
        {
            cn = objCon.getConecta();
            cn.Open();
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZACLIENTE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDE", objC.codigo);
                cmd.Parameters.AddWithValue("@APE", objC.apellido);
                cmd.Parameters.AddWithValue("@NOM", objC.nombre);
                cmd.Parameters.AddWithValue("@DNI", objC.dni);
                cmd.Parameters.AddWithValue("@TEL", objC.telefono);
                cmd.Parameters.AddWithValue("@DIST", objC.distrito);
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " Cliente Actualizado!!!";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                throw;
            }
            cn.Close();
            return mensaje;
        }
        public string eliminaCliente(int id)
        {
            cn = objCon.getConecta();
            cn.Open();
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ELIMINACLIENTE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDE", id);
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " Cliente Actualizado!!!";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                throw;
            }
            cn.Close();
            return mensaje;
        }
    }
}
