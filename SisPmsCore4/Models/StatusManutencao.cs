using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class StatusManutencao
    {
        public int IdStatusManutencao { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }

        IHttpContextAccessor HttpContextAccessor;

        public StatusManutencao()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public StatusManutencao(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public List<StatusManutencao> ListaStatusManutencao()
        {
            List<StatusManutencao> lista = new List<StatusManutencao>();
            StatusManutencao item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT * FROM status_manutencao";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new StatusManutencao();
                item.IdStatusManutencao = int.Parse(dt.Rows[i]["idstatus_manutencao"].ToString());
                item.Nome = dt.Rows[i]["nome"].ToString();
                item.Observacao = dt.Rows[i]["observacao"].ToString();
                lista.Add(item);
            }
            return lista;

        }

        public void SalvarNovoRegistro()
        {
            string sql = $"INSERT INTO status_manutencao (nome, observacao) VALUES ('{Nome}', '{Observacao}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
