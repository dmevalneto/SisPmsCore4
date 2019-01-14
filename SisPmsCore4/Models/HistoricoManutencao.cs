using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class HistoricoManutencao
    {
        public int idHistoricoManutencao { get; set; }
        public string Data { get; set; }
        public int manutencao_idmanutencao { get; set; }
        public int status_manutencao_idstatus_manutencao { get; set; }
        public int usuario_idusuario { get; set; }

        public string Observacao { get; set; }
        public string DataManut { get; set; }
        public string NomeSe { get; set; }
        public string NomeUsu { get; set; }
        public string NomeStatus { get; set; }


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
                " historico_manutencao.idhistorico_manutencao , historico_manutencao.data, historico_manutencao.manutencao_idmanutencao, historico_manutencao.status_manutencao_idstatus_manutencao, historico_manutencao.usuario_idusuario," +
                " manutencao.observacao, manutencao.data as DataManut," +
                " setor.nome as NomeSe," +
                " usuario.nome as NomeUsu," +
                " status_manutencao.nome as NomeStatus" +
                " from historico_manutencao" +
                " inner join manutencao on manutencao.idmanutencao = historico_manutencao.manutencao_idmanutencao" +
                " inner join setor on setor.idsetor = manutencao.setor_idsetor" +
                " inner join usuario on usuario.idusuario = historico_manutencao.usuario_idusuario" +
                " inner join status_manutencao on status_manutencao.idstatus_manutencao = historico_manutencao.status_manutencao_idstatus_manutencao " +
                " ORDER BY historico_manutencao.data DESC ";


            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new HistoricoManutencao();
                item.idHistoricoManutencao = int.Parse(dt.Rows[i]["idhistorico_manutencao"].ToString());
                item.Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyy");
                item.manutencao_idmanutencao = int.Parse(dt.Rows[i]["manutencao_idmanutencao"].ToString());
                item.status_manutencao_idstatus_manutencao = int.Parse(dt.Rows[i]["status_manutencao_idstatus_manutencao"].ToString());
                item.usuario_idusuario = int.Parse(dt.Rows[i]["usuario_idusuario"].ToString());
                item.Observacao = dt.Rows[i]["observacao"].ToString();
                item.DataManut = DateTime.Parse(dt.Rows[i]["DataManut"].ToString()).ToString("dd/MM/yyy");
                item.NomeSe = dt.Rows[i]["NomeSe"].ToString();
                item.NomeUsu = dt.Rows[i]["NomeUsu"].ToString();
                item.NomeStatus = dt.Rows[i]["NomeStatus"].ToString();
                lista.Add(item);
            }
            return lista;
        }


        public void AddStatus()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            var data = DateTime.Now.ToString("yyyy/MM/dd");
            string sql = $"INSERT INTO historico_manutencao (data, manutencao_idmanutencao, status_manutencao_idstatus_manutencao, usuario_idusuario) VALUES ('{data}', '{manutencao_idmanutencao}', '{status_manutencao_idstatus_manutencao}', '{id_usuario_logado}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
