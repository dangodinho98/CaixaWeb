using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Caixa.Web.Models
{
    [Table("Comissionado")]
    public class Comissionado
    {
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "O campo telefone não pode ultrapassar 15 caracteres.")]
        public string Telefone { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "O campo CPF não pode ultrapassar 11 caracteres. Não coloque pontos.")]
        public string CPF { get; set; }

        [Required]
        public float Comissao { get; set; }
        public float? GanhoAcumulado { get; set; }
        public bool Bloqueado { get; set; }

        [Display(Name = "Último usuário que modificou")]
        public string ModUser { get; set; }
    }
}