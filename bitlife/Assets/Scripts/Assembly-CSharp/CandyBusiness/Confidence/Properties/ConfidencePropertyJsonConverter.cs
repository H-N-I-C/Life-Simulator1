using System;
using Newtonsoft.Json;

namespace CandyBusiness.Confidence.Properties
{
	public class ConfidencePropertyJsonConverter : JsonConverter
	{
		public override bool CanRead => false;

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
}
