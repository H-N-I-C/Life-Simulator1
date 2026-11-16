using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioVampireCouncilBloodBath : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataVampireBloodBathScenario, string> _003C_003E9__4_0;

		public static Func<SimVampireCouncilMember, bool> _003C_003E9__4_1;

		public static Func<SimVampireCouncilMember, string> _003C_003E9__4_2;

		internal string _003CRefreshScenario_003Eb__4_0(DataVampireBloodBathScenario activity)
		{
			return null;
		}

		internal bool _003CRefreshScenario_003Eb__4_1(SimVampireCouncilMember m)
		{
			return false;
		}

		internal string _003CRefreshScenario_003Eb__4_2(SimVampireCouncilMember p)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public string activityString;

		public string memberString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataVampireBloodBathScenario activity)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__1(SimVampireCouncilMember aMember)
		{
			return false;
		}
	}

	private string _narrative;

	private List<SimVampireCouncilMember> _members;

	private List<DataVampireBloodBathScenario> _activities;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void UpdateNarrative()
	{
	}

	private void HandleActivityImpact(DataVampireBloodBathScenario selectedActivity, SimPerson member)
	{
	}

	private int CalculateImpact(int maxImpact)
	{
		return 0;
	}

	private bool CalculateActivitySuccess(SimVampireCouncilMember member)
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
