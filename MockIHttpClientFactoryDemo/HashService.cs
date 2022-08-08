using System.Security.Cryptography;
using System.Text;

namespace MockIHttpClientFactoryDemo
{
    public class HashService : IHashService
    {
        public string HashText(string text)
        {
            using SHA1Managed sha1 = new SHA1Managed();

            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
