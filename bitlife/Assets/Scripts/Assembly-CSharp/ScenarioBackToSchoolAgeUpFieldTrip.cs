using System;
using Game;

public class ScenarioBackToSchoolAgeUpFieldTrip : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimClassmate, float> _003C_003E9__15_0;

		internal float _003CGetLocationColor_003Eb__15_0(SimClassmate t)
		{
			return 0f;
		}
	}

	private SimOccupationStudent _student;

	private SimTeacher _teacher;

	private string _location;

	private TEACHER_TYPE _teacherType;

	private string _narrative;

	public override void RefreshScenario()
	{
	}

	private SimTeacher SelectTeacher(TEACHER_TYPE teacherType)
	{
		return null;
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void StayHomeFeedback()
	{
	}

	private void GoofOffFeedback()
	{
	}

	private void SocializeFeedback()
	{
	}

	private void PayAttentionFeedback()
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}

	private (string, SimClassmate) GetLocationColor(FIELD_TRIP_FOCUS focus, TEACHER_TYPE type)
	{
		return default;
	}
}
