using System.Security.Cryptography;
using System.Text;

namespace DespesasCasa.Application.Security;

public class EncryptUtils
{
    public static string EncryptPassword(string password)
    {
        var message = Encoding.UTF8.GetBytes(password);
        using (var alg = SHA512.Create())
        {
            string hex = "";

            var hashValue = alg.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += string.Format("{0:x2}", x);
            }
            return hex;
        }
    }
}
