using System.Security.Cryptography;
using System.Text;

namespace Fashion_Website.Helpers
{
    public static class Hash
    {
        public static string ComputeSha256(string text)
        {
            SHA256 sha256Hash = SHA256.Create();

            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

            //Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for(int i =  0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}