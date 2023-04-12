using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteCamposDealer.Response
{
    public class VendasClientes
    {
        public int idVenda { get; set; }
        public int idCliente { get; set; }
        public int idProduto { get; set; }
        public int vlrTotal { get; set; }
        public string nomeCliente { get; set; }
    }
}