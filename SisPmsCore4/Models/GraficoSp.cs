using SisPmsCore4.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisPmsCore4.Models
{
    public class GraficoSp
    {
        public double Total { get; set; }
        public string Descricao { get; set; }

        public List<GraficoSp> RetornarDadosGragicoPie()
        {
            List<GraficoSp> lista = new List<GraficoSp>();
            GraficoSp item;

            string sql = "select  cargo.nome, count(cargo.nome) as Total from colaborador inner join cargo on colaborador.cargo_idcargo=cargo.idcargo group by cargo.nome";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new GraficoSp();
                item.Total = double.Parse(dt.Rows[i]["Total"].ToString());
                item.Descricao = dt.Rows[i]["nome"].ToString();
                lista.Add(item);
            }
            return lista;
        }
    }
}
