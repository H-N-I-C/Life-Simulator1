using System;
using System.Collections.Generic;
using Backend;

public class ScenarioUFMCustomLifeOffer : Scenario
{
	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public ScenarioUFMCustomLifeOffer _003C_003E4__this;

		public StuffItem existingItem;

		public Action<IMyStuffUseCheck> _003C_003E9__2;

		public Action<MYSTUFF_REDEEM> _003C_003E9__4;

		internal void _003COnSwitchToThisLife_003Eb__0(MYSTUFF_USE_STATE useState)
		{
		}

		internal void _003COnSwitchToThisLife_003Eb__2(IMyStuffUseCheck check)
		{
		}

		internal void _003COnSwitchToThisLife_003Eb__1(List<StuffItem> result)
		{
		}

		internal void _003COnSwitchToThisLife_003Eb__4(MYSTUFF_REDEEM resultRedeem)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_1
	{
		public IMyStuffUseCheck check;

		public _003C_003Ec__DisplayClass5_0 CS_0024_003C_003E8__locals1;

		internal void _003COnSwitchToThisLife_003Eb__3(bool resultExecute)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_2
	{
		public StuffItem item;

		internal void _003COnSwitchToThisLife_003Eb__5(IMyStuffUseCheck check)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_3
	{
		public IMyStuffUseCheck check;

		public _003C_003Ec__DisplayClass5_2 CS_0024_003C_003E8__locals2;

		public Action<bool> _003C_003E9__7;

		internal void _003COnSwitchToThisLife_003Eb__6(MYSTUFF_USE_STATE success)
		{
		}

		internal void _003COnSwitchToThisLife_003Eb__7(bool resultExecute)
		{
		}
	}

	private const string DukeSupItemId = "CUSTOM_LIFE_DUKE_SUP";

	private const string RedeemDukeSupItemId = "CUSTOM_LIFE_DUKE_SUP_REDEEM";

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void OnSwitchToThisLife()
	{
	}

	private void OnImGonnaWait()
	{
	}

	public static StuffItem GetRedeemItem()
	{
		return null;
	}

	private void ShowErrorMessage()
	{
	}

	private static ScenarioUFMCustomLifeGuidance.Branch GetWaitGuidanceBranch()
	{
		return ScenarioUFMCustomLifeGuidance.Branch.Switched;
	}

	private static void InjectGuidanceScenario(ScenarioUFMCustomLifeGuidance.Branch branch, Scenario nextScenario = null)
	{
	}

	private void _003COnImGonnaWait_003Eb__6_0(MYSTUFF_REDEEM resultRedeem)
	{
	}
}
