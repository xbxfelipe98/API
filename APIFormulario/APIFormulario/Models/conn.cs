using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ApiFormulario.Models
{
    public class conn : System.Data.Entity.DbContext
    { 

        public conn() : base("name=conn")
        {

        }

        public DbSet<form> CADASTRO { get; set; }
    }
}