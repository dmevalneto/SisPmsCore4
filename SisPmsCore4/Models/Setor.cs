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
    public class Setor
    {
        public int idSetor { get; set; }
        [Required(ErrorMessage ="*")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "*")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "*")]
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        [Required(ErrorMessage = "*")]
        public string Gestor { get; set; }
        public int tipo_setor_idtipo_setor { get; set; }

        IHttpContextAccessor HttpContextAccessor;

        public Setor()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public Setor(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public List<Setor> ListaSetor()
        {
            List<Setor> lista = new List<Setor>();
            Setor item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT * FROM setor";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Setor();
                item.idSetor = int.Parse(dt.Rows[i]["idsetor"].ToString());
                item.Codigo = dt.Rows[i]["codigo"].ToString();
                item.Nome = dt.Rows[i]["nome"].ToString();
                item.Telefone1 = dt.Rows[i]["telefone1"].ToString();
                item.Telefone2 = dt.Rows[i]["telefone2"].ToString();
                item.Cep = dt.Rows[i]["cep"].ToString();
                item.Bairro = dt.Rows[i]["bairro"].ToString();
                item.Logradouro = dt.Rows[i]["logradouro"].ToString();
                item.Numero = dt.Rows[i]["numero"].ToString();
                item.Latitude = dt.Rows[i]["latitude"].ToString();
                item.Longitude = dt.Rows[i]["longitude"].ToString();
                item.Gestor = dt.Rows[i]["gestor"].ToString();
                item.tipo_setor_idtipo_setor = int.Parse(dt.Rows[i]["tipo_setor_idtipo_setor"].ToString());
                lista.Add(item);
            }
            return lista;

        }

        public void SalvarNovoRegistro()
        {
            string sql = $"INSERT INTO setor (codigo, nome, telefone1, telefone2, cep, bairro, logradouro, numero, latitude, longitude, gestor, tipo_setor_idtipo_setor) VALUES ('{Codigo}', '{Nome}', '{Telefone1}', '{Telefone2}', '{Cep}', '{Bairro}', '{Logradouro}', '{Numero}', '{Latitude}', '{Longitude}', '{Gestor}', {tipo_setor_idtipo_setor})";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
