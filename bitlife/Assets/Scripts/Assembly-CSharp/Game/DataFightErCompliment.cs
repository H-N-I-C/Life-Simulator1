namespace Game
{
	public class DataFightErCompliment
	{
		private string ComplimentOutbound__BackingField;

		private string ComplimentInbound__BackingField;

		private string OutboundLogString__BackingField;

		private string InboundLogString__BackingField;

		private int CommonnessScore__BackingField;

		public string ComplimentOutbound
		{
			get
			{
				return ComplimentOutbound__BackingField;
			}
			set
			{
				ComplimentOutbound__BackingField = value;
			}
		}

		public string ComplimentInbound
		{
			get
			{
				return ComplimentInbound__BackingField;
			}
			set
			{
				ComplimentInbound__BackingField = value;
			}
		}

		public string OutboundLogString
		{
			get
			{
				return OutboundLogString__BackingField;
			}
			set
			{
				OutboundLogString__BackingField = value;
			}
		}

		public string InboundLogString
		{
			get
			{
				return InboundLogString__BackingField;
			}
			set
			{
				InboundLogString__BackingField = value;
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
