using System;
using System.Collections;
using Game;

public class ScenarioCultCommunardEducate : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataCultCommunardEducate, string> _003C_003E9__3_0;

		internal string _003CRefreshScenario_003Eb__3_0(DataCultCommunardEducate t)
		{
			return null;
		}
	}

	private string _narrative;

	private readonly SimCommunard _communard;

	public ScenarioCultCommunardEducate(SimCommunard communard)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
