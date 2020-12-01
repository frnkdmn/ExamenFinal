using Dominio.Core.Entities;
using Dominio.MainModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaPresentacionASPMVC.Models;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace CapaPresentacionASPMVC.Controllers
{
    public class ClienteController : Controller
    {
        DistrtitoManager managerDistrito = new DistrtitoManager();
        ClienteManager managerCliente = new ClienteManager();
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }
        //listado clientes
        public ActionResult listadoClientes(int p = 0)
        {
            List<Cliente> aCleinte = managerCliente.ListClientes();
            int filas = 10;
            int total = aCleinte.Count();

            int pag = 0;
            if (total % filas > 0)
            {
                pag = total / filas + 1;
            }
            else
            {
                pag = total / filas;
            }
            ViewBag.p = p;
            ViewBag.pag = pag;
            return View(aCleinte.Skip(p * filas).Take(filas));
        }
        //listado clientesxDistrito
        public ActionResult listadoClientesxDistritito(int dis = 0, int p = 0)
        {
            ViewBag.dis = dis;
            ViewBag.distrito = new SelectList(managerDistrito.listDistrito(), "codigo", "nombre");
            List<Cliente> aCleinte = managerCliente.ListClientesxDistrtito(dis);
            int filas = 10;
            int total = aCleinte.Count();

            int pag = 0;
            if (total % filas > 0)
            {
                pag = total / filas + 1;
            }
            else
            {
                pag = total / filas;
            }
            ViewBag.p = p;
            ViewBag.pag = pag;
            return View(aCleinte.Skip(p * filas).Take(filas));
        }
        //Nuevo cliente
        public ActionResult nuevoCliente()
        {
            ViewBag.distrito = new SelectList(managerDistrito.listDistrito(), "codigo", "nombre");
            return View(new ClienteO());
        }
        [HttpPost]
        public ActionResult nuevoCliente(ClienteO objO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.distrito = new SelectList(managerDistrito.listDistrito(), "codigo", "nombre");
                return View(objO);
            }
            ViewBag.mensaje = managerCliente.nuevoCliente(objO);
            ViewBag.distrito = new SelectList(managerDistrito.listDistrito(), "codigo", "nombre");
            return View(objO);
        }
        //Modificar cliente
        public ActionResult modificaCliente(int id)
        {
            ClienteO objO = managerCliente.ListClientesO().Where(
                            v => v.codigo == id).FirstOrDefault();
            ViewBag.distrito = new SelectList(managerDistrito.listDistrito(), "codigo", "nombre", objO.distrito);
            return View(objO);
        }
        [HttpPost]
        public ActionResult modificaCliente(ClienteO objO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.distrito = new SelectList(managerDistrito.listDistrito(), "codigo", "nombre", objO.distrito);
                return View(objO);
            }
            ViewBag.mensaje = managerCliente.modificaCliente(objO);
            ViewBag.distrito = new SelectList(managerDistrito.listDistrito(), "codigo", "nombre", objO.distrito);
            return View(objO);
        }
        //Detalle Cliente
        public ActionResult detalleCliente(int id)
        {
            Cliente objC = managerCliente.ListClientes().Where(
                v => v.codigo == id).FirstOrDefault();
            return View(objC);
        }
        //Elimina Cliente
        public ActionResult eliminaCliente(int id)
        {
            ClienteO objO = managerCliente.ListClientesO().Where(
                            v => v.codigo == id).FirstOrDefault();
            ViewBag.mensaje = managerCliente.eliminaCliente(id);
            return RedirectToAction("listadoClientes");
        }

        Infraestructura.Data.SqlServer.Conexion objCon = new Infraestructura.Data.SqlServer.Conexion();
        SqlConnection cn = null;

        List<SP_LISTADOCLIENTES_Result> listClientesPdf()
        {
            cn = objCon.getConecta();
            cn.Open();

            List<SP_LISTADOCLIENTES_Result> aClientes = new List<SP_LISTADOCLIENTES_Result>();
            SqlCommand cmd = new SqlCommand("SP_LISTADOCLIENTES", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                SP_LISTADOCLIENTES_Result objC = new SP_LISTADOCLIENTES_Result()
                {
                    IDE_CLI = dr.GetInt32(0),
                    CLIENTE = dr.GetString(1),
                    DNI_CLI = dr.GetString(2),
                    TEL_CLI = dr.GetString(3),
                    DES_DIS = dr.GetString(4),
                };
                aClientes.Add(objC);
            }
            dr.Close();
            cn.Close();
            return aClientes;
        }
        
        public ActionResult reporteClientes()
        {
            ReportDocument rdClientes = new ReportDocument();
            rdClientes.Load(Path.Combine(Server.MapPath("~/Reportes"),"crClientes.rpt"));
            rdClientes.SetDataSource(listClientesPdf().ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream st = rdClientes.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                st.Seek(0, SeekOrigin.Begin);
                return File(st, "application/pdf","ReporteClientes.pdf");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}