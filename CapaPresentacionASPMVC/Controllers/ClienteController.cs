using Dominio.Core.Entities;
using Dominio.MainModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        //listado clientes
        public ActionResult listadoClientesxDistritito(int dis=0,int p = 0)
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
    }
}