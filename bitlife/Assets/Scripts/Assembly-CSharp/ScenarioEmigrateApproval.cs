using System;
using System.Collections;

public class ScenarioEmigrateApproval : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimEmigration, bool> _003C_003E9__8_0;

		public static Func<SimEmigration, bool> _003C_003E9__8_1;

		public static Func<SimEmigration, bool> _003C_003E9__8_2;

		public static Func<SimEmigration, bool> _003C_003E9__8_3;

		internal bool _003CChoiceSelectedAtIndex_003Eb__8_0(SimEmigration x)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__8_1(SimEmigration x)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__8_2(SimEmigration x)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__8_3(SimEmigration x)
		{
			return false;
		}
	}

	protected SimPlace _place;

	protected bool _currentLoverGoing;

	protected ArrayList _emigratingChildArray;

	protected float _cost;

	protected string _narrative;

	public ScenarioEmigrateApproval(SimPlace thePlace)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
