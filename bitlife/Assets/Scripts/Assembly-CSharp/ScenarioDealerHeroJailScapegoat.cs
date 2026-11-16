using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioDealerHeroJailScapegoat : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDealerCartelMember, bool> _003C_003E9__6_0;

		public static Func<SimDealerCartelMember, string> _003C_003E9__6_1;

		internal bool _003CRefreshScenario_003Eb__6_0(SimDealerCartelMember t)
		{
			return false;
		}

		internal string _003CRefreshScenario_003Eb__6_1(SimDealerCartelMember t)
		{
			return null;
		}
	}

	private string _narrative;

	private readonly SimSpecialJobDealer _dealerJob;

	private List<SimDealerCartelMember> _members;

	private ArrayList _dropDownContents;

	private SimOccupationPrisoner _prisoner;

	public ScenarioDealerHeroJailScapegoat(SimSpecialJobDealer dealerJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private string GetSentenceCrimesString(ArrayList crimes)
	{
		return null;
	}
}
