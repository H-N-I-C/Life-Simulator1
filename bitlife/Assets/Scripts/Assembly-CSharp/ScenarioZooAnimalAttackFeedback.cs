using System;
using Game;

public class ScenarioZooAnimalAttackFeedback : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataZooAnimalAttackAction, int> _003C_003E9__4_0;

		internal int _003CRefreshScenario_003Eb__4_0(DataZooAnimalAttackAction t)
		{
			return 0;
		}
	}

	private SimZoo _zoo;

	private readonly SimZooAnimal _animal;

	private string _narrative;

	public ScenarioZooAnimalAttackFeedback(SimZooAnimal animal)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
