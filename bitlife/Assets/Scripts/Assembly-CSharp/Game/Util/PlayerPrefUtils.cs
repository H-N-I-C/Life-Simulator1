using System;

namespace Game.Util
{
	public static class PlayerPrefUtils
	{
		public static void SaveBoolOverride(string key, bool? value)
		{
		}

		public static bool TryGetBoolOverride(string key, out bool value)
		{
			value = default;
			return false;
		}

		public static bool TryGetPlayerPrefsProviderOverride<T>(string playerPrefsKey, out T provider) where T : struct, Enum
		{
			provider = default;
			return false;
		}

		public static bool TryGetPlayerPrefsProviderOverride(string playerPrefsKey, out string provider)
		{
			provider = null;
			return false;
		}

		public static void SaveProviderToPlayerPrefs(string key, string provider)
		{
		}
	}
}
