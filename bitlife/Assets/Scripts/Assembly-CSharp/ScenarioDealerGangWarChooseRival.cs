using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioDealerGangWarChooseRival : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDealerCartel, float> _003C_003E9__5_0;

		internal float _003CRefreshScenario_003Eb__5_0(SimDealerCartel t)
		{
			return 0f;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private List<SimDealerCartel> _rivalCartels;

	private ArrayList _dropDownContents;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
