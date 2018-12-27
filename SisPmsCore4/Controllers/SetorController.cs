using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SisPmsCore4.Models;

namespace SisPmsCore4.Controllers
{
    public class SetorController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;
        public SetorController(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        // GET: Setor
        public ActionResult Index()
        {
            ViewBag.ListaTipoSetor = new TipoSetor(HttpContextAccessor).ListaTipoSetor();
            ViewBag.ListaSetor = new Setor(HttpContextAccessor).ListaSetor();
            return View();
        }

        // GET: Setor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Setor/Create
        public ActionResult Create()
        {
            ViewBag.ListaTipoSetor = new TipoSetor(HttpContextAccessor).ListaTipoSetor();
            return View();
        }

        // POST: Setor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Setor formulario)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    formulario.SalvarNovoRegistro();
                    return RedirectToAction("Index");
                }
                ViewBag.ListaTipoSetor = new TipoSetor(HttpContextAccessor).ListaTipoSetor();
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Setor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Setor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Setor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Setor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}