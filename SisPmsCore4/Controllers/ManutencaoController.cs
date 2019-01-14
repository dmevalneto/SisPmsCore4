﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SisPmsCore4.Models;

namespace SisPmsCore4.Controllers
{
    public class ManutencaoController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;
        public ManutencaoController(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListarManutencao()
        {
            ViewBag.ListaManutencao = new Manutencao(HttpContextAccessor).ListaManutencao();
            return View();
        }

        public IActionResult ListarHistoricoManutencao()
        {
            ViewBag.ListarHistoricoManutencao = new HistoricoManutencao(HttpContextAccessor).ListaHistoricoManutencao();
            return View();
        }

        [HttpGet]
        public IActionResult AddStatus(int id)
        {
            TempData["idManutencao"] = id;
            TempData.Keep("idManutencao");
            ViewBag.ListaStatusManutencao = new StatusManutencao(HttpContextAccessor).ListaStatusManutencao();
            return View();
        }

        [HttpPost]
        public IActionResult AddStatus(HistoricoManutencao formulario)
        {
            ViewBag.idManutencao = TempData["idManutencao"].ToString();
            formulario.manutencao_idmanutencao = int.Parse(ViewBag.idManutencao);
            formulario.HttpContextAccessor = HttpContextAccessor;
            formulario.AddStatus();
            return RedirectToAction("ListarHistoricoManutencao");
        }

        // GET: Ocorrencia/Create
        public ActionResult Create()
        {
            ViewBag.ListaSetor = new Setor(HttpContextAccessor).ListaSetor();
            ViewBag.ListaItem = new Item(HttpContextAccessor).ListaItem();
            return View();
        }

        // POST: Ocorrencia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Manutencao formulario)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    formulario.SalvarNovoRegistro();
                    return RedirectToAction("ListarManutencao");
                }
                ViewBag.ListaSetor = new Setor(HttpContextAccessor).ListaSetor();
                ViewBag.ListaItem = new Item(HttpContextAccessor).ListaItem();
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}