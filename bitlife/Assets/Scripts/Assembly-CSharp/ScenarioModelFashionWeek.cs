using System;

public class ScenarioModelFashionWeek : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimModelingAgencyClient, string> _003C_003E9__4_0;

		public static Func<SimModelingAgencyClient, float> _003C_003E9__4_1;

		internal string _003CRefreshScenario_003Eb__4_0(SimModelingAgencyClient x)
		{
			return null;
		}

		internal float _003CRefreshScenario_003Eb__4_1(SimModelingAgencyClient x)
		{
			return 0f;
		}
	}

	private string _narrative;

	private SimSpecialJobModel _modelJob;

	private SimFashionWeek _fashionWeek;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
