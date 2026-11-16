using System;

public class ScenarioActorFilmContract : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Scenario, bool> _003C_003E9__5_0;

		internal bool _003CChoiceSelectedAtIndex_003Eb__5_0(Scenario x)
		{
			return false;
		}
	}

	protected SimActorFilmRole _role;

	protected SimActorFilm _film;

	protected bool _allowNegotiation;

	public ScenarioActorFilmContract(SimActorFilmRole theRole, bool shouldAllowNegotiation)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
