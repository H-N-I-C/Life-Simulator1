using System;
using Newtonsoft.Json;

public class IntToStringConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return false;
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		return null;
	}
}
