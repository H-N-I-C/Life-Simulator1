using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioDealerBribeAuthoritiesChooseMember : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDealerCartelMember, string> _003C_003E9__5_0;

		internal string _003CRefreshScenario_003Eb__5_0(SimDealerCartelMember t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private List<SimDealerCartelMember> _members;

	private ArrayList _dropDownContents;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
