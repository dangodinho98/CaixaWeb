using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Caixa.Web.Models
{
    public class Estabelecimentos
    {
        public int Id { get; set; }

        [MaxLength(255)]
        [Index(IsUnique = true)]
        [Required]
        [Display(Name = "Nome do Estabelecimento")]
        public string Nome { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "O campo telefone não pode ultrapassar 15 caracteres.")]
        public string Telefone { get; set; }

        [Display(Name = "Região")]
        public string Regiao { get; set; }
        public bool Ativo { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        public ICollection<Maquinas> Maquinas { get; set; }

        [Display(Name = "Último usuário que modificou")]
        public string ModUser { get; set; }
    }
}