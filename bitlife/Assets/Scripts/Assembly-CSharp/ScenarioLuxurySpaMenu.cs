using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioLuxurySpaMenu : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataLuxurySpaTreatment, int> _003C_003E9__5_0;

		internal int _003CRefreshScenario_003Eb__5_0(DataLuxurySpaTreatment x)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string treatmentString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataLuxurySpaTreatment x)
		{
			return false;
		}
	}

	private string _narrative;

	private SimLuxury _luxury;

	private readonly LUXURY_SPA_TYPE _type;

	private List<DataLuxurySpaTreatment> _listOfTreatments;

	public ScenarioLuxurySpaMenu(LUXURY_SPA_TYPE type)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
