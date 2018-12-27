using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class TipoSetor
    {
        public int idTipoSetor { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Descricao { get; set; }
        IHttpContextAccessor HttpContextAccessor;

        public TipoSetor()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public TipoSetor(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public List<TipoSetor> ListaTipoSetor()
        {
            List<TipoSetor> lista = new List<TipoSetor>();
            TipoSetor item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT idtipo_setor, nome, descricao FROM tiposetor";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new TipoSetor();
                item.idTipoSetor = int.Parse(dt.Rows[i]["idtipo_setor"].ToString());
                item.Nome = dt.Rows[i]["nome"].ToString();
                item.Descricao = dt.Rows[i]["descricao"].ToString();
                lista.Add(item);
            }
            return lista;

        }

        public void SalvarNovoRegistro()
        {
            string sql = $"INSERT INTO tiposetor (nome, descricao) VALUES ('{Nome}', '{Descricao}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM tiposetor WHERE idtipo_setor = {id}";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
