using System;

public class ScenarioCasinoAmenityInteractionMafiaMember : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string, string> _003C_003E9__7_0;

		internal string _003CChoiceSelectedAtIndex_003Eb__7_0(string i, string j)
		{
			return null;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoAmenity _amenity;

	private SimCasinoMafiaMember _mafiaMember;

	private bool _leavesCasino;

	public ScenarioCasinoAmenityInteractionMafiaMember(SimCasinoAmenity amenity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
