using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class Item
    {
        public int IdItem { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }

        IHttpContextAccessor HttpContextAccessor;

        public Item()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public Item(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }


        public List<Item> ListaItem()
        {
            List<Item> lista = new List<Item>();
            Item item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT iditem, nome, observacao FROM item order by nome";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Item();
                item.IdItem = int.Parse(dt.Rows[i]["iditem"].ToString());
                item.Nome = dt.Rows[i]["nome"].ToString();
                item.Observacao = dt.Rows[i]["observacao"].ToString();
                lista.Add(item);
            }
            return lista;

        }

        public void SalvarNovoRegistro()
        {
            string sql = $"INSERT INTO item (nome, observacao) VALUES ('{Nome}', '{Observacao}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }


        public Item CarregarRegistro(int? id)
        {
            Item item = new Item();
            string sql = $"SELECT iditem, nome, observacao FROM item WHERE iditem = {id}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            item.IdItem = int.Parse(dt.Rows[0]["iditem"].ToString());
            item.Nome = dt.Rows[0]["nome"].ToString();
            item.Observacao = dt.Rows[0]["observacao"].ToString();

            return item;
        }


        public void AtualizarRegistro()
        {
            string sql = $"UPDATE item SET nome = '{Nome}', observacao = '{Observacao}' WHERE  iditem = {IdItem}";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

    }
}
