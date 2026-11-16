using System;
using System.Collections.Generic;

public class ScenarioBackToSchoolAgeUpPromRoyal : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimClassmate, float> _003C_003E9__8_0;

		public static Func<SimClassmate, float> _003C_003E9__9_0;

		public static Func<SimClassmate, float> _003C_003E9__10_0;

		public static Func<SimClassmate, float> _003C_003E9__12_0;

		public static Func<SimClassmate, float> _003C_003E9__14_0;

		public static Func<SimClassmate, bool> _003C_003E9__15_0;

		public static Func<SimClassmate, bool> _003C_003E9__18_0;

		internal float _003CClassMattesLooks_003Eb__8_0(SimClassmate t)
		{
			return 0f;
		}

		internal float _003CClassMattesHealth_003Eb__9_0(SimClassmate t)
		{
			return 0f;
		}

		internal float _003CClassMattesSmarts_003Eb__10_0(SimClassmate t)
		{
			return 0f;
		}

		internal float _003CTwoPopularClassmates_003Eb__12_0(SimClassmate t)
		{
			return 0f;
		}

		internal float _003CScenarioIsAgeAppropriateForPush_003Eb__14_0(SimClassmate t)
		{
			return 0f;
		}

		internal bool _003CRobbedOutcome_003Eb__15_0(SimClassmate t)
		{
			return false;
		}

		internal bool _003CRefusedOutcome_003Eb__18_0(SimClassmate t)
		{
			return false;
		}
	}

	private readonly SimOccupationStudent _student;

	private readonly string _schoolName;

	private readonly ChoiceRewardedVideo _rewardedVideo;

	private string _promType;

	private string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private float ClassMattesLooks()
	{
		return 0f;
	}

	private float ClassMattesHealth()
	{
		return 0f;
	}

	private float ClassMattesSmarts()
	{
		return 0f;
	}

	private bool ZeroClassmates()
	{
		return false;
	}

	private IEnumerable<SimClassmate> TwoPopularClassmates()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}

	private void RobbedOutcome()
	{
	}

	private Scenario CrownedOutcome()
	{
		return null;
	}

	private void DestinyOutcome()
	{
	}

	private void RefusedOutcome()
	{
	}
}
