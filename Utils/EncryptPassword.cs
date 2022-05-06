using System;
using System.Security.Cryptography;
using System.Text;

namespace CrudUsuarios.Utils
{
    public class EncryptPassword
    {
        private static string hashGenerate(byte[] senha)
        {
            SHA512 hasher = SHA512.Create();
            
            byte[] hashBytes = hasher.ComputeHash(senha);
            
            string hash = Convert.ToBase64String(hashBytes);
            
            hasher.Clear();

            return hash;
        }

        public static string execute(string password)
        {
            byte[] bytesSenha = Encoding.UTF8.GetBytes(password);
            
            return hashGenerate(bytesSenha);
        }
    }
}
