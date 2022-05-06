using CrudUsuarios.Models;
using System;
using System.Linq;

namespace CrudUsuarios.Utils
{
    public class TokenUtil
    {
        public const int validadeToken = 5;
        public static string Token { get => GetToken(); }

        private static int DaysBetween(DateTime dt1, DateTime dt2)
        {
            TimeSpan days = dt2.Subtract(dt1);

            return (int)days.TotalDays;
        }

        private static string GetToken()
        {
            return GenerateToken(6);
        }

        public static string GenerateToken(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool IsValid(ResetPasswordToken token)
        {
            return IsDateValid(token);
        }

        private static bool IsDateValid(ResetPasswordToken token)
        {
            return DaysBetween(token.Cadastro, DateTime.Now) <= token.Validade;
        }
    }
}
