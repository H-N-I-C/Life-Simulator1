using System.Collections.Generic;
using Amazon.Runtime;

namespace Backend.Aws
{
	public class AwsSigV4Signer
	{
		private readonly string _region;

		private readonly string _service;

		private readonly ImmutableCredentials _credentials;

		public AwsSigV4Signer(ImmutableCredentials credentials, string region, string service)
		{
		}

		public Dictionary<string, string> SignRequest(string method, string url, string body = "", Dictionary<string, string> extraHeaders = null)
		{
			return null;
		}

		private static string Hash(string data)
		{
			return null;
		}

		private static byte[] HmacSHA256(string data, byte[] key)
		{
			return null;
		}

		private static byte[] GetSignatureKey(string key, string dateStamp, string regionName, string serviceName)
		{
			return null;
		}

		private static string ToHexString(byte[] bytes, bool lowercase)
		{
			return null;
		}
	}
}
