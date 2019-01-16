using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class Manutencao
    {
        public int idManutencao { get; set; }
        public string Observacao { get; set; }
        public int Quantidade { get; set; }
        public string Prioridade { get; set; }
        public string Data { get; set; }
        public int Flg { get; set; }
        public int setor_idsetor { get; set; }
        public int item_iditem { get; set; }

        public string NomeSe { get; set; }
        public string NomeItem { get; set; }

       public IHttpContextAccessor HttpContextAccessor;

        public Manutencao()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public Manutencao(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public List<Manutencao> ListaManutencao()
        {
            List<Manutencao> lista = new List<Manutencao>();
            Manutencao item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT" +
                " manutencao.idmanutencao, manutencao.observacao, manutencao.quantidade, manutencao.prioridade, manutencao.data, manutencao.setor_idsetor, manutencao.item_iditem," +
                " setor.nome as NomeSe," +
                " item.nome as NomeItem " +
                " FROM 9256_sispmscore.manutencao" +
                " inner join setor on manutencao.setor_idsetor = setor.idsetor" +
                " inner join item on manutencao.item_iditem = item.iditem  " +
                " WHERE flg = 0 ";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Manutencao();
                item.idManutencao = int.Parse(dt.Rows[i]["idmanutencao"].ToString());
                item.Observacao = dt.Rows[i]["observacao"].ToString();
                item.Quantidade = int.Parse(dt.Rows[i]["quantidade"].ToString());
                item.Prioridade = dt.Rows[i]["prioridade"].ToString();
                item.Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyy");
                item.setor_idsetor = int.Parse(dt.Rows[i]["setor_idsetor"].ToString());
                item.item_iditem = int.Parse(dt.Rows[i]["item_iditem"].ToString());
                item.NomeSe = dt.Rows[i]["NomeSe"].ToString();
                item.NomeItem = dt.Rows[i]["NomeItem"].ToString();
                lista.Add(item);
            }
            return lista;

        }

        public void SalvarNovoRegistro()
        {
            var data = DateTime.Now.ToString("yyyy/MM/dd");
            string sql = $"INSERT INTO manutencao (observacao, quantidade, prioridade, data, flg, setor_idsetor, item_iditem) VALUES ('{Observacao}', '{Quantidade}', '{Prioridade}', '{data}', 0, '{setor_idsetor}', '{item_iditem}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }


        public void AtualizarFlgManutencao()
        {
            string sql = $"UPDATE manutencao SET flg = 1 WHERE  idmanutencao = {idManutencao}";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

    }
}
