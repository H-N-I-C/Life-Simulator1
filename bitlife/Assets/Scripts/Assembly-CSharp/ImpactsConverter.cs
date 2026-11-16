using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ImpactsConverter : JsonConverter<string>
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<JToken, string> _003C_003E9__0_0;

		public static Func<string, bool> _003C_003E9__0_1;

		public static Func<JToken, string> _003C_003E9__2_0;

		public static Func<string, bool> _003C_003E9__2_1;

		internal string _003CReadJson_003Eb__0_0(JToken t)
		{
			return null;
		}

		internal bool _003CReadJson_003Eb__0_1(string s)
		{
			return false;
		}

		internal string _003CConvertRawImpacts_003Eb__2_0(JToken t)
		{
			return null;
		}

		internal bool _003CConvertRawImpacts_003Eb__2_1(string s)
		{
			return false;
		}
	}

	public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		return null;
	}

	public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
	{
	}

	public static string ConvertRawImpacts(string raw)
	{
		return null;
	}

	private static string ConvertImpactToken(JToken t)
	{
		return null;
	}
}
