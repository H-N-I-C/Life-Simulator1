using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioLuxurySecretSocietyMeetingDiscussion : Scenario
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string topicString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataLuxurySecretSocietyDiscussionTopic t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimLuxury _luxury;

	private readonly SimLuxurySecretSociety _society;

	private List<DataLuxurySecretSocietyDiscussionTopic> _data;

	public ScenarioLuxurySecretSocietyMeetingDiscussion(SimLuxurySecretSociety society)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
