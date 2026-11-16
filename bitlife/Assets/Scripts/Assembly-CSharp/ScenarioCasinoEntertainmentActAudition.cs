using System;

public class ScenarioCasinoEntertainmentActAudition : Scenario
{
	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public string actName;

		public Func<SimCasinoEntertainmentAct, bool> _003C_003E9__0;

		internal bool _003CRefreshScenario_003Eb__0(SimCasinoEntertainmentAct t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	private SimCasinoEntertainmentAct _act;

	private int _numAuditions;

	private readonly string _theme;

	private readonly string _category;

	private readonly float _salary;

	public ScenarioCasinoEntertainmentActAudition(string theme, string category, float salary, int numAuditions = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
