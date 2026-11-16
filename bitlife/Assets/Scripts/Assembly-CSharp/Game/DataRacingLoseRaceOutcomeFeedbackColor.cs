using System.Collections.Generic;

namespace Game
{
	public class DataRacingLoseRaceOutcomeFeedbackColor
	{
		private string String__BackingField;

		private List<RACING_TRACK_CATEGORY> RaceType__BackingField;

		private List<RACING_LICENSE> RaceLevel__BackingField;

		private bool OtherRacerInvolved__BackingField;

		private bool RivalRacerInvolved__BackingField;

		private int MinRacerCraziness__BackingField;

		private int MaxRacerCraziness__BackingField;

		private int CommonnessScore__BackingField;

		public string String
		{
			get
			{
				return String__BackingField;
			}
			set
			{
				String__BackingField = value;
			}
		}

		public List<RACING_TRACK_CATEGORY> RaceType
		{
			get
			{
				return RaceType__BackingField;
			}
			set
			{
				RaceType__BackingField = value;
			}
		}

		public List<RACING_LICENSE> RaceLevel
		{
			get
			{
				return RaceLevel__BackingField;
			}
			set
			{
				RaceLevel__BackingField = value;
			}
		}

		public bool OtherRacerInvolved
		{
			get
			{
				return OtherRacerInvolved__BackingField;
			}
			set
			{
				OtherRacerInvolved__BackingField = value;
			}
		}

		public bool RivalRacerInvolved
		{
			get
			{
				return RivalRacerInvolved__BackingField;
			}
			set
			{
				RivalRacerInvolved__BackingField = value;
			}
		}

		public int MinRacerCraziness
		{
			get
			{
				return MinRacerCraziness__BackingField;
			}
			set
			{
				MinRacerCraziness__BackingField = value;
			}
		}

		public int MaxRacerCraziness
		{
			get
			{
				return MaxRacerCraziness__BackingField;
			}
			set
			{
				MaxRacerCraziness__BackingField = value;
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
