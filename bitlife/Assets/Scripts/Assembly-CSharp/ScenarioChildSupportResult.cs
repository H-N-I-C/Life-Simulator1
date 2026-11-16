using System;
using System.Collections.Generic;

public class ScenarioChildSupportResult : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimChild, bool> _003C_003E9__10_0;

		public static Func<SimChild, float> _003C_003E9__10_1;

		internal bool _003CChoiceSelectedAtIndex_003Eb__10_0(SimChild t)
		{
			return false;
		}

		internal float _003CChoiceSelectedAtIndex_003Eb__10_1(SimChild t)
		{
			return 0f;
		}
	}

	private readonly SimLover _exLover;

	private readonly List<SimChild> _children;

	private bool _yourCustody;

	private readonly int _yourCase;

	private readonly int _exCase;

	private double _amount;

	private string _narrative;

	public ScenarioChildSupportResult(List<SimChild> children, SimLover exLover, int yourCase, int exCase)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void CalculateChildSupportAmount()
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
