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
            Historico item = new Historico();
            string id_setor_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdSetorUsuarioLogado");
            string sql = " SELECT  " +
                " historico.idhistorico , historico.data, historico.observacao, colaborador.idcolaborador, historico.setor_idsetor, historico.usuario_idusuario, " +
                " colaborador.nome as NomeCol, cpf as CpfCol, telefone as telefoneCol, " +
                " setor.nome as NomeSe, setor.gestor as GestorSe, " +
                " cargo.nome as NomeCargo, " +
                " prestadora_servico.razao_social " +
                " from historico  inner join colaborador  on historico.colaborador_idcolaborador = colaborador.idcolaborador  " +
                " inner join setor on historico.setor_idsetor = setor.idsetor " +
                " inner join cargo  on colaborador.cargo_idcargo = cargo.idcargo  " +
                " inner join ocorrencia on colaborador.ocorrencia_idocorrencia = ocorrencia.idocorrencia " +
                " inner join prestadora_servico on colaborador.prestadora_servico_idprestadora_servico = prestadora_servico.idprestadora_servico " +
                $" WHERE historico.idhistorico = {id}" +
                " order by historico.data DESC";



            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            item.idhistorico = int.Parse(dt.Rows[0]["idhistorico"].ToString());
            item.NomeCol = dt.Rows[0]["NomeCol"].ToString();
            item.SetorSe = dt.Rows[0]["NomeSe"].ToString();
            item.data = DateTime.Parse(dt.Rows[0]["data"].ToString()).ToString("dd/MM/yyy");
            item.CpfCol = dt.Rows[0]["CpfCol"].ToString();
            item.TelefoneCol = dt.Rows[0]["telefoneCol"].ToString();
            item.RazaoSocial = dt.Rows[0]["razao_social"].ToString();
            item.NomeCargo = dt.Rows[0]["NomeCargo"].ToString();
            item.GestorSe = dt.Rows[0]["GestorSe"].ToString();

            ViewBag.IdHistorico = item.idhistorico;
            ViewBag.NomeCol = item.NomeCol;
            ViewBag.SetorSe = item.SetorSe;
            ViewBag.Data = item.data;
            ViewBag.CpfCol = item.CpfCol;
            ViewBag.TelefoneCol = item.TelefoneCol;
            ViewBag.RazaoSocial = item.RazaoSocial;
            ViewBag.NomeCargo = item.NomeCargo;
            ViewBag.GestorSe = item.GestorSe;
            return View();
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