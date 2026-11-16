namespace Game
{
	public class DataCasinoGamblerConversation
	{
		private CASINO_AUDIENCE_TYPE Audience__BackingField;

		private string Conversation__BackingField;

		private int HeroMinAge__BackingField;

		private int HeroMaxAge__BackingField;

		private int AudienceMinAge__BackingField;

		private int AudienceMaxAge__BackingField;

		public CASINO_AUDIENCE_TYPE Audience
		{
			get
			{
				return Audience__BackingField;
			}
			set
			{
				Audience__BackingField = value;
			}
		}

		public string Conversation
		{
			get
			{
				return Conversation__BackingField;
			}
			set
			{
				Conversation__BackingField = value;
			}
		}

		public int HeroMinAge
		{
			get
			{
				return HeroMinAge__BackingField;
			}
			set
			{
				HeroMinAge__BackingField = value;
			}
		}

		public int HeroMaxAge
		{
			get
			{
				return HeroMaxAge__BackingField;
			}
			set
			{
				HeroMaxAge__BackingField = value;
			}
		}

		public int AudienceMinAge
		{
			get
			{
				return AudienceMinAge__BackingField;
			}
			set
			{
				AudienceMinAge__BackingField = value;
			}
		}

		public int AudienceMaxAge
		{
			get
			{
				return AudienceMaxAge__BackingField;
			}
			set
			{
				AudienceMaxAge__BackingField = value;
			}
		}
	}
}
