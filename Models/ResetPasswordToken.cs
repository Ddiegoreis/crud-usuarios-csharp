using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CrudUsuarios.Models
{
    public partial class ResetPasswordToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int IdUser { get; set; }
        public DateTime Cadastro { get; set; }
        public int? Validade { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
