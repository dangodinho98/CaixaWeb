using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caixa.Web.Models
{
    public class EstabelecimentosViewModel
    {
        public IList<Regioes> Regioes { get; set; }

        public Estabelecimentos Estabelecimento { get; set; }
    }
}