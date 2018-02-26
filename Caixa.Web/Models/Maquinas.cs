using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Caixa.Web.Models
{
    public class Maquinas
    {
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public int Codigo { get; set; }
        public string Tema { get; set; }

        [Index(IsUnique = true)]
        public int Lacre { get; set; }

        [Display(Name = "DI (Entrada)")]
        public float DI { get; set; }

        [Display(Name = "DS (Saída)")]
        public float DS { get; set; }

        [Display(Name = "Estabelecimento")]
        [ForeignKey("Estabelecimento")]
        public int IdEstabelecimento { get; set; }
        public Estabelecimentos Estabelecimento { get; set; }
        public bool Ativo { get; set; }

        [Display(Name = "Último usuário que modificou")]
        public string ModUser { get; set; }

    }
}