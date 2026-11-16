using System;
using System.Collections.Generic;

public class ScenarioBusinessAnnualReport : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<KeyValuePair<SimProduct, KeyValuePair<int, double>>, double> _003C_003E9__5_0;

		public static Func<SimAnnualReport, bool> _003C_003E9__5_1;

		public static Func<SimAnnualReport, bool> _003C_003E9__5_2;

		public static Func<SimAnnualReport, bool> _003C_003E9__5_3;

		public static Func<SimAnnualReport, bool> _003C_003E9__5_4;

		public static Func<SimAnnualReport, float> _003C_003E9__6_0;

		internal double _003CRefreshScenario_003Eb__5_0(KeyValuePair<SimProduct, KeyValuePair<int, double>> x)
		{
			return 0.0;
		}

		internal bool _003CRefreshScenario_003Eb__5_1(SimAnnualReport x)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__5_2(SimAnnualReport x)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__5_3(SimAnnualReport x)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__5_4(SimAnnualReport x)
		{
			return false;
		}

		internal float _003CChoiceSelectedAtIndex_003Eb__6_0(SimAnnualReport x)
		{
			return 0f;
		}
	}

	protected SimAnnualReport _theReport;

	protected SimSpecialJobBusiness _specialJob;

	protected bool _isPush;

	protected string _colorString;

	public ScenarioBusinessAnnualReport(SimAnnualReport report, SimSpecialJobBusiness job, bool push)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
