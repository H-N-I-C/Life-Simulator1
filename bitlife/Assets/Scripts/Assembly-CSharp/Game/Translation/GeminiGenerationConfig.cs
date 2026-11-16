namespace Game.Translation
{
	public class GeminiGenerationConfig
	{
		public float temperature;

		public int maxOutputTokens;

		private string responseMimeType__BackingField;

		private string[] stopSequences__BackingField;

		public string responseMimeType
		{
			get
			{
				return responseMimeType__BackingField;
			}
			set
			{
				responseMimeType__BackingField = value;
			}
		}

		public string[] stopSequences
		{
			get
			{
				return stopSequences__BackingField;
			}
			set
			{
				stopSequences__BackingField = value;
			}
		}
	}
}
