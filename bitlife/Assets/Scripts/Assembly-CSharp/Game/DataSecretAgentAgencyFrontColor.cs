using System.Collections.Generic;

namespace Game
{
	public class DataSecretAgentAgencyFrontColor
	{
		private string Snippet__BackingField;

		private List<SECRET_AGENT_AGENCY_FRONT_TYPE> ApplicableFronts__BackingField;

		private int CommonnessScore__BackingField;

		public string Snippet
		{
			get
			{
				return Snippet__BackingField;
			}
			set
			{
				Snippet__BackingField = value;
			}
		}

		public List<SECRET_AGENT_AGENCY_FRONT_TYPE> ApplicableFronts
		{
			get
			{
				return ApplicableFronts__BackingField;
			}
			set
			{
				ApplicableFronts__BackingField = value;
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
