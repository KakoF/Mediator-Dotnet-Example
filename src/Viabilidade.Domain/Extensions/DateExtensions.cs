using System.Security.Cryptography;
using System.Text;

namespace Viabilidade.Domain.Extensions
{
    public static class DateExtensions
    {
        public static string DateToCode(this DateTime input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(input.ToString()))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}
