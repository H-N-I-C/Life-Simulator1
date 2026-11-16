using System;

public class ScenarioLandlordPortfolio : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPropertyLeaseContract, float> _003C_003E9__1_1;

		public static Func<SimPropertyLeaseContract, float> _003C_003E9__1_2;

		public static Func<SimPropertyLeaseContract, double> _003C_003E9__1_0;

		internal float _003CRefreshScenario_003Eb__1_1(SimPropertyLeaseContract x)
		{
			return 0f;
		}

		internal float _003CRefreshScenario_003Eb__1_2(SimPropertyLeaseContract x)
		{
			return 0f;
		}

		internal double _003CRefreshScenario_003Eb__1_0(SimPropertyLeaseContract x)
		{
			return 0.0;
		}
	}

	public override void RefreshScenario()
	{
	}
}
