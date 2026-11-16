using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioCultCommuneCeremonyHealing : Scenario
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string communardDisease;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimDisease t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimCult _cult;

	private SimCommunard _communard;

	private List<DataCultHealingCeremony> _healingCeremonyList;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
