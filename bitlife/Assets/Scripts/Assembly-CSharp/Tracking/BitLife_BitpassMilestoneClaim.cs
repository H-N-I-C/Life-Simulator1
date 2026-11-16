using System.Collections.Generic;

namespace Tracking
{
	public class BitLife_BitpassMilestoneClaim : GameTelemetryEvent
	{
		private string TrackSeason__BackingField;

		private string TrackType__BackingField;

		private int TrackMilestone__BackingField;

		private int BitsRequiredCumulative__BackingField;

		private int BitsRequiredMarginal__BackingField;

		public const string Free = "Free";

		public const string Premium = "Premium";

		public string TrackSeason
		{
			get
			{
				return TrackSeason__BackingField;
			}
			set
			{
				TrackSeason__BackingField = value;
			}
		}

		public string TrackType
		{
			get
			{
				return TrackType__BackingField;
			}
			set
			{
				TrackType__BackingField = value;
			}
		}

		public int TrackMilestone
		{
			get
			{
				return TrackMilestone__BackingField;
			}
			set
			{
				TrackMilestone__BackingField = value;
			}
		}

		public int BitsRequiredCumulative
		{
			get
			{
				return BitsRequiredCumulative__BackingField;
			}
			set
			{
				BitsRequiredCumulative__BackingField = value;
			}
		}

		public int BitsRequiredMarginal
		{
			get
			{
				return BitsRequiredMarginal__BackingField;
			}
			set
			{
				BitsRequiredMarginal__BackingField = value;
			}
		}

		public override Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
