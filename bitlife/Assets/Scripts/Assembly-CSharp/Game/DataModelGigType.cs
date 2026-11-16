using System.Collections.Generic;

namespace Game
{
	public class DataModelGigType
	{
		private MODEL_CAMPAIGN_TYPE JobType__BackingField;

		private List<MODEL_INDUSTRY> Industries__BackingField;

		private int CommonnessScore__BackingField;

		public MODEL_CAMPAIGN_TYPE JobType
		{
			get
			{
				return JobType__BackingField;
			}
			set
			{
				JobType__BackingField = value;
			}
		}

		public List<MODEL_INDUSTRY> Industries
		{
			get
			{
				return Industries__BackingField;
			}
			set
			{
				Industries__BackingField = value;
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
