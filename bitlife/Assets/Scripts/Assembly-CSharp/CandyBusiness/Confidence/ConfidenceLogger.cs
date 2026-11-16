using Game.Util.Debugging;

namespace CandyBusiness.Confidence
{
	public static class ConfidenceLogger
	{
		private const string Tag = "Confidence";

		private static Logger Logger__BackingField;

		public static Logger Logger
		{
			get
			{
				return Logger__BackingField;
			}
			private set
			{
				Logger__BackingField = value;
			}
		}

		public static void Configure(LoggerConfig config)
		{
		}
	}
}
