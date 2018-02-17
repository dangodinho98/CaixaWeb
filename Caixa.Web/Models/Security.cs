using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Caixa.Web.Models
{
    [Table("Security")]
    public class Security
    {
        public int Id { get; set; }
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Range(1, 5, ErrorMessage = "Defina um nível de segurança entre 1 e 5.")]
        public short? Level { get; set; }
    }
}