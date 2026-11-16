using System;
using System.Collections;
using Game;

public class ScenarioDealerAgeUpExtortMethod : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataDealerExtortionMethod, string> _003C_003E9__6_0;

		internal string _003CRefreshScenario_003Eb__6_0(DataDealerExtortionMethod t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private double _amount;

	private double _reward;

	private DataStreetPerson _person;

	public ScenarioDealerAgeUpExtortMethod(double amount, double reward, DataStreetPerson person)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
