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
    public class HistoricoManutencao
    {
        [Display(Name = "Id")]
        public int idHistoricoManutencao { get; set; }
        [Display(Name = "Data Atendida")]
        public string Data { get; set; }
        [Display(Name = "O.S")]
        public int Os { get; set; }
        public int manutencao_idmanutencao { get; set; }
        public int status_manutencao_idstatus_manutencao { get; set; }
        public int usuario_idusuario { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        [Display(Name = "Data Solicitação")]
        public string DataManut { get; set; }
        [Display(Name = "Setor")]
        public string NomeSe { get; set; }
        [Display(Name = "Bairro")]
        public string BairroSe { get; set; }
        [Display(Name = "Responsavel")]
        public string NomeUsu { get; set; }
        [Display(Name = "Status")]
        public string NomeStatus { get; set; }
        [Display(Name = "Item")]
        public string NomeItem { get; set; }



        public IHttpContextAccessor HttpContextAccessor;

        public HistoricoManutencao()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public HistoricoManutencao(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }


        public List<HistoricoManutencao> ListaHistoricoManutencao()
        {
            List<HistoricoManutencao> lista = new List<HistoricoManutencao>();
            HistoricoManutencao item;
            string id_setor_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdSetorUsuarioLogado");
            string sql = " select " +
                " historico_manutencao.idhistorico_manutencao , historico_manutencao.data, historico_manutencao.os, historico_manutencao.manutencao_idmanutencao, historico_manutencao.status_manutencao_idstatus_manutencao, historico_manutencao.usuario_idusuario," +
                " manutencao.observacao, manutencao.data as DataManut, manutencao.flg, " +
                " setor.nome as NomeSe, setor.bairro as BairroSe, " +
                " usuario.nome as NomeUsu," +
                " status_manutencao.nome as NomeStatus, " +
                " item.nome as NomeItem " +
                " from historico_manutencao" +
                " inner join manutencao on manutencao.idmanutencao = historico_manutencao.manutencao_idmanutencao" +
                " inner join setor on setor.idsetor = manutencao.setor_idsetor" +
                " inner join usuario on usuario.idusuario = historico_manutencao.usuario_idusuario" +
                " inner join status_manutencao on status_manutencao.idstatus_manutencao = historico_manutencao.status_manutencao_idstatus_manutencao " +
                " inner join item on manutencao.item_iditem = item.iditem " +
                " WHERE manutencao.flg = 11  " +
                " ORDER BY historico_manutencao.data DESC ";


            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new HistoricoManutencao();
                item.idHistoricoManutencao = int.Parse(dt.Rows[i]["idhistorico_manutencao"].ToString());
                item.Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyy");
                item.Os = int.Parse(dt.Rows[i]["os"].ToString());
                item.manutencao_idmanutencao = int.Parse(dt.Rows[i]["manutencao_idmanutencao"].ToString());
                item.status_manutencao_idstatus_manutencao = int.Parse(dt.Rows[i]["status_manutencao_idstatus_manutencao"].ToString());
                item.usuario_idusuario = int.Parse(dt.Rows[i]["usuario_idusuario"].ToString());
                item.Observacao = dt.Rows[i]["observacao"].ToString();
                item.DataManut = DateTime.Parse(dt.Rows[i]["DataManut"].ToString()).ToString("dd/MM/yyy");
                item.NomeSe = dt.Rows[i]["NomeSe"].ToString();
                item.BairroSe = dt.Rows[i]["BairroSe"].ToString();
                item.NomeUsu = dt.Rows[i]["NomeUsu"].ToString();
                item.NomeStatus = dt.Rows[i]["NomeStatus"].ToString();
                item.NomeItem = dt.Rows[i]["NomeItem"].ToString();
                lista.Add(item);
            }
            return lista;
        }

     


        public void AddStatus()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            var data = DateTime.Now.ToString("yyyy/MM/dd");
            string sql = $"INSERT INTO historico_manutencao (data, os, manutencao_idmanutencao, status_manutencao_idstatus_manutencao, usuario_idusuario) VALUES ('{data}', '{Os}', '{manutencao_idmanutencao}', 11, '{id_usuario_logado}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

        public void NovaOs()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            var data = DateTime.Now.ToString("yyyy/MM/dd");
            string sql = $"INSERT INTO historico_manutencao (data, os, manutencao_idmanutencao, status_manutencao_idstatus_manutencao, usuario_idusuario) VALUES ('{data}', '{Os}', '{manutencao_idmanutencao}', 10, '{id_usuario_logado}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }


    }
}
