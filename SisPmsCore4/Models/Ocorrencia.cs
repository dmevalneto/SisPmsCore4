using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class Ocorrencia
    {
        public int idOcorrencia { get; set; }
        public int Numero { get; set; }
        public string Descricao { get; set; }

        IHttpContextAccessor HttpContextAccessor;

        public Ocorrencia()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public Ocorrencia(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public List<Ocorrencia> ListaOcorrencia()
        {
            List<Ocorrencia> lista = new List<Ocorrencia>();
            Ocorrencia item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT * FROM ocorrencia";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Ocorrencia();
                item.idOcorrencia = int.Parse(dt.Rows[i]["idocorrencia"].ToString());
                item.Numero = int.Parse(dt.Rows[i]["numero"].ToString());
                item.Descricao = dt.Rows[i]["descricao"].ToString();
                lista.Add(item);
            }
            return lista;

        }

        public void SalvarNovoRegistro()
        {
            string sql = $"INSERT INTO ocorrencia (numero, descricao) VALUES ('{Numero}', '{Descricao}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
