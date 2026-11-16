using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioDealerCartelMemberScapegoat : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDealerCartelMember, string> _003C_003E9__6_0;

		internal string _003CRefreshScenario_003Eb__6_0(SimDealerCartelMember t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerCartelMember _member;

	private List<SimDealerCartelMember> _members;

	private ArrayList _dropDownContents;

	public ScenarioDealerCartelMemberScapegoat(SimDealerCartelMember member)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
