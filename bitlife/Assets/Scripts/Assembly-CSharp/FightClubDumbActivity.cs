using System;
using System.Collections.Generic;
using Game;

public class FightClubDumbActivity : DumbScenarioLazyActivity<DataClubUpdateUFLDumbScenario>
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPerson, bool> _003C_003E9__11_0;

		public static Func<SimPerson, bool> _003C_003E9__13_0;

		internal bool _003CIsDataValid_003Eb__11_0(SimPerson friend)
		{
			return false;
		}

		internal bool _003CTryGetFocusPerson_003Eb__13_0(SimPerson friend)
		{
			return false;
		}
	}

	private readonly Life _life;

	private readonly SimHero _hero;

	private SimPerson _selectedPerson;

	private const string ParentGenderBracket = "<parent>";

	private const string LoverGenderBracket = "<lover>";

	private const string FriendGenderBracket = "<friend>";

	public override void ApplyImpacts(IDumbChoice choice)
	{
	}

	public override void HandleGenderBracket(string genderBracket)
	{
	}

	public override Scenario GetScenario()
	{
		return null;
	}

	public override bool MeetCriteria()
	{
		return false;
	}

	protected override List<DataClubUpdateUFLDumbScenario> GetDataSet()
	{
		return null;
	}

	protected override bool IsDataValid(DataClubUpdateUFLDumbScenario data)
	{
		return false;
	}

	public SimPerson GetFocusPerson()
	{
		return null;
	}

	private SimPerson TryGetFocusPerson(string genderBracket)
	{
		return null;
	}

	private SimPerson SelectRandomParent(Life life)
	{
		return null;
	}

	public void ScenarioShown()
	{
	}
}
