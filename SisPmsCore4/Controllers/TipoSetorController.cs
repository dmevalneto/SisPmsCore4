using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SisPmsCore4.Models;

namespace SisPmsCore4.Controllers
{
    public class TipoSetorController : Controller
    {

        IHttpContextAccessor HttpContextAccessor;
        public TipoSetorController(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public IActionResult Index()
        {
            TipoSetor objTipoSetor = new TipoSetor(HttpContextAccessor);
            ViewBag.ListaTipoSetor = objTipoSetor.ListaTipoSetor();
            return View();
        }

    

        // GET: TipoSetor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoSetor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoSetor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoSetor formulario)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    formulario.SalvarNovoRegistro();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoSetor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TipoSetor/Edit/5
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

        // GET: TipoSetor/Delete/5
        public ActionResult Delete(int id)
        {
            TipoSetor objTipoSetor = new TipoSetor(HttpContextAccessor);
            objTipoSetor.Delete(id);
            return RedirectToAction("Index");
        }

        // POST: TipoSetor/Delete/5
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