namespace Game.Seasons.Models
{
	public class SeasonModelError
	{
		private string Error__BackingField;

		private string Message__BackingField;

		public string Error
		{
			get
			{
				return Error__BackingField;
			}
			set
			{
				Error__BackingField = value;
			}
		}

		public string Message
		{
			get
			{
				return Message__BackingField;
			}
			set
			{
				Message__BackingField = value;
			}
		}

		public string GetMessage()
		{
			return null;
		}

		public string GetPrettyMessage()
		{
			return null;
		}
	}
}
