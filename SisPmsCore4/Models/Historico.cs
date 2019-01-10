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
        public string CpfCol { get; set; }
        public string TelefoneCol { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeCargo { get; set; }
        public string GestorSe { get; set; }
        public string Tel1Col { get; set; }
        public string Tel2Col { get; set; }
        public string CepCol { get; set; }
        public string BairroCol { get; set; }
        public string LogradouroCol { get; set; }


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


        public void AtualizarObservacao()
        {
            string sql = $"UPDATE historico SET observacao = '{observacao}' WHERE  idhistorico = {idhistorico}";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

        public Historico CarregarObsercacao(int? id)
        {
            Historico item = new Historico();
            string sql = $"SELECT idhistorico, observacao FROM historico WHERE idhistorico = {id}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            item.idhistorico = int.Parse(dt.Rows[0]["idhistorico"].ToString());
            item.observacao = dt.Rows[0]["observacao"].ToString();

            return item;
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



        public List<Historico> PesquisarHistoricoPorColaborador(int id)
        {
            List<Historico> lista = new List<Historico>();
            Historico item;
         //   string id_setor_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdSetorUsuarioLogado");
            string sql = " SELECT  " +
                " historico.idhistorico , historico.data, historico.observacao, colaborador.idcolaborador, historico.setor_idsetor, historico.usuario_idusuario, " +
                " colaborador.nome as NomeCol, " +
                " setor.nome as NomeSe " +
                " from historico  inner join colaborador  on historico.colaborador_idcolaborador = colaborador.idcolaborador  " +
                " inner join setor on historico.setor_idsetor = setor.idsetor " +
                " inner join cargo  on colaborador.cargo_idcargo = cargo.idcargo  " +
                " inner join ocorrencia on colaborador.ocorrencia_idocorrencia = ocorrencia.idocorrencia " +
                $" WHERE colaborador_idcolaborador = {id}  " +
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


        public Historico CartaEncaminhamento(int? id)
        {
            Historico item = new Historico();
            string id_setor_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdSetorUsuarioLogado");
            string sql = " SELECT  " +
                " historico.idhistorico , historico.data, historico.observacao, colaborador.idcolaborador, historico.setor_idsetor, historico.usuario_idusuario, " +
                " colaborador.nome as NomeCol, cpf as CpfCol, telefone as telefoneCol, " +
                " setor.nome as NomeSe, setor.gestor as GestorSe, setor.telefone1 as Tel1Col, setor.telefone2 as Tel2Col, setor.cep as CepCol, setor.bairro as BairroCol, setor.logradouro as LogradouroCol,   " +
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
            item.observacao = dt.Rows[0]["observacao"].ToString();
            item.NomeCol = dt.Rows[0]["NomeCol"].ToString();
            item.SetorSe = dt.Rows[0]["NomeSe"].ToString();
            item.data = DateTime.Parse(dt.Rows[0]["data"].ToString()).ToString("dd/MM/yyy");
            item.CpfCol = dt.Rows[0]["CpfCol"].ToString();
            item.TelefoneCol = dt.Rows[0]["telefoneCol"].ToString();
            item.RazaoSocial = dt.Rows[0]["razao_social"].ToString();
            item.NomeCargo = dt.Rows[0]["NomeCargo"].ToString();
            item.GestorSe = dt.Rows[0]["GestorSe"].ToString();
            item.Tel1Col = dt.Rows[0]["Tel1Col"].ToString();
            item.Tel2Col = dt.Rows[0]["Tel2Col"].ToString();
            item.CepCol = dt.Rows[0]["CepCol"].ToString();
            item.BairroCol = dt.Rows[0]["BairroCol"].ToString();
            item.LogradouroCol = dt.Rows[0]["LogradouroCol"].ToString();

            return item;
        }

    }
}

