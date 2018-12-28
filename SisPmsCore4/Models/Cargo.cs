using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class Cargo
    {
        public int idCargo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        IHttpContextAccessor HttpContextAccessor;

        public Cargo()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public Cargo(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public List<Cargo> ListaCargo()
        {
            List<Cargo> lista = new List<Cargo>();
            Cargo item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT idcargo, nome, descricao FROM cargo";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Cargo();
                item.idCargo = int.Parse(dt.Rows[i]["idcargo"].ToString());
                item.Nome = dt.Rows[i]["nome"].ToString();
                item.Descricao = dt.Rows[i]["descricao"].ToString();
                lista.Add(item);
            }
            return lista;

        }

        public void SalvarNovoRegistro()
        {
            string sql = $"INSERT INTO cargo (nome, descricao) VALUES ('{Nome}', '{Descricao}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
