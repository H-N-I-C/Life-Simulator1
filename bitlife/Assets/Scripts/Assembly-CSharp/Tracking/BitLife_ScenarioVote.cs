using System.Collections.Generic;

namespace Tracking
{
	public class BitLife_ScenarioVote : GameTelemetryEvent
	{
		private string ScenarioID__BackingField;

		private string ScenarioCategory__BackingField;

		private string ScenarioExternalTitle__BackingField;

		private string ScenarioTitle__BackingField;

		private bool VoteResult__BackingField;

		private const string ThumbsUp = "thumbs_up";

		private const string ThumbsDown = "thumbs_down";

		public string ScenarioID
		{
			get
			{
				return ScenarioID__BackingField;
			}
			private set
			{
				ScenarioID__BackingField = value;
			}
		}

		public string ScenarioCategory
		{
			get
			{
				return ScenarioCategory__BackingField;
			}
			private set
			{
				ScenarioCategory__BackingField = value;
			}
		}

		public string ScenarioExternalTitle
		{
			get
			{
				return ScenarioExternalTitle__BackingField;
			}
			private set
			{
				ScenarioExternalTitle__BackingField = value;
			}
		}

		public string ScenarioTitle
		{
			get
			{
				return ScenarioTitle__BackingField;
			}
			private set
			{
				ScenarioTitle__BackingField = value;
			}
		}

		public bool VoteResult
		{
			get
			{
				return VoteResult__BackingField;
			}
			set
			{
				VoteResult__BackingField = value;
			}
		}

		public BitLife_ScenarioVote SetVoteResult(bool result)
		{
			return null;
		}

		public void SetData(Scenario scenario)
		{
		}

		public override Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
