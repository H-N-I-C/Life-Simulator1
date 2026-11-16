using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioChildSupportCustody : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimChild, string> _003C_003E9__4_0;

		internal string _003CRefreshScenario_003Eb__4_0(SimChild t)
		{
			return null;
		}
	}

	private readonly SimLover _exLover;

	private List<SimChild> _children;

	private string _narrative;

	public ScenarioChildSupportCustody(SimLover exLover)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void PushNextScenario()
	{
	}
}
