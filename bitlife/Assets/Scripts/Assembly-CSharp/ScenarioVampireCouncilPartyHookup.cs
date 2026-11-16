using System;

public class ScenarioVampireCouncilPartyHookup : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimVampireCouncilMember, bool> _003C_003E9__3_0;

		internal bool _003C_002Ector_003Eb__3_0(SimVampireCouncilMember q)
		{
			return false;
		}
	}

	private SimVampireCouncilMember _member;

	private string _narrative;

	private string _partyType;

	public ScenarioVampireCouncilPartyHookup(string partyType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
