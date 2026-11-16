namespace Game
{
	public class DataCasinoRandomGameRoomName
	{
		private string Name__BackingField;

		private CASINO_GAME_TYPE GameRoomType__BackingField;

		private int CommonnessScore__BackingField;

		public string Name
		{
			get
			{
				return Name__BackingField;
			}
			set
			{
				Name__BackingField = value;
			}
		}

		public CASINO_GAME_TYPE GameRoomType
		{
			get
			{
				return GameRoomType__BackingField;
			}
			set
			{
				GameRoomType__BackingField = value;
			}
		}

		public int CommonnessScore
		{
			get
			{
				return CommonnessScore__BackingField;
			}
			set
			{
				CommonnessScore__BackingField = value;
			}
		}
	}
}
