using System;
using System.Collections;
using Game;

public class ScenarioModelAgencyModelSabotage : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataModelAgencyModelSabotage, string> _003C_003E9__4_0;

		internal string _003CRefreshScenario_003Eb__4_0(DataModelAgencyModelSabotage t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimSpecialJobModel _modelJob;

	private readonly SimModel _model;

	public ScenarioModelAgencyModelSabotage(SimModel model)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
