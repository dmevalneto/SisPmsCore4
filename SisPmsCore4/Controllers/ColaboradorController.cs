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
        //primeira view pega o Id do colaborador e lista os setores
        public IActionResult Encaminhar(int? id)
        {
            TempData["idColaborador"] = id;
            TempData.Keep("idColaborador");

            ViewBag.ListaSetor = new Setor(HttpContextAccessor).ListaSetor();
            return View();
        }

        [HttpGet]
        //Segunda view de encaminhamento persiste os dados de colaborador e seleciona os dados do setor a ser encaminhado cadastrando as info no banco
        //e redireciona para a action exibindo os dados do historico 
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



        public IActionResult VisualizarCartaEncaminhamento(int? id)
        {

            Historico objHistorico = new Historico(HttpContextAccessor);
            ViewBag.CartaEncaminhamento = objHistorico.CartaEncaminhamento(id);
            ViewBag.IdHistorico = ViewBag.CartaEncaminhamento.idhistorico;
            ViewBag.NomeCol = ViewBag.CartaEncaminhamento.NomeCol;
            ViewBag.SetorSe = ViewBag.CartaEncaminhamento.SetorSe;
            ViewBag.Data = ViewBag.CartaEncaminhamento.data;
            ViewBag.CpfCol = ViewBag.CartaEncaminhamento.CpfCol;
            ViewBag.TelefoneCol = ViewBag.CartaEncaminhamento.TelefoneCol;
            ViewBag.RazaoSocial = ViewBag.CartaEncaminhamento.RazaoSocial;
            ViewBag.NomeCargo = ViewBag.CartaEncaminhamento.NomeCargo;
            ViewBag.GestorSe = ViewBag.CartaEncaminhamento.GestorSe;
            ViewBag.Observacao = ViewBag.CartaEncaminhamento.observacao;
            ViewBag.Tel1Col = ViewBag.CartaEncaminhamento.Tel1Col;
            ViewBag.Tel1Co2 = ViewBag.CartaEncaminhamento.Tel2Col;
            ViewBag.CepCol = ViewBag.CartaEncaminhamento.CepCol;
            ViewBag.BairroCol = ViewBag.CartaEncaminhamento.BairroCol;
            ViewBag.LogradouroCol = ViewBag.CartaEncaminhamento.LogradouroCol;
            ViewBag.CarregarSetorUsuario = new Setor(HttpContextAccessor).CarregarSetorUsuario();
            return View();
        }

        public IActionResult FiltrarColaborador(Colaborador form)
        {
            if (form.Nome == null)
            {
                ViewBag.Error = " Por favor digite um nome para a pesquisa !!";
                return View("Index");
            }
            else
            {
                ViewBag.FiltrarColaborador = new Colaborador(HttpContextAccessor).FiltrarColaborador(form.Nome);
               
            }
            return View();
        }



        // GET: Colaborador/Edit/5
        public ActionResult AtualizarObservacao(int? id)
        {
            Historico objHistorico = new Historico(HttpContextAccessor);
            ViewBag.Desc = objHistorico.CarregarObsercacao(id);
            return View();
        }

        // POST: Colaborador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AtualizarObservacao(Historico formulario)
        {

            // TODO: Add update logic here

            formulario.AtualizarObservacao();
            return RedirectToAction("HistoricoColaborador");

        }



        // GET: Colaborador/Details/5
        public ActionResult PesquisarHistoricoPorColaborador(int id)
        {
            Historico objHistorico = new Historico();
            ViewBag.PesquisarHistoricoPorColaborador = objHistorico.PesquisarHistoricoPorColaborador(id);
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
            try
            {
                Colaborador objColaborador = new Colaborador(HttpContextAccessor);
                objColaborador.ExcluirRegistro(id);
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Contate o Administrador de Sistema!!" + (ex.Message);
                return View("Index");
            }

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