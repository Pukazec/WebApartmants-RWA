using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Model
{
    public class Cryptography
    {
        public static string HashPassword(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using (var hash = SHA512.Create())
            {
                var hashInputBytes = hash.ComputeHash(bytes);
                var hashInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashInputBytes)
                    hashInputStringBuilder.Append(b.ToString("X2"));
                return hashInputStringBuilder.ToString();
            }
        }
    }
}
