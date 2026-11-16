using System;
using System.Collections.Generic;

public class ScenarioVampireHeroCoffinFeedback : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<SimPerson> _003C_003E9__6_0;

		internal bool _003CGetRelationshipsImpacted_003Eb__6_0(SimPerson x)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly Life _life;

	private readonly SimVampire _heroVampire;

	public override void RefreshScenario()
	{
	}

	private void ImpactHero()
	{
	}

	private List<SimPerson> GetRelationshipsImpacted()
	{
		return null;
	}

	public override void DismissSelected()
	{
	}
}
