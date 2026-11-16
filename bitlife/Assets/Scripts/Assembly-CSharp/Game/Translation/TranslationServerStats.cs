namespace Game.Translation
{
	public class TranslationServerStats
	{
		private int dynamoDb__BackingField;

		private int llm__BackingField;

		public int dynamoDb
		{
			get
			{
				return dynamoDb__BackingField;
			}
			set
			{
				dynamoDb__BackingField = value;
			}
		}

		public int llm
		{
			get
			{
				return llm__BackingField;
			}
			set
			{
				llm__BackingField = value;
			}
		}
	}
}
