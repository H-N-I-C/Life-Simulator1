using System;
using UnityEngine;

namespace BitLife.UI.Managers
{
	public class UIOnboardingManager : MonoBehaviour
	{
		public static UIOnboardingManager Inst;

		private Transform onboardingSequenceParent;

		private Action[] steps;

		private bool isInit;

		private bool isDestroyed;

		public static bool ShouldShowOnboarding;

		public static Action<GameObject> OnOnboardingCreated;

		private GameObject onboardingInstance;

		public bool IsShowing => false;

		private void Awake()
		{
		}

		private void OnDisable()
		{
		}

		public void SelfDestruct()
		{
		}

		public void DestroyOnboardingForSkip()
		{
		}

		public void Step5InitCheck()
		{
		}

		private void Init()
		{
		}

		private void OnTranslated(bool obj)
		{
		}

		public void ShowStep(int step)
		{
		}

		public bool ShouldShowOnboardingCheck(int forStepIndex)
		{
			return false;
		}
	}
}
