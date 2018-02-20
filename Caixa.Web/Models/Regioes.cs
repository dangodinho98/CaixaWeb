using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Caixa.Web.Models
{
    [Table("Regioes")]
    public class Regioes
    {
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        [Display(Name = "Região")]
        public string Regiao { get; set; }

        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        public ICollection<Estabelecimentos> Estabelecimentos { get; set; }
    }
}