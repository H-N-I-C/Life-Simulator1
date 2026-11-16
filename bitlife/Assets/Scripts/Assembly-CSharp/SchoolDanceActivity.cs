using System;
using System.Collections.Generic;

public class SchoolDanceActivity
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPerson, bool> _003C_003E9__26_0;

		public static Func<SimPerson, bool> _003C_003E9__29_0;

		internal bool _003CGoWithAFriendFeedback_003Eb__26_0(SimPerson friend)
		{
			return false;
		}

		internal bool _003CGoWithFriend_003Eb__29_0(SimPerson friend)
		{
			return false;
		}
	}

	private const string FRIEND_COMPANION_TYPE = "friend";

	private const string LOVER_COMPANION_TYPE = "lover";

	private const string NO_COMPANION_TYPE = "none";

	private readonly SimHero _hero;

	private readonly SimOccupationStudent _occupation;

	private readonly Life _life;

	private SimPerson _companion;

	private string _danceTheme;

	public SimHero Hero => null;

	public void AddEnemy(SimPerson person)
	{
	}

	public bool HasAttendedDanceThisYear()
	{
		return false;
	}

	public void AttendDance()
	{
	}

	public SimLover GetLover()
	{
		return null;
	}

	public SimPerson GetCompanion()
	{
		return null;
	}

	public bool IsSingle()
	{
		return false;
	}

	public float GetSchoolPopularity()
	{
		return 0f;
	}

	public void ImpactHappiness(int happiness)
	{
	}

	public void ImpactPopularity(int popularityImpact)
	{
	}

	public void SetCompanion(SimPerson companion)
	{
	}

	public void SetDanceTheme(string danceTheme)
	{
	}

	public string GetDanceTheme()
	{
		return null;
	}

	public string GetCompanionType()
	{
		return null;
	}

	private List<SimPerson> GetActiveFriendsList()
	{
		return null;
	}

	public List<SimClassmate> GetClassmatesList()
	{
		return null;
	}

	public Scenario GoWithAFriendFeedback()
	{
		return null;
	}

	public Scenario GoAloneFeedback()
	{
		return null;
	}

	public void LogNarrative(string narrative)
	{
	}

	private Scenario GoWithFriend()
	{
		return null;
	}

	private void CheckLoverArgument(Scenario toAppendTo)
	{
	}

	private Scenario ForeverAloneFeedback()
	{
		return null;
	}

	private Scenario NoFriendWantsToGoFeedback()
	{
		return null;
	}
}
