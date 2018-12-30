using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SisPmsCore4.Models;
using SisPmsCore4.Util;
using System;
using System.Data;

namespace SisPmsCore4.Controllers
{
    public class ColaboradorController : Controller
    {

        IHttpContextAccessor HttpContextAccessor;

        public ColaboradorController(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        // GET: Colaborador
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Encaminhar(int? id)
        {
            TempData["idColaborador"] = id;
            TempData.Keep("idColaborador");

            ViewBag.ListaSetor = new Setor(HttpContextAccessor).ListaSetor();
            return View();
        }

        [HttpGet]
        public IActionResult FinalizarEncaminhamento(int? id)
        {
            try
            {
                ViewBag.idColaborador = TempData["idColaborador"].ToString();
                ViewBag.idSetor = id;
                Historico objHistorico = new Historico(HttpContextAccessor);
                objHistorico.colaborador_idcolaborador = int.Parse(ViewBag.idColaborador);
                objHistorico.setor_idsetor = ViewBag.idSetor;
                objHistorico.data = DateTime.Now.ToString();
                objHistorico.Encaminhar();
                return RedirectToAction("HistoricoColaborador");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }

        }

        public IActionResult HistoricoColaborador()
        {
            try
            {
                Historico objHistorico = new Historico(HttpContextAccessor);
                ViewBag.HistoricoColaborador = objHistorico.HistoricoColaborador();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

      


        public IActionResult FiltrarColaborador(Colaborador form)
        {
            ViewBag.FiltrarColaborador = new Colaborador(HttpContextAccessor).FiltrarColaborador(form.Nome);
            return View();
        }



        // GET: Colaborador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Colaborador/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ListaSetor = new Setor(HttpContextAccessor).ListaSetor();
            ViewBag.ListaCargo = new Cargo(HttpContextAccessor).ListaCargo();
            ViewBag.ListaOcorrencia = new Ocorrencia(HttpContextAccessor).ListaOcorrencia();
            ViewBag.ListaPrestadoraServico = new PrestadoraServico(HttpContextAccessor).ListaPrestadoraServico();
            return View();
        }

        // POST: Colaborador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Colaborador formulario)
        {
            try
            {
                // TODO: Add insert logic here


                if (ModelState.IsValid)
                {
                    formulario.SalvarNovoRegistro();
                    return RedirectToAction("Index");
                }
                ViewBag.ListaSetor = new Setor(HttpContextAccessor).ListaSetor();
                ViewBag.ListaCargo = new Cargo(HttpContextAccessor).ListaCargo();
                ViewBag.ListaOcorrencia = new Ocorrencia(HttpContextAccessor).ListaOcorrencia();
                ViewBag.ListaPrestadoraServico = new PrestadoraServico(HttpContextAccessor).ListaPrestadoraServico();
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
            ViewBag.ListaSetor = new Setor(HttpContextAccessor).ListaSetor();
            ViewBag.ListaCargo = new Cargo(HttpContextAccessor).ListaCargo();
            ViewBag.ListaOcorrencia = new Ocorrencia(HttpContextAccessor).ListaOcorrencia();
            ViewBag.ListaPrestadoraServico = new PrestadoraServico(HttpContextAccessor).ListaPrestadoraServico();
            Colaborador objColaborador = new Colaborador(HttpContextAccessor);
            return View(objColaborador.CarregarRegistro(id));
        }

        // POST: Colaborador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Colaborador formulario)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    formulario.AtualizarRegistro();
                    return RedirectToAction("Index");
                }

                ViewBag.ListaSetor = new Setor(HttpContextAccessor).ListaSetor();
                ViewBag.ListaCargo = new Cargo(HttpContextAccessor).ListaCargo();
                ViewBag.ListaOcorrencia = new Ocorrencia(HttpContextAccessor).ListaOcorrencia();
                ViewBag.ListaPrestadoraServico = new PrestadoraServico(HttpContextAccessor).ListaPrestadoraServico();
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Colaborador/Delete/5
        public ActionResult Delete(int? id)
        {
            Colaborador objColaborador = new Colaborador(HttpContextAccessor);
            objColaborador.ExcluirRegistro(id);
            return View("Index");
        }

        // POST: Colaborador/Delete/5
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