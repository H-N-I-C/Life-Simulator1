namespace Game.Seasons.Models
{
	public class DailyApiResult
	{
		private bool Success__BackingField;

		private long StatusCode__BackingField;

		private string Body__BackingField;

		private string ErrorCode__BackingField;

		public bool Success
		{
			get
			{
				return Success__BackingField;
			}
			private set
			{
				Success__BackingField = value;
			}
		}

		public long StatusCode
		{
			get
			{
				return StatusCode__BackingField;
			}
			private set
			{
				StatusCode__BackingField = value;
			}
		}

		public string Body
		{
			get
			{
				return Body__BackingField;
			}
			private set
			{
				Body__BackingField = value;
			}
		}

		public string ErrorCode
		{
			get
			{
				return ErrorCode__BackingField;
			}
			private set
			{
				ErrorCode__BackingField = value;
			}
		}

		public static DailyApiResult Create(bool success, long statusCode, string body, string fallbackErrorCode = null)
		{
			return null;
		}
	}
}
