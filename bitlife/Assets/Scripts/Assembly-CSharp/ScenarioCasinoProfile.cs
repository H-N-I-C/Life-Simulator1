using System;

public class ScenarioCasinoProfile : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCasinoGameRoom, bool> _003C_003E9__6_0;

		public static Func<SimCasinoGameRoom, float> _003C_003E9__6_1;

		internal bool _003CLowPopularityRooms_003Eb__6_0(SimCasinoGameRoom x)
		{
			return false;
		}

		internal float _003CLowPopularityRooms_003Eb__6_1(SimCasinoGameRoom t)
		{
			return 0f;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	public override void RefreshScenario()
	{
	}

	private bool CelebrityWithDifferentTheme(out SimCasinoCelebrity celebrity)
	{
		celebrity = null;
		return false;
	}

	private bool ActWithDifferentTheme(out SimCasinoEntertainmentAct act)
	{
		act = null;
		return false;
	}

	private bool AmenityWithDifferentTheme(out SimCasinoAmenity amenity)
	{
		amenity = null;
		return false;
	}

	private bool LowPopularityRooms(out SimCasinoGameRoom room)
	{
		room = null;
		return false;
	}

	private bool _003CActWithDifferentTheme_003Eb__4_0(SimCasinoEntertainmentAct t)
	{
		return false;
	}

	private bool _003CAmenityWithDifferentTheme_003Eb__5_0(SimCasinoAmenity t)
	{
		return false;
	}
}
