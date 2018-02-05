using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caixa.Web.Models
{
    public class AcertoViewModel
    {
        public Acerto Acerto { get; set; }

        public IList<Maquinas> Maquinas { get; set; }

        public IList<Estabelecimentos> Estabelecimentos { get; set; }
    }
}