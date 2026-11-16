using System;
using System.Collections;

public class ScenarioBusinessNewProduct : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimProduct, string> _003C_003E9__4_0;

		internal string _003CRefreshScenario_003Eb__4_0(SimProduct x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public string productString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimProduct x)
		{
			return false;
		}
	}

	protected SimSpecialJobBusiness _specialJob;

	protected float _productLaunchCost;

	protected bool _isPush;

	public ScenarioBusinessNewProduct(SimSpecialJobBusiness specialJob, bool isPush = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void Research(SimProduct chosenProduct, string productString)
	{
	}

	private void Launch(SimProduct chosenProduct, string productString)
	{
	}

	private bool IsInvalidState(string context, string productString = null, SimProduct chosenProduct = null, bool validateProduct = false)
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
