using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class OutdoorCaveSpelunkActivityData
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<(int, ScenarioChoice), ScenarioChoice> _003C_003E9__16_0;

		internal ScenarioChoice _003CGetStepButtons_003Eb__16_0((int, ScenarioChoice) t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass17_0
	{
		public string buttonName;

		internal bool _003CGetButtonIndex_003Eb__0((int, ScenarioChoice) t)
		{
			return false;
		}
	}

	private int _totalRisk;

	private int _stepIndex;

	private List<(int, ScenarioChoice)> _riskButton;

	private readonly DataOutdoorCave _cave;

	private float _enjoyment;

	private List<OutdoorCaveData> _caveData;

	public DataOutdoorCave Cave => null;

	public float Enjoyment => 0f;

	public int TotalRisk => 0;

	public int StepIndex => 0;

	public OutdoorCaveSpelunkActivityData(DataOutdoorCave dataOutdoorCave)
	{
	}

	private void GenerateDiveScenarios(string caveName)
	{
	}

	public ArrayList GetStepButtons()
	{
		return null;
	}

	public int GetButtonIndex(string buttonName)
	{
		return 0;
	}

	public void IncrementStepIndex()
	{
	}

	public Scenario GetNextScenario(SimOutdoor outdoorLifestyle, int riskScore)
	{
		return null;
	}

	private int GetLimitsScenario(int limitAmountScenarios)
	{
		return 0;
	}

	public float GetEnjoyment()
	{
		return 0f;
	}

	public void ImpactEnjoyment(int score)
	{
	}

	public string GetDiveDescription()
	{
		return null;
	}
}
