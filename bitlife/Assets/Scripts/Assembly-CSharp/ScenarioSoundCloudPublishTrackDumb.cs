using System.Collections.Generic;
using Game;

public class ScenarioSoundCloudPublishTrackDumb : Scenario
{
	public class ChoiceData
	{
		private string Choice__BackingField;

		private string Narrative__BackingField;

		private int ReceptionImpact__BackingField;

		public string Choice
		{
			get
			{
				return Choice__BackingField;
			}
			set
			{
				Choice__BackingField = value;
			}
		}

		public string Narrative
		{
			get
			{
				return Narrative__BackingField;
			}
			set
			{
				Narrative__BackingField = value;
			}
		}

		public int ReceptionImpact
		{
			get
			{
				return ReceptionImpact__BackingField;
			}
			set
			{
				ReceptionImpact__BackingField = value;
			}
		}
	}

	private string _narrative;

	private readonly SoundCloudTrackPublishingActivity _activity;

	private BracketParser _parser;

	private readonly List<ChoiceData> _choices;

	public ScenarioSoundCloudPublishTrackDumb(SoundCloudTrackPublishingActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void BuildParser(string genderBracket)
	{
	}

	private SimPerson FindRandomChild()
	{
		return null;
	}

	private SimPerson FindRandomFriend()
	{
		return null;
	}

	private void BuildChoices(DataMusicSoundCloudDumbScenario dumb)
	{
	}

	private void LoadChoiceData(DataMusicSoundCloudDumbScenario dumb)
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void UpdateNarrative()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
