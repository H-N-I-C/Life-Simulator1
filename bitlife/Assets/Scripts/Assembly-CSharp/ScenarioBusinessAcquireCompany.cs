using System;
using System.Collections;

public class ScenarioBusinessAcquireCompany : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimProduct, double> _003C_003E9__5_0;

		public static Func<SimProduct, double> _003C_003E9__5_1;

		internal double _003CRefreshScenario_003Eb__5_0(SimProduct x)
		{
			return 0.0;
		}

		internal double _003CRefreshScenario_003Eb__5_1(SimProduct x)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public DataEmployeeType employeeType;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimEmployee x)
		{
			return false;
		}
	}

	protected SimBusiness _theBusiness;

	protected double _sellPrice;

	protected double _revenue;

	protected double _netIncome;

	public ScenarioBusinessAcquireCompany(SimBusiness theBusiness, double sellPrice)
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
