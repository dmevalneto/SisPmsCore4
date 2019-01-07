using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SisPmsCore4.Models;

namespace SisPmsCore4.Controllers
{
    public class AnotacoesController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;
        public AnotacoesController(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }


        // GET: Anotacoes
        public ActionResult Index()
        {
            Anotacoes objAnotacoes = new Anotacoes(HttpContextAccessor);
            ViewBag.ListaAnotacoes = objAnotacoes.ListaAnotacoes();
            return View();
        }

        // GET: Anotacoes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cargo/Create
        public ActionResult Create()
        {
            ViewBag.ListaSetor = new Setor(HttpContextAccessor).ListaSetor();
            return View();
        }

        // POST: Cargo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Anotacoes formulario)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    formulario.HttpContextAccessor = HttpContextAccessor;
                    formulario.SalvarNovoRegistro();
                    return RedirectToAction("Index");
                }
                ViewBag.ListaSetor = new Setor(HttpContextAccessor).ListaSetor();
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View();
            }
        }

        // GET: Anotacoes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Anotacoes/Edit/5
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

        // GET: Anotacoes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Anotacoes/Delete/5
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