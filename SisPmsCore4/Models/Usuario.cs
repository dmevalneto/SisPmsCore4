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
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Data { get; set; }
        public int setorid { get; set; }

        IHttpContextAccessor HttpContextAccessor;

        public Usuario()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public Usuario(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }


        public bool Validarlogin()
        {
            string sql = $"select idusuario, nome, email, senha, setor_idsetor FROM usuario WHERE Email = '{Email}' AND Senha = '{Senha}'";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    IdUsuario = int.Parse(dt.Rows[0]["idusuario"].ToString());
                    Nome = (dt.Rows[0]["nome"].ToString());
                    setorid =int.Parse((dt.Rows[0]["setor_idsetor"].ToString()));
                    // Data = (dt.Rows[0]["data"].ToString());
                    return true;
                }
            }
            return false;
        }

        public void RegistrarUsuario()
        {
            string dataNascimento = DateTime.Parse(Data).ToString("yyyy/MM/dd");
            string sql = $"INSERT INTO usuario (nome, email, senha, data, setor_idsetor) VALUES('{Nome}','{Email}','{Senha}','{dataNascimento}','{setorid}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
