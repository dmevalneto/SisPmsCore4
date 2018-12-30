using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace SisPmsCore4.Models
{
    public class Historico
    {
        public int idhistorico { get; set; }
        public string data { get; set; }
        public string observacao { get; set; }
        public int colaborador_idcolaborador { get; set; }
        public int setor_idsetor { get; set; }
        public int usuario_idusuario { get; set; }
        public string NomeCol { get; set; }
        public string SetorSe { get; set; }


        IHttpContextAccessor HttpContextAccessor;

        public Historico()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public Historico(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }


        public void Encaminhar()
        {
            string dataConvert = DateTime.Parse(data).ToString("yyyy/MM/dd");
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"INSERT INTO historico (data, observacao, colaborador_idcolaborador, setor_idsetor, usuario_idusuario) VALUES ('{dataConvert}', '{observacao}', {colaborador_idcolaborador}, {setor_idsetor}, {id_usuario_logado})";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

        public List<Historico> HistoricoColaborador()
        {
            List<Historico> lista = new List<Historico>();
            Historico item;
            string id_setor_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdSetorUsuarioLogado");
            string sql = " SELECT  " +
                " historico.idhistorico , historico.data, historico.observacao, colaborador.idcolaborador, historico.setor_idsetor, historico.usuario_idusuario, " +
                " colaborador.nome as NomeCol, " +
                " setor.nome as NomeSe " +
                " from historico  inner join colaborador  on historico.colaborador_idcolaborador = colaborador.idcolaborador  " +
                " inner join setor on historico.setor_idsetor = setor.idsetor " +
                " inner join cargo  on colaborador.cargo_idcargo = cargo.idcargo  " +
                " inner join ocorrencia on colaborador.ocorrencia_idocorrencia = ocorrencia.idocorrencia " +
                " order by historico.data DESC";


            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Historico();
                item.idhistorico = int.Parse(dt.Rows[i]["idhistorico"].ToString());
                item.NomeCol = dt.Rows[i]["NomeCol"].ToString();
                item.SetorSe = dt.Rows[i]["NomeSe"].ToString();
                item.data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyy");
                lista.Add(item);
            }
            return lista;
        }

    }
}
