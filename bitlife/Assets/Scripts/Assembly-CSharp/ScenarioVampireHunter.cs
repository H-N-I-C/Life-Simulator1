using Game;

public class ScenarioVampireHunter : Scenario
{
	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public ScenarioVampireHunter _003C_003E4__this;

		public bool notFirstEncounter;

		internal bool _003CRefreshScenario_003Eb__0(DataVampireHunterEncounterScenario encounter)
		{
			return false;
		}
	}

	private string _narrative;

	private SimVampireHunter _hunter;

	private readonly SimVampire _vampire;

	private readonly string _extraDescription;

	private DataVampireHunterEncounterScenario _encounter;

	private readonly BracketParser _bracketParser;

	private readonly ChoiceRewardedVideo _rewardedVideo;

	public ScenarioVampireHunter(SimVampire vampire, string extraDescription = "")
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void UpdateNarrative()
	{
	}

	private void ShowAttackScenario()
	{
	}

	private void ShowSeduceScenario()
	{
	}

	private void ShowVanishScenario()
	{
	}

	private void ShowSurrenderScenario()
	{
	}

	private void ShowEscapeScenario()
	{
	}

	private void EscapeFailureScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private void HeroDeath()
	{
	}
}
