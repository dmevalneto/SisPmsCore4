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
    public class Colaborador
    {
        [Display(Name = "Id")]
        public int idColaborador { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DataType(DataType.Date)]
        public string DataAdmissao { get; set; }
        // [RegularExpression(@"/^(\(11\) (9\d{4})-\d{4})|((\(1[2-9]{1}\)|\([2-9]{1}\d{1}\)) [5-9]\d{3}-\d{4})$/", ErrorMessage = "(11) 92222-2222")]
        public string Telefone { get; set; }
        public int setor_idsetor { get; set; }
        public int cargo_idcargo { get; set; }
        public int ocorrencia_idocorrencia { get; set; }
        public int prestadora_servico_idprestadora_servico { get; set; }

        [Display(Name = "Setor")]
        public string NomeSe { get; set; }
        [Display(Name = "Codigo")]
        public int CodigoSe { get; set; }
        public string GestorSe { get; set; }
        public string NomeCargo { get; set; }
        public string NumeroOco { get; set; }
        public string DescOco { get; set; }
        IHttpContextAccessor HttpContextAccessor;

        public Colaborador()
        {

        }



        //Recebe o contexto para acesso as variaveis de sessão
        public Colaborador(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public List<Colaborador> FiltrarColaborador(string nome)
        {

            List<Colaborador> lista = new List<Colaborador>();
            Colaborador item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            //string sql = $"SELECT idcolaborador, nome, cpf, data_admissao, telefone, setor_idsetor, cargo_idcargo, ocorrencia_idocorrencia, prestadora_servico_idprestadora_servico FROM colaborador WHERE  nome LIKE '%{nome}%'";
            string sql = " SELECT " +
                " colaborador.idcolaborador, colaborador.nome , colaborador.cpf, colaborador.data_admissao, colaborador.telefone, colaborador.setor_idsetor, colaborador.cargo_idcargo, colaborador.ocorrencia_idocorrencia, colaborador.prestadora_servico_idprestadora_servico , " +
                " setor.nome as NomeSe, setor.codigo as CodigoSe " +
                " FROM colaborador " +
                " inner join setor on colaborador.setor_idsetor = setor.idsetor " +
                $" WHERE  colaborador.nome LIKE '%{nome}%' ";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Colaborador();
                item.idColaborador = int.Parse(dt.Rows[i]["idcolaborador"].ToString());
                item.Nome = dt.Rows[i]["nome"].ToString();
                item.Cpf = dt.Rows[i]["cpf"].ToString();
                item.DataAdmissao = DateTime.Parse(dt.Rows[i]["data_admissao"].ToString()).ToString("dd/MM/yyy");
                item.Telefone = dt.Rows[i]["telefone"].ToString();
                item.setor_idsetor = int.Parse(dt.Rows[i]["setor_idsetor"].ToString());
                item.cargo_idcargo = int.Parse(dt.Rows[i]["cargo_idcargo"].ToString());
                item.ocorrencia_idocorrencia = int.Parse(dt.Rows[i]["ocorrencia_idocorrencia"].ToString());
                item.prestadora_servico_idprestadora_servico = int.Parse(dt.Rows[i]["prestadora_servico_idprestadora_servico"].ToString());
                item.NomeSe = dt.Rows[i]["NomeSe"].ToString();
                item.CodigoSe = int.Parse(dt.Rows[i]["CodigoSe"].ToString());
                lista.Add(item);
            }
            return lista;

        }

        public List<Colaborador> FiltrarColaboradorPorSetor(int id)
        {

            List<Colaborador> lista = new List<Colaborador>();
            Colaborador item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = " select " +
                " colaborador.idcolaborador, colaborador.nome, colaborador.cpf, colaborador.data_admissao, colaborador.telefone, " +
                " setor.nome as NomeSe, setor.gestor as GestorSe " +
                " from colaborador " +
                " inner join setor on colaborador.setor_idsetor = setor.idsetor " +
                $" where setor_idsetor = {id} ";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Colaborador();
                item.idColaborador = int.Parse(dt.Rows[i]["idcolaborador"].ToString());
                item.Nome = dt.Rows[i]["nome"].ToString();
                item.Cpf = dt.Rows[i]["cpf"].ToString();
                item.DataAdmissao = DateTime.Parse(dt.Rows[i]["data_admissao"].ToString()).ToString("dd/MM/yyy");
                item.Telefone = dt.Rows[i]["telefone"].ToString();
                item.NomeSe = dt.Rows[i]["NomeSe"].ToString();
                item.GestorSe = dt.Rows[i]["GestorSe"].ToString();

                lista.Add(item);
            }

            return lista;

        }

        public List<Colaborador> FiltrarColaboradorPorCargo(int id)
        {

            List<Colaborador> lista = new List<Colaborador>();
            Colaborador item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = " SELECT" +
                " colaborador.nome, cargo.nome as NomeCargo" +
                " from colaborador" +
                " inner join cargo on colaborador.cargo_idcargo = cargo.idcargo" +
                $" where cargo.idcargo = {id} ORDER BY colaborador.nome";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Colaborador();
                item.Nome = dt.Rows[i]["nome"].ToString();
                item.NomeCargo = dt.Rows[i]["NomeCargo"].ToString();

                lista.Add(item);
            }

            return lista;

        }


        public List<Colaborador> FiltrarColaboradorPorOcorrencia(int id)
        {

            List<Colaborador> lista = new List<Colaborador>();
            Colaborador item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = " SELECT" +
                " colaborador.nome, ocorrencia.numero as NumeroOco, ocorrencia.descricao as DescOco from colaborador " +
                " inner join ocorrencia on colaborador.ocorrencia_idocorrencia = ocorrencia.idocorrencia " +
                $" where colaborador.ocorrencia_idocorrencia = {id} order by colaborador.nome ";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Colaborador();
                item.Nome = dt.Rows[i]["nome"].ToString();
                item.NumeroOco = dt.Rows[i]["NumeroOco"].ToString();
                item.DescOco = dt.Rows[i]["DescOco"].ToString();

                lista.Add(item);
            }

            return lista;

        }


        public Colaborador CarregarRegistro(int? id)
        {
            Colaborador item = new Colaborador();
            string sql = $"SELECT idcolaborador, nome, cpf, data_admissao, telefone, setor_idsetor, cargo_idcargo, ocorrencia_idocorrencia, prestadora_servico_idprestadora_servico FROM colaborador WHERE idcolaborador = {id}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            item.idColaborador = int.Parse(dt.Rows[0]["idcolaborador"].ToString());
            item.Nome = dt.Rows[0]["nome"].ToString();
            item.Cpf = dt.Rows[0]["cpf"].ToString();
            item.DataAdmissao = DateTime.Parse(dt.Rows[0]["data_admissao"].ToString()).ToString("dd/MM/yyy");
            item.Telefone = dt.Rows[0]["telefone"].ToString();
            item.setor_idsetor = int.Parse(dt.Rows[0]["setor_idsetor"].ToString());
            item.cargo_idcargo = int.Parse(dt.Rows[0]["cargo_idcargo"].ToString());
            item.ocorrencia_idocorrencia = int.Parse(dt.Rows[0]["ocorrencia_idocorrencia"].ToString());
            item.prestadora_servico_idprestadora_servico = int.Parse(dt.Rows[0]["prestadora_servico_idprestadora_servico"].ToString());

            return item;
        }


        public List<Colaborador> ListaColaborador()
        {
            List<Colaborador> lista = new List<Colaborador>();
            Colaborador item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT idcolaborador, nome, cpf, data_admissao, telefone, setor_idsetor, cargo_idcargo, ocorrencia_idocorrencia, prestadora_servico_idprestadora_servico FROM colaborador";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new Colaborador();
                item.idColaborador = int.Parse(dt.Rows[i]["idcolaborador"].ToString());
                item.Nome = dt.Rows[i]["nome"].ToString();
                item.Cpf = dt.Rows[i]["cpf"].ToString();
                item.DataAdmissao = DateTime.Parse(dt.Rows[i]["data_admissao"].ToString()).ToString("dd/MM/yyy");
                item.Telefone = dt.Rows[i]["telefone"].ToString();
                item.setor_idsetor = int.Parse(dt.Rows[i]["setor_idsetor"].ToString());
                item.cargo_idcargo = int.Parse(dt.Rows[i]["cargo_idcargo"].ToString());
                item.ocorrencia_idocorrencia = int.Parse(dt.Rows[i]["ocorrencia_idocorrencia"].ToString());
                item.prestadora_servico_idprestadora_servico = int.Parse(dt.Rows[i]["prestadora_servico_idprestadora_servico"].ToString());
                lista.Add(item);
            }
            return lista;

        }

        public void SalvarNovoRegistro()
        {
            string dataAdmissao = DateTime.Parse(DataAdmissao).ToString("yyyy/MM/dd");
            string sql = $"INSERT INTO colaborador (nome, cpf, data_admissao, telefone, setor_idsetor, cargo_idcargo, ocorrencia_idocorrencia, prestadora_servico_idprestadora_servico) VALUES ('{Nome}', '{Cpf}','{dataAdmissao}', '{Telefone}', {setor_idsetor}, {cargo_idcargo}, {ocorrencia_idocorrencia}, {prestadora_servico_idprestadora_servico})";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

        public void AtualizarRegistro()
        {
            string dataAdmissao = DateTime.Parse(DataAdmissao).ToString("yyyy/MM/dd");
            string sql = $"UPDATE colaborador SET nome = '{Nome}', cpf = '{Cpf}', data_admissao = '{dataAdmissao}', telefone = '{Telefone}', setor_idsetor = '{setor_idsetor}', cargo_idcargo = '{cargo_idcargo}', ocorrencia_idocorrencia = '{ocorrencia_idocorrencia}', prestadora_servico_idprestadora_servico = '{prestadora_servico_idprestadora_servico}' WHERE  idcolaborador = {idColaborador}";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

        public void ExcluirRegistro(int? id)
        {
            string sql = $"DELETE  FROM colaborador WHERE idcolaborador = {id}";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

       
    }
}
