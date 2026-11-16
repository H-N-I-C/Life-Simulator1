using System;
using System.Collections.Generic;
using Genesis.Controllers;

public class ScenarioDefendLawsuitLawyer : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCar, string> _003C_003E9__12_0;

		public static Func<SimJewel, string> _003C_003E9__12_1;

		public static Func<SimMusicInstrumentPhysical, string> _003C_003E9__12_2;

		public static Func<SimHouse, string> _003C_003E9__12_3;

		internal string _003CGetAssetsToLiquidate_003Eb__12_0(SimCar t)
		{
			return null;
		}

		internal string _003CGetAssetsToLiquidate_003Eb__12_1(SimJewel t)
		{
			return null;
		}

		internal string _003CGetAssetsToLiquidate_003Eb__12_2(SimMusicInstrumentPhysical t)
		{
			return null;
		}

		internal string _003CGetAssetsToLiquidate_003Eb__12_3(SimHouse t)
		{
			return null;
		}
	}

	private string _narrative;

	private float _firm1Cost;

	private float _firm2Cost;

	private readonly string _reason;

	private readonly float _damages;

	private readonly int _scenarioStyle;

	private readonly string _whoString;

	private readonly string _postProcess;

	private readonly ScenarioController _dataController;

	public ScenarioDefendLawsuitLawyer(int scenarioStyle, string whoString, string reason, float damages, string postProcess, ScenarioController dataController = null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private (List<string>, float) GetAssetsToLiquidate(float amount)
	{
		return default;
	}

	public override string EmojiString()
	{
		return null;
	}
}
