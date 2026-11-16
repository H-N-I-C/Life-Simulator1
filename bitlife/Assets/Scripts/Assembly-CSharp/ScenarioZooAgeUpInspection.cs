using System;

public class ScenarioZooAgeUpInspection : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Comparison<SimZooHabitat> _003C_003E9__5_0;

		public static Comparison<SimZooAnimal> _003C_003E9__6_0;

		internal int _003CGetHabitatWithWorstLivingConditionUnder50_003Eb__5_0(SimZooHabitat x, SimZooHabitat y)
		{
			return 0;
		}

		internal int _003CGetAnimalWithLessThan20HealthWithoutTerminalDisease_003Eb__6_0(SimZooAnimal x, SimZooAnimal y)
		{
			return 0;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private static SimZooHabitat GetHabitatWithWorstLivingConditionUnder50()
	{
		return null;
	}

	private static SimZooAnimal GetAnimalWithLessThan20HealthWithoutTerminalDisease()
	{
		return null;
	}

	private static SimZooHabitat GetRandomHabitatWithNoHandler()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
