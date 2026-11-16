using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioCrimeMurder : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimOutdoorHandbookEntry, bool> _003C_003E9__6_0;

		public static Predicate<SimOutdoorHandbookEntry> _003C_003E9__6_2;

		public static Func<DataMurder, bool> _003C_003E9__6_3;

		public static Func<DataMurder, string> _003C_003E9__6_4;

		internal bool _003CRefreshScenario_003Eb__6_0(SimOutdoorHandbookEntry x)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__6_2(SimOutdoorHandbookEntry x)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__6_3(DataMurder murder)
		{
			return false;
		}

		internal string _003CRefreshScenario_003Eb__6_4(DataMurder q)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public List<string> murderMethodSet;

		internal bool _003CRefreshScenario_003Eb__1(string q)
		{
			return false;
		}
	}

	private DataMurder _vampireTeeth;

	private SimOutdoorPlant _outdoorLifestylePlant;

	private DataMurder _outdoorLifestylePlantMurder;

	private List<DataMurder> _vampireMurderMethods;

	private string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private string GetSuccessString(DataMurder theMurderMethod, string retaliationMurderTargetName, string retaliationMurderTargetNamePossessive, GENDER gender)
	{
		return null;
	}

	private void ShowHunterAppearScenario(SimMurderTarget theMurderTarget)
	{
	}

	private static string GetVampireMurderAttemptTitle()
	{
		return null;
	}

	private void ShowVampireAppearScenario(SimMurderTarget murderTarget)
	{
	}

	private string GetVampireRetaliationTitleScenario()
	{
		return null;
	}

	private static Scenario KillHero(DEATH_TYPE deathType)
	{
		return null;
	}

	private string GetVampireDeathRetaliationBodyString(SimMurderTarget target)
	{
		return null;
	}

	private string GetVampireFailedMurderString(SimPerson target)
	{
		return null;
	}

	public void ImpactKarmaForEvenConsideringIt()
	{
	}

	private int GetMurderAttemptOutcomeForTargetAndMethod(SimMurderTarget target, DataMurder murder)
	{
		return 0;
	}
}
