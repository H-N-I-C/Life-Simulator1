using System;
using System.Collections;

public class ScenarioBirth : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimChild, bool> _003C_003E9__5_0;

		internal bool _003CChoiceSelectedAtIndex_003Eb__5_0(SimChild child)
		{
			return false;
		}
	}

	protected SimPregnancy _pregnancy;

	protected ArrayList _newChildArray;

	protected int _newChildGender;

	public ScenarioBirth(SimPregnancy thePregnancy)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public ArrayList GetPossibleSurnameArray()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
