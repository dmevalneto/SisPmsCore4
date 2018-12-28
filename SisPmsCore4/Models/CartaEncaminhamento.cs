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
        public string NomeCo { get; set; }
        public string TelCo { get; set; }


        IHttpContextAccessor HttpContextAccessor;

        public CartaEncaminhamento()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public CartaEncaminhamento(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }


        public List<CartaEncaminhamento> RetornaCartaEncaminhamento()
        {
            List<CartaEncaminhamento> lista = new List<CartaEncaminhamento>();
            CartaEncaminhamento item;

            string sql = " select " +
                " colaborador.idcolaborador as IdColaborador, colaborador.nome as NomeCo, colaborador.telefone as TelCo, colaborador.cpf as CpfCo, " +
                " setor.nome as NomeSe, setor.cep as CepSe, setor.bairro as BairroSe, setor.logradouro as LogradouroSe, setor.numero as NumeroSe, setor.gestor as GestorSe," +
                " ocorrencia.descricao as DescricaoOco," +
                " cargo.nome as NomeCa" +
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
                "";


            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new CartaEncaminhamento();
                item.IdColaborador = int.Parse(dt.Rows[i]["IdColaborador"].ToString());
                item.NomeCo = dt.Rows[i]["NomeCo"].ToString();
                item.TelCo = dt.Rows[i]["TelCo"].ToString();
                lista.Add(item);
            }

            return lista;
        }
    }
}
