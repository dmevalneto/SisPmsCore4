using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class Anotacoes
    {
        public int IdAnotacoes { get; set; }
        public string Nota { get; set; }
        public int setor_idsetor { get; set; }
        public int usuario_idusuario { get; set; }

        IHttpContextAccessor HttpContextAccessor;

        public Anotacoes()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public Anotacoes(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        //public List<Anotacoes> ListaAnotacoes()
        //{
        //    List<Anotacoes> lista = new List<Anotacoes>();
        //    Anotacoes item;

        //    string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
        //    string sql = $"SELECT * FROM 9256_sispmscore.anotacoes_setor WHERE usuario_idusuario = {usuario_idusuario}";
        //    DAL objDAL = new DAL();
        //    DataTable dt = objDAL.RetDataTable(sql);

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        item = new Cargo();
        //        item.idCargo = int.Parse(dt.Rows[i]["idcargo"].ToString());
        //        item.Nome = dt.Rows[i]["nome"].ToString();
        //        item.Descricao = dt.Rows[i]["descricao"].ToString();
        //        lista.Add(item);
        //    }
        //    return lista;

        //}
    }
}
