using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SisPmsCore4.Models;

namespace SisPmsCore4.Controllers
{
    public class PrestadoraServicoController : Controller
    {

        IHttpContextAccessor HttpContextAccessor;
        public PrestadoraServicoController(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        // GET: PrestadoraServico
        public ActionResult Index()
        {
            ViewBag.ListaPrestadoraServico = new PrestadoraServico(HttpContextAccessor).ListaPrestadoraServico();
            return View();
        }

        // GET: PrestadoraServico/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrestadoraServico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrestadoraServico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrestadoraServico formulario)
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

        // GET: PrestadoraServico/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrestadoraServico/Edit/5
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

        // GET: PrestadoraServico/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrestadoraServico/Delete/5
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