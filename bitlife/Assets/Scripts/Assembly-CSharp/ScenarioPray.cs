using System;
using System.Collections;

public class ScenarioPray : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDisease, bool> _003C_003E9__3_0;

		public static Func<SimDisease, bool> _003C_003E9__3_1;

		public static Func<SimDisease, int> _003C_003E9__3_2;

		public static Func<SimDisease, int> _003C_003E9__3_7;

		public static Func<SimPerson, bool> _003C_003E9__3_8;

		public static Func<SimPerson, bool> _003C_003E9__3_9;

		public static Func<SimDisease, bool> _003C_003E9__6_0;

		public static Func<SimDisease, bool> _003C_003E9__6_1;

		public static Func<SimDisease, bool> _003C_003E9__6_2;

		public static Func<SimDisease, bool> _003C_003E9__6_3;

		public static Func<SimDisease, bool> _003C_003E9__6_4;

		internal bool _003CChoiceSelectedAtIndex_003Eb__3_0(SimDisease x)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__3_1(SimDisease x)
		{
			return false;
		}

		internal int _003CChoiceSelectedAtIndex_003Eb__3_2(SimDisease x)
		{
			return 0;
		}

		internal int _003CChoiceSelectedAtIndex_003Eb__3_7(SimDisease x)
		{
			return 0;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__3_8(SimPerson x)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__3_9(SimPerson x)
		{
			return false;
		}

		internal bool _003CRelativeWithDisease_003Eb__6_0(SimDisease x)
		{
			return false;
		}

		internal bool _003CRelativeWithDisease_003Eb__6_1(SimDisease x)
		{
			return false;
		}

		internal bool _003CRelativeWithDisease_003Eb__6_2(SimDisease x)
		{
			return false;
		}

		internal bool _003CRelativeWithDisease_003Eb__6_3(SimDisease x)
		{
			return false;
		}

		internal bool _003CRelativeWithDisease_003Eb__6_4(SimDisease x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass3_0
	{
		public DataDiseaseType depressionDisease;

		public DataDiseaseType anxietyDisease;

		internal bool _003CChoiceSelectedAtIndex_003Eb__3(SimDisease x)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__4(SimDisease x)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__5(SimDisease x)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__6(SimDisease x)
		{
			return false;
		}
	}

	public static int MinMinutesSinceLastPrayRewardedVideo;

	protected bool _requireRewardedVideo;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void BitLifeGodsGiveHappiness()
	{
	}

	private int LocalValue(int value)
	{
		return 0;
	}

	private SimPerson RelativeWithDisease()
	{
		return null;
	}
}
