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
    public class PrestadoraServico
    {
        public int idPrestadoraServico { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }
        public string Descricao { get; set; }

        IHttpContextAccessor HttpContextAccessor;

        public PrestadoraServico()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public PrestadoraServico(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public List<PrestadoraServico> ListaPrestadoraServico()
        {
            List<PrestadoraServico> lista = new List<PrestadoraServico>();
            PrestadoraServico item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT * FROM prestadora_servico";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new PrestadoraServico();
                item.idPrestadoraServico = int.Parse(dt.Rows[i]["idprestadora_servico"].ToString());
                item.RazaoSocial = dt.Rows[i]["razao_social"].ToString();
                item.Cnpj = dt.Rows[i]["cnpj"].ToString();
                item.Telefone1 = dt.Rows[i]["telefone1"].ToString();
                item.Telefone2 = dt.Rows[i]["telefone2"].ToString();
                item.Cep = dt.Rows[i]["cep"].ToString();
                item.Endereco = dt.Rows[i]["endereco"].ToString();
                item.Bairro = dt.Rows[i]["bairro"].ToString();
                item.Cidade = dt.Rows[i]["cidade"].ToString();
                item.Pais = dt.Rows[i]["pais"].ToString();
                item.Descricao = dt.Rows[i]["descricao"].ToString();
                lista.Add(item);
            }
            return lista;

        }

        public void SalvarNovoRegistro()
        {
            string sql = $"INSERT INTO prestadora_servico (razao_social, cnpj, telefone1, telefone2, cep, endereco, bairro, cidade, pais, descricao) VALUES ('{RazaoSocial}', '{Cnpj}', '{Telefone1}', '{Telefone2}', '{Cep}', '{Endereco}', '{Bairro}', '{Cidade}', '{Pais}', '{Descricao}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
