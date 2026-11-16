using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioCasinoAmenityInstallPickAmenity : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCasinoAmenity, string> _003C_003E9__5_0;

		public static Func<SimCasinoAmenity, string> _003C_003E9__5_1;

		public static Func<DataCasinoAmenity, string> _003C_003E9__5_2;

		public static Func<DataCasinoAmenity, string> _003C_003E9__5_3;

		internal string _003CRefreshScenario_003Eb__5_0(SimCasinoAmenity t)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__5_1(SimCasinoAmenity t)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__5_2(DataCasinoAmenity t)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__5_3(DataCasinoAmenity t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string amenityName;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataCasinoAmenity t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	private readonly string _category;

	private List<DataCasinoAmenity> _amenities;

	public ScenarioCasinoAmenityInstallPickAmenity(string category)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
