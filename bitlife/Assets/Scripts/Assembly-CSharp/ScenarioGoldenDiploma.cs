using System;
using System.Collections;

public class ScenarioGoldenDiploma : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimOccupationStudent, string> _003C_003E9__2_0;

		internal string _003CRefreshScenario_003Eb__2_0(SimOccupationStudent x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass2_0
	{
		public int i;

		internal bool _003CRefreshScenario_003Eb__1(SimOccupationStudent x)
		{
			return false;
		}
	}

	public SCHOOL_MAJOR currentMajor;

	public ScenarioGoldenDiploma(int schoolMajor)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public bool IsRegularMajor(string degree)
	{
		return false;
	}

	public string SpecialDegreeName(int schoolType)
	{
		return null;
	}
}
