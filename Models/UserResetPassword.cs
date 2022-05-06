namespace CrudUsuarios.Models
{
    public class UserResetPassword : User
    {
        public string SenhaNova { get; set; }
        public string Token { get; set; }
    }
}
