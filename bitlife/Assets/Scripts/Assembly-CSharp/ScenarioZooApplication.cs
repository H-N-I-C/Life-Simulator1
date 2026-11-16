using System;

public class ScenarioZooApplication : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooHabitat, string> _003C_003E9__3_0;

		internal string _003CRefreshScenario_003Eb__3_0(SimZooHabitat x)
		{
			return null;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	public ScenarioZooApplication(SimZoo zoo)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
