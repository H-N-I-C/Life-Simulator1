using System.Security.Cryptography;
using System.Text;

namespace AvoEx
{
	public static class BackwardsCompatibilityEncryptor
	{
		private const int keySize = 32;

		private const string keyString = "ThisthekeyforGroomLakeDevelopmentApps";

		private const int IvLength = 16;

		private static readonly UTF8Encoding encoder;

		private static readonly AesManaged aes;

		static BackwardsCompatibilityEncryptor()
		{
		}

		public static byte[] GenerateIV()
		{
			return null;
		}

		public static byte[] Encrypt(byte[] buffer)
		{
			return null;
		}

		public static byte[] Decrypt(byte[] buffer)
		{
			return null;
		}

		public static byte[] EncryptIV(byte[] buffer, byte[] IV)
		{
			return null;
		}

		public static byte[] DecryptIV(byte[] buffer, byte[] IV)
		{
			return null;
		}

		public static byte[] EncryptKeyIV(byte[] buffer, byte[] key, byte[] IV)
		{
			return null;
		}

		public static byte[] DecryptKeyIV(byte[] buffer, byte[] key, byte[] IV)
		{
			return null;
		}

		public static string Encrypt(string unencrypted)
		{
			return null;
		}

		public static string Decrypt(string encrypted)
		{
			return null;
		}

		public static string DecryptString(string encrypted)
		{
			return null;
		}

		public static string DecryptString(byte[] encrypted)
		{
			return null;
		}

		public static string EncryptIV(string unencrypted, byte[] vector)
		{
			return null;
		}

		public static string DecryptIV(string encrypted, byte[] vector)
		{
			return null;
		}

		public static string Encrypt(bool unencrypted)
		{
			return null;
		}

		public static bool DecryptBool(string encrypted)
		{
			return false;
		}

		public static bool DecryptBool(byte[] encrypted)
		{
			return false;
		}

		public static string Encrypt(char unencrypted)
		{
			return null;
		}

		public static char DecryptChar(string encrypted)
		{
			return '\0';
		}

		public static char DecryptChar(byte[] encrypted)
		{
			return '\0';
		}

		public static string Encrypt(double unencrypted)
		{
			return null;
		}

		public static double DecryptDouble(string encrypted)
		{
			return 0.0;
		}

		public static double DecryptDouble(byte[] encrypted)
		{
			return 0.0;
		}

		public static string Encrypt(float unencrypted)
		{
			return null;
		}

		public static float DecryptFloat(string encrypted)
		{
			return 0f;
		}

		public static float DecryptFloat(byte[] encrypted)
		{
			return 0f;
		}

		public static string Encrypt(int unencrypted)
		{
			return null;
		}

		public static int DecryptInt(string encrypted)
		{
			return 0;
		}

		public static int DecryptInt(byte[] encrypted)
		{
			return 0;
		}

		public static string Encrypt(long unencrypted)
		{
			return null;
		}

		public static long DecryptLong(string encrypted)
		{
			return 0L;
		}

		public static long DecryptLong(byte[] encrypted)
		{
			return 0L;
		}

		public static string Encrypt(short unencrypted)
		{
			return null;
		}

		public static short DecryptShort(string encrypted)
		{
			return 0;
		}

		public static short DecryptShort(byte[] encrypted)
		{
			return 0;
		}

		public static string Encrypt(uint unencrypted)
		{
			return null;
		}

		public static uint DecryptUInt(string encrypted)
		{
			return 0u;
		}

		public static uint DecryptUInt(byte[] encrypted)
		{
			return 0u;
		}

		public static string Encrypt(ulong unencrypted)
		{
			return null;
		}

		public static ulong DecryptULong(string encrypted)
		{
			return 0uL;
		}

		public static ulong DecryptULong(byte[] encrypted)
		{
			return 0uL;
		}

		public static string Encrypt(ushort unencrypted)
		{
			return null;
		}

		public static ushort DecryptUShort(string encrypted)
		{
			return 0;
		}

		public static ushort DecryptUShort(byte[] encrypted)
		{
			return 0;
		}
	}
}
