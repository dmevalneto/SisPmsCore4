using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Data;
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
        public string NomeSetor { get; set; }
        public string Data { get; set; }

        public IHttpContextAccessor HttpContextAccessor;

        public Anotacoes()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public Anotacoes(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public List<Anotacoes> ListaAnotacoes()
        {
            List<Anotacoes> lista = new List<Anotacoes>();
            Anotacoes item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = " SELECT " +
                " anotacoes_setor.idanotacoes, anotacoes_setor.nota , anotacoes_setor.setor_idsetor , anotacoes_setor.usuario_idusuario, anotacoes_setor.data, setor.nome " +
                " FROM anotacoes_setor " +
                " inner join setor on setor.idsetor = anotacoes_setor.setor_idsetor " +
                $" where usuario_idusuario = {id_usuario_logado} ORDER BY data";

            //string sql = $"SELECT * FROM 9256_sispmscore.anotacoes_setor WHERE usuario_idusuario = {id_usuario_logado}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Anotacoes();
                item.IdAnotacoes = int.Parse(dt.Rows[i]["idanotacoes"].ToString());
                item.Nota = dt.Rows[i]["nota"].ToString();
                item.setor_idsetor = int.Parse(dt.Rows[i]["setor_idsetor"].ToString());
                item.usuario_idusuario = int.Parse(dt.Rows[i]["usuario_idusuario"].ToString());
                item.NomeSetor = dt.Rows[i]["nome"].ToString();
                item.Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyy");
                lista.Add(item);
            }
            return lista;

        }

        public void SalvarNovoRegistro()
        {
            var data = DateTime.Now.ToString("yyyy/MM/dd");
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"INSERT INTO anotacoes_setor (nota, setor_idsetor, usuario_idusuario, data) VALUES ('{Nota}', {setor_idsetor}, {id_usuario_logado}, '{data}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);


        }
    }
}
