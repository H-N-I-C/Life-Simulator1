using System;
using System.Collections.Generic;

public abstract class ScenarioMusicProducerAgeUpsBangerzAwardsWon : ScenarioMusicProducerAgeUp
{
	private readonly List<KeyValuePair<ScenarioChoice, Action>> _choiceActionList;

	private readonly SimBangerzAwardsNomination _nomination;

	public ScenarioMusicProducerAgeUpsBangerzAwardsWon(SimBangerzAwardsNomination nomination)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private void AcceptItGraciously()
	{
	}

	private void TurnItDown()
	{
	}

	private void BeSmugAboutIt()
	{
	}
}
