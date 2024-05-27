using System.Security.Cryptography;
using System.Text;

namespace Domain.Extensions;

public static class PasswordHasher
{
	public static string ComputeStringToSha256Hash(string plainText)
	{
		using SHA256 sha256 = SHA256.Create();
		byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
		StringBuilder stringBuilder = new();
		for (int i = 0; i < bytes.Length; i++)
		{
			stringBuilder.Append(bytes[i].ToString("x2"));
		}
		return stringBuilder.ToString();

	}

}
