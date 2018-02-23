using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caixa.Web.Models
{
    public class RegioesViewModel
    {
        public Regioes Regiao { get; set; }
        public IList<Regioes> Regioes { get; set; }

        public IList<Estabelecimentos> Estabelecimentos { get; set; }
        public Estabelecimentos Estabelecimento { get; set; }
    }
}