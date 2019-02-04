using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class Manutencao
    {
        [Display(Name = "Id")]
        public int idManutencao { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        [Display(Name = "Qtd")]
        public int Quantidade { get; set; }
        [Display(Name = "Prioridade")]
        public string Prioridade { get; set; }
        [Display(Name = "Data da Solicitação")]
        public string Data { get; set; }
        public int Flg { get; set; }
        public int setor_idsetor { get; set; }
        public int item_iditem { get; set; }

        [Display(Name = "Setor")]
        public string NomeSe { get; set; }
        [Display(Name = "Item")]
        public string NomeItem { get; set; }
        public string Os { get; set; }
        public string DataAtendida { get; set; }
        public string Status { get; set; }

        public IHttpContextAccessor HttpContextAccessor;

        public Manutencao()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public Manutencao(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public List<Manutencao> ListaManutencao()
        {
            List<Manutencao> lista = new List<Manutencao>();
            Manutencao item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT" +
                " manutencao.idmanutencao, manutencao.observacao, manutencao.quantidade, manutencao.prioridade, manutencao.data, manutencao.setor_idsetor, manutencao.item_iditem," +
                " setor.nome as NomeSe," +
                " item.nome as NomeItem " +
                " FROM 9256_sispmscore.manutencao" +
                " inner join setor on manutencao.setor_idsetor = setor.idsetor" +
                " inner join item on manutencao.item_iditem = item.iditem  " +
                " WHERE flg = 12 ";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Manutencao();
                item.idManutencao = int.Parse(dt.Rows[i]["idmanutencao"].ToString());
                item.Observacao = dt.Rows[i]["observacao"].ToString();
                item.Quantidade = int.Parse(dt.Rows[i]["quantidade"].ToString());
                item.Prioridade = dt.Rows[i]["prioridade"].ToString();
                item.Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyy");
                item.setor_idsetor = int.Parse(dt.Rows[i]["setor_idsetor"].ToString());
                item.item_iditem = int.Parse(dt.Rows[i]["item_iditem"].ToString());
                item.NomeSe = dt.Rows[i]["NomeSe"].ToString();
                item.NomeItem = dt.Rows[i]["NomeItem"].ToString();
                lista.Add(item);
            }
            return lista;

        }

        public List<Manutencao> ListaManutencaoConcluidas()
        {
            List<Manutencao> lista = new List<Manutencao>();
            Manutencao item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT" +
                " manutencao.idmanutencao, manutencao.observacao, manutencao.quantidade, manutencao.prioridade, manutencao.data, manutencao.setor_idsetor, manutencao.item_iditem," +
                " setor.nome as NomeSe," +
                " item.nome as NomeItem " +
                " FROM 9256_sispmscore.manutencao" +
                " inner join setor on manutencao.setor_idsetor = setor.idsetor" +
                " inner join item on manutencao.item_iditem = item.iditem  " +
                " WHERE flg = 10 ";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Manutencao();
                item.idManutencao = int.Parse(dt.Rows[i]["idmanutencao"].ToString());
                item.Observacao = dt.Rows[i]["observacao"].ToString();
                item.Quantidade = int.Parse(dt.Rows[i]["quantidade"].ToString());
                item.Prioridade = dt.Rows[i]["prioridade"].ToString();
                item.Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyy");
                item.setor_idsetor = int.Parse(dt.Rows[i]["setor_idsetor"].ToString());
                item.item_iditem = int.Parse(dt.Rows[i]["item_iditem"].ToString());
                item.NomeSe = dt.Rows[i]["NomeSe"].ToString();
                item.NomeItem = dt.Rows[i]["NomeItem"].ToString();
                lista.Add(item);
            }
            return lista;

        }

        public List<Manutencao> FiltrarManutencaoPorSetor(int id)
        {
            List<Manutencao> lista = new List<Manutencao>();
            Manutencao item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = " select " +
                " manutencao.idmanutencao, manutencao.observacao, manutencao.quantidade, manutencao.prioridade, manutencao.data, manutencao.setor_idsetor, manutencao.item_iditem," +
                " setor.nome as NomeSe," +
                " item.nome as NomeItem," +
                " historico_manutencao.os, historico_manutencao.data as DataAtendida," +
                " status_manutencao.nome as Status " +
                " from manutencao" +
                " inner join setor on manutencao.setor_idsetor = setor.idsetor" +
                " inner join item on manutencao.item_iditem = item.iditem" +
                " inner join historico_manutencao on manutencao.idmanutencao = historico_manutencao.manutencao_idmanutencao" +
                " inner join status_manutencao on historico_manutencao.status_manutencao_idstatus_manutencao = status_manutencao.idstatus_manutencao " +
                $" where manutencao.setor_idsetor = {id} ";

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Manutencao();
                item.idManutencao = int.Parse(dt.Rows[i]["idmanutencao"].ToString());
                item.Observacao = dt.Rows[i]["observacao"].ToString();
                item.Quantidade = int.Parse(dt.Rows[i]["quantidade"].ToString());
                item.Prioridade = dt.Rows[i]["prioridade"].ToString();
                item.Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyy");
                item.setor_idsetor = int.Parse(dt.Rows[i]["setor_idsetor"].ToString());
                item.item_iditem = int.Parse(dt.Rows[i]["item_iditem"].ToString());
                item.NomeSe = dt.Rows[i]["NomeSe"].ToString();
                item.NomeItem = dt.Rows[i]["NomeItem"].ToString();
                item.Os = dt.Rows[i]["os"].ToString();
                item.DataAtendida = DateTime.Parse(dt.Rows[i]["DataAtendida"].ToString()).ToString("dd/MM/yyy");
                item.Status = dt.Rows[i]["Status"].ToString();
                lista.Add(item);
            }
            return lista;

        }

        public List<Manutencao> FiltrarManutencaoPorSetorPorStatus(int id, int idStatus)
        {
            List<Manutencao> lista = new List<Manutencao>();
            Manutencao item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = " select " +
                " manutencao.idmanutencao, manutencao.observacao, manutencao.quantidade, manutencao.prioridade, manutencao.data, manutencao.setor_idsetor, manutencao.item_iditem," +
                " setor.nome as NomeSe," +
                " item.nome as NomeItem," +
                " historico_manutencao.os, historico_manutencao.data as DataAtendida," +
                " status_manutencao.nome as Status " +
                " from manutencao" +
                " inner join setor on manutencao.setor_idsetor = setor.idsetor" +
                " inner join item on manutencao.item_iditem = item.iditem" +
                " inner join historico_manutencao on manutencao.idmanutencao = historico_manutencao.manutencao_idmanutencao" +
                " inner join status_manutencao on historico_manutencao.status_manutencao_idstatus_manutencao = status_manutencao.idstatus_manutencao " +
                $" where manutencao.setor_idsetor = {id} and   manutencao.flg = {idStatus}";

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Manutencao();
                item.idManutencao = int.Parse(dt.Rows[i]["idmanutencao"].ToString());
                item.Observacao = dt.Rows[i]["observacao"].ToString();
                item.Quantidade = int.Parse(dt.Rows[i]["quantidade"].ToString());
                item.Prioridade = dt.Rows[i]["prioridade"].ToString();
                item.Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyy");
                item.setor_idsetor = int.Parse(dt.Rows[i]["setor_idsetor"].ToString());
                item.item_iditem = int.Parse(dt.Rows[i]["item_iditem"].ToString());
                item.NomeSe = dt.Rows[i]["NomeSe"].ToString();
                item.NomeItem = dt.Rows[i]["NomeItem"].ToString();
                item.Os = dt.Rows[i]["os"].ToString();
                item.DataAtendida = DateTime.Parse(dt.Rows[i]["DataAtendida"].ToString()).ToString("dd/MM/yyy");
                item.Status = dt.Rows[i]["Status"].ToString();
                lista.Add(item);
            }
            return lista;

        }

        public void SalvarNovoRegistro()
        {
            var data = DateTime.Now.ToString("yyyy/MM/dd");
            string sql = $"INSERT INTO manutencao (observacao, quantidade, prioridade, data, flg, setor_idsetor, item_iditem) VALUES ('{Observacao}', '{Quantidade}', '{Prioridade}', '{data}', 12, '{setor_idsetor}', '{item_iditem}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }


        public void AtualizarFlgManutencao()
        {
            string sql = $"UPDATE manutencao SET flg = 11 WHERE  idmanutencao = {idManutencao}";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

        public void AtualizarFlgManutencaoConcluido(int id)
        {
            string sql = $"UPDATE manutencao SET flg = 10 WHERE  idmanutencao = {id}";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

    }
}
