using System.Security.Cryptography;
using System.Text;

namespace Test.Helpers
{
    public static class HashPassword
    {

        public static string HashPas(string input)
        {
            using (var sha = SHA256.Create())
            {
                var hasByt = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                string hash = BitConverter.ToString(hasByt).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
