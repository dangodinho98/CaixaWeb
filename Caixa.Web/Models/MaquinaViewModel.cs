using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caixa.Web.Models
{
    public class MaquinaViewModel
    {
        public Maquinas Maquina { get; set; }

        public IList<Estabelecimentos> Estabelecimentos { get; set; }
    }
}