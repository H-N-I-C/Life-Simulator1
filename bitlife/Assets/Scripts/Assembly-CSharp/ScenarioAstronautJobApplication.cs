using System;
using System.Collections.Generic;

public class ScenarioAstronautJobApplication : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string, string> _003C_003E9__6_0;

		internal string _003CRefreshScenario_003Eb__6_0(string i, string j)
		{
			return null;
		}
	}

	private static string _previousQuestion;

	private string _currentAnswer;

	private SimSpecialJobAstronaut _specialJob;

	private string _narrative;

	private readonly int _startRank;

	public ScenarioAstronautJobApplication(SimSpecialJobAstronaut specialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private List<string> CheckProgramCondition(int startRank)
	{
		return null;
	}

	public static bool CanAchieveRank(DataAstronautCareerRank rankData, float avgScoreTraining)
	{
		return false;
	}
}
