using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SisPmsCore4.Models;

namespace SisPmsCore4.Controllers
{
    public class ItemController : Controller
    {

        IHttpContextAccessor HttpContextAccessor;
        public ItemController(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }


        // GET: Item
        public IActionResult Index()
        {
            ViewBag.ListaItem = new Item(HttpContextAccessor).ListaItem();
            return View();
        }

        // GET: Item/Details/5
        public IActionResult Details(int id)
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
        public ActionResult Create(Item formulario)
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

        // GET: Colaborador/Edit/5
        public ActionResult Edit(int? id)
        {
            Item objItem = new Item();
            return View(objItem.CarregarRegistro(id));
        }

        // POST: Colaborador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item formulario)
        {

            // TODO: Add update logic here

            formulario.AtualizarRegistro();
            return RedirectToAction("Index");

        }

        // GET: Item/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Item/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
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