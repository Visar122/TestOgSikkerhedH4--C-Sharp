using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

public static class HashHelper
{
    // ✅ Hash using BCrypt (For Passwords)
    public static string HashBCrypt(string input) => BCrypt.Net.BCrypt.HashPassword(input);

    // ✅ Hash using SHA256
    public static string HashSHA256(string input)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }

    // ✅ Hash using SHA512 (Recommended for CPR)
    public static string HashSHA512(string input)
    {
        using var sha512 = SHA512.Create();
        byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }

    // ✅ Hash using MD5 (Not Secure, just for demonstration)
    public static string HashMD5(string input)
    {
        using var md5 = MD5.Create();
        byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }

    // ✅ CPR Hashing (Uses SHA512)
    public static string HashCPR(string cpr) => HashSHA512(cpr);
}
