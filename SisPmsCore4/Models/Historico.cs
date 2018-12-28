using Microsoft.AspNetCore.Http;
using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class Historico
    {
        public int idHistorico { get; set; }
        public string Data { get; set; }
        public string Observacao { get; set; }
        public int colaborador_idcolaborador { get; set; }
        public int setor_idsetor { get; set; }
        public int usuario_idusuario { get; set; }

        IHttpContextAccessor HttpContextAccessor;

        public Historico()
        {

        }

        //Recebe o contexto para acesso as variaveis de sessão
        public Historico(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }


        public void Encaminhar()
        {
            string dataConvert = DateTime.Parse(Data).ToString("yyyy/MM/dd");
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"INSERT INTO historico (data, observacao, colaborador_idcolaborador, setor_idsetor, usuario_idusuario) VALUES ('{dataConvert}', '{Observacao}', {colaborador_idcolaborador}, {setor_idsetor}, {id_usuario_logado})";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
