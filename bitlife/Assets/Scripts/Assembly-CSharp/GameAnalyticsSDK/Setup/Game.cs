namespace GameAnalyticsSDK.Setup
{
	public class Game
	{
		private string Name__BackingField;

		private int ID__BackingField;

		private string GameKey__BackingField;

		private string SecretKey__BackingField;

		public string Name
		{
			get
			{
				return Name__BackingField;
			}
			private set
			{
				Name__BackingField = value;
			}
		}

		public int ID
		{
			get
			{
				return ID__BackingField;
			}
			private set
			{
				ID__BackingField = value;
			}
		}

		public string GameKey
		{
			get
			{
				return GameKey__BackingField;
			}
			private set
			{
				GameKey__BackingField = value;
			}
		}

		public string SecretKey
		{
			get
			{
				return SecretKey__BackingField;
			}
			private set
			{
				SecretKey__BackingField = value;
			}
		}

		public Game(string name, int id, string gameKey, string secretKey)
		{
		}
	}
}
