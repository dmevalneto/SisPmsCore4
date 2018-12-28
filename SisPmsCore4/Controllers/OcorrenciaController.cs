using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SisPmsCore4.Models;

namespace SisPmsCore4.Controllers
{
    public class OcorrenciaController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;
        public OcorrenciaController(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        // GET: Ocorrencia
        public ActionResult Index()
        {
            ViewBag.ListaOcorrencia = new Ocorrencia(HttpContextAccessor).ListaOcorrencia();
            return View();
        }

        // GET: Ocorrencia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ocorrencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ocorrencia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ocorrencia formulario)
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

        // GET: Ocorrencia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ocorrencia/Edit/5
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

        // GET: Ocorrencia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ocorrencia/Delete/5
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