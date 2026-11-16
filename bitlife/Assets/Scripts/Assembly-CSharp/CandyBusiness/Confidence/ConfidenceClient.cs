using Game.Util.Debugging;
using UnityEngine;

namespace CandyBusiness.Confidence
{
	public sealed class ConfidenceClient : ScriptableObject
	{
		public string clientName;

		private int[] secretObfuscated;

		public bool localEncryption;

		public LoggerConfig loggerConfig;

		private static ConfidenceClient s_active;

		public static ConfidenceClient Active => null;

		public bool IsValid()
		{
			return false;
		}

		public string GetSecret()
		{
			return null;
		}

		public string GetPath()
		{
			return null;
		}

		public static string MakePath(string name, bool encrypted)
		{
			return null;
		}

		private void OnEnable()
		{
		}
	}
}
