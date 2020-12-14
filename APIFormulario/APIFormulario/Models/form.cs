using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiFormulario.Models
{
    public class form
    {
        [Key]
        public Int32 ID { get; set; }

        public String NOME { get; set; }

        public String SOBRENOME { get; set; }

        public String TELEFONE { get; set; }

    }
}