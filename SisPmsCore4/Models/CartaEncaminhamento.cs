using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class CartaEncaminhamento
    {
        public int IdColaborador { get; set; }



        IHttpContextAccessor HttpContextAccessor;

        public CartaEncaminhamento()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public CartaEncaminhamento(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }


        public List<CartaEncaminhamento> RetornaCartaEncaminhamento(string id)
        {
            List<CartaEncaminhamento> lista = new List<CartaEncaminhamento>();
            CartaEncaminhamento item;
            string id_setor_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdSetorUsuarioLogado");
            string sql = "SELECT " +
                " historico.idhistorico, historico.data, historico.observacao, colaborador.idcolaborador, historico.setor_idsetor, historico.usuario_idusuario, " +
                " colaborador.idcolaborador, colaborador.nome, colaborador.cpf, colaborador.data_admissao, colaborador.telefone, colaborador.cargo_idcargo, colaborador.ocorrencia_idocorrencia, " +
                " setor.idsetor, setor.nome, setor.telefone1, setor.telefone2, setor.cep, setor.bairro, setor.logradouro, setor.numero, setor.gestor, " +
                " cargo.nome, " +
                " ocorrencia.descricao " +
                " from historico " +
                " inner join colaborador " +
                " on historico.colaborador_idcolaborador = colaborador.idcolaborador " +
                " inner join setor" +
                " on historico.setor_idsetor = setor.idsetor" +
                " inner join cargo " +
                " on colaborador.cargo_idcargo = cargo.idcargo " +
                " inner join ocorrencia" +
                " on colaborador.ocorrencia_idocorrencia = ocorrencia.idocorrencia" +
                $" where colaborador.idcolaborador = {id} ";
               


            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new CartaEncaminhamento();
                item.IdColaborador = int.Parse(dt.Rows[i]["IdColaborador"].ToString());
                lista.Add(item);
            }

            return lista;
        }

        public List<CartaEncaminhamento> copia()
        {
            List<CartaEncaminhamento> lista = new List<CartaEncaminhamento>();
            CartaEncaminhamento item;
            string id_setor_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdSetorUsuarioLogado");
            string sql = " select " +
                " colaborador.idcolaborador as IdColaborador, colaborador.nome as NomeCo, colaborador.telefone as TelCo, colaborador.cpf as CpfCo, colaborador.data_admissao as DataCo,  " +
                " setor.idsetor as IdSetor, setor.nome as NomeSe, setor.cep as CepSe, setor.bairro as BairroSe, setor.logradouro as LogradouroSe, setor.numero as NumeroSe, setor.gestor as GestorSe, " +
                " ocorrencia.descricao as DescricaoOco, " +
                " cargo.nome as NomeCa, " +
                " historico.setor_idsetor as IdSeHis, historico.data as DataHis " +
                " from colaborador " +
                " inner join setor " +
                " on colaborador.setor_idsetor=setor.idsetor " +
                " inner join ocorrencia " +
                " on colaborador.ocorrencia_idocorrencia=ocorrencia.idocorrencia " +
                " inner join prestadora_servico " +
                " on colaborador.prestadora_servico_idprestadora_servico = prestadora_servico.idprestadora_servico " +
                " inner join cargo " +
                " on colaborador.cargo_idcargo = cargo.idcargo " +
                " inner join historico " +
                " on colaborador.idcolaborador = historico.colaborador_idcolaborador " +
                $" WHERE historico.setor_idsetor = {id_setor_usuario_logado}";


            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new CartaEncaminhamento();
                item.IdColaborador = int.Parse(dt.Rows[i]["IdColaborador"].ToString());
                //item.NomeCo = dt.Rows[i]["NomeCo"].ToString();
                //item.TelCo = dt.Rows[i]["TelCo"].ToString();
                //item.CpfCo = dt.Rows[i]["CpfCo"].ToString();
                //item.DataCo = DateTime.Parse(dt.Rows[0]["DataCo"].ToString()).ToString("dd/MM/yyy");
                //item.IdSetor = int.Parse(dt.Rows[i]["IdSetor"].ToString());
                //item.NomeSe = dt.Rows[i]["NomeSe"].ToString();
                //item.CepSe = dt.Rows[i]["CepSe"].ToString();
                //item.BairroSe = dt.Rows[i]["BairroSe"].ToString();
                //item.LogradouroSe = dt.Rows[i]["LogradouroSe"].ToString();
                //item.NumeroSe = dt.Rows[i]["NumeroSe"].ToString();
                //item.GestorSe = dt.Rows[i]["GestorSe"].ToString();
                //item.DescricaoOco = dt.Rows[i]["DescricaoOco"].ToString();
                //item.NomeCa = dt.Rows[i]["NomeCa"].ToString();
                //item.IdSeHis = int.Parse(dt.Rows[i]["IdSeHis"].ToString());
                //item.DataHis = DateTime.Parse(dt.Rows[0]["DataHis"].ToString()).ToString("dd/MM/yyy");
                lista.Add(item);
            }

            return lista;
        }
    }
}
