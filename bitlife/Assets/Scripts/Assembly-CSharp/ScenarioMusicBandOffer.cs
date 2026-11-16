using System;

public class ScenarioMusicBandOffer : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Scenario, bool> _003C_003E9__6_0;

		internal bool _003CChoiceSelectedAtIndex_003Eb__6_0(Scenario x)
		{
			return false;
		}
	}

	protected SimMusicBand _band;

	protected int _role;

	protected bool _afterAudition;

	protected bool _currentlySoloArtist;

	public ScenarioMusicBandOffer(SimMusicBand theBand, int theRole, bool isAfterAudition)
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
}
