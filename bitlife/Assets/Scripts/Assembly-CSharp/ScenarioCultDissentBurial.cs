using System;
using System.Collections;
using Game;

public class ScenarioCultDissentBurial : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataCultBodyDiscardLocation, string> _003C_003E9__5_0;

		internal string _003CRefreshScenario_003Eb__5_0(DataCultBodyDiscardLocation t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimCult _cult;

	private readonly SimCommunard _communard;

	private readonly SimCommunard _dissentCommunard;

	public ScenarioCultDissentBurial(SimCommunard communard, SimCommunard dissentCommunard)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
