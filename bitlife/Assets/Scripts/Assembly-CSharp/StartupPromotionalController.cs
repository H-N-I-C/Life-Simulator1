using System;
using System.Collections.Generic;
using BitLife.UI.Managers;

public class StartupPromotionalController
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<StartupPromotionalScenarioProvider, bool> _003C_003E9__0_0;

		public static Action _003C_003E9__13_0;

		internal bool _003CHasPromotionToShow_003Eb__0_0(StartupPromotionalScenarioProvider provider)
		{
			return false;
		}

		internal void _003CTryPresent_003Eb__13_0()
		{
		}
	}

	private static int completedSteps;

	private static bool presentationCheckScheduled;

	private const int TotalStepsRequired = 4;

	private static List<StartupPromotionalScenarioProvider> Providers;

	private static UIManager UIManager => null;

	public static bool HasPromotionToShow()
	{
		return false;
	}

	private static void InitAfterSceneLoad()
	{
	}

	private static void OnLogin()
	{
	}

	private static void OnGameReady()
	{
	}

	private static void OnOnboarding()
	{
	}

	private static void OnFlagsResolved()
	{
	}

	private static void HandleStepComplete()
	{
	}

	private static void TryPresent()
	{
	}
}
