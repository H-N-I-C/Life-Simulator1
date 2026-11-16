using System;

public class ScenarioAstronautProfile : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPublication, bool> _003C_003E9__2_0;

		public static Func<SimPublication, bool> _003C_003E9__2_1;

		internal bool _003CRefreshScenario_003Eb__2_0(SimPublication x)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__2_1(SimPublication x)
		{
			return false;
		}
	}

	private readonly SimSpecialJobAstronaut _specialJob;

	public ScenarioAstronautProfile(SimSpecialJobAstronaut specialJob)
	{
	}

	public override void RefreshScenario()
	{
	}
}
