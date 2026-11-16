using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioBackToSchoolDanceClassMateInvite : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimClassmate, string> _003C_003E9__4_0;

		public static Comparison<SimClassmate> _003C_003E9__9_0;

		public static Comparison<SimClassmate> _003C_003E9__9_1;

		internal string _003CRefreshScenario_003Eb__4_0(SimClassmate classMate)
		{
			return null;
		}

		internal int _003CGetFourBestRelationshipsPlusOnePopular_003Eb__9_0(SimClassmate left, SimClassmate right)
		{
			return 0;
		}

		internal int _003CGetFourBestRelationshipsPlusOnePopular_003Eb__9_1(SimClassmate left, SimClassmate right)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public string chosenPartnerName;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimClassmate person)
		{
			return false;
		}
	}

	private readonly SchoolDanceActivity _activity;

	private List<SimClassmate> _topChoices;

	private readonly ChoiceRewardedVideo _rewardedVideo;

	public ScenarioBackToSchoolDanceClassMateInvite(SchoolDanceActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void WorkUpTheCourageToAskOut(SimClassmate classMate)
	{
	}

	private Scenario SucceedToAskOut(SimClassmate classMate)
	{
		return null;
	}

	private void FailedToAskOut(SimClassmate classMate)
	{
	}

	private List<SimClassmate> GetFourBestRelationshipsPlusOnePopular()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
