using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CrudUsuarios.Models
{
    public partial class User
    {
        public User()
        {
            ResetPasswordToken = new HashSet<ResetPasswordToken>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ResetPasswordToken> ResetPasswordToken { get; set; }
    }
}
