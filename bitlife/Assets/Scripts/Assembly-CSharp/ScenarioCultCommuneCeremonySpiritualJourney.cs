using System;
using System.Collections;
using Game;

public class ScenarioCultCommuneCeremonySpiritualJourney : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataCultSpiritualJourneyDrug, string> _003C_003E9__3_0;

		internal string _003CRefreshScenario_003Eb__3_0(DataCultSpiritualJourneyDrug t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimCult _cult;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
