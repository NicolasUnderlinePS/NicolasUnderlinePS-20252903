using EmployeeUnitManagementDomain.Models.Constants;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeUnitManagementDomain.Models.Utils
{
    public static class PasswordHash
    {
        public static string TransformIntoSha256(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(String.Format("{0}{1}", password, ConstantSystem.PasswordSecret));
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
