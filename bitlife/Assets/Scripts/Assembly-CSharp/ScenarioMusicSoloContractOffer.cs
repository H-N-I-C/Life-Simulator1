using System;

public class ScenarioMusicSoloContractOffer : Scenario
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

	protected SimMusicSoloContract _contract;

	protected int _role;

	protected bool _newContractAtCurrentCompany;

	protected bool _currentlyInBand;

	public ScenarioMusicSoloContractOffer(SimMusicSoloContract theContract, int theRole)
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
