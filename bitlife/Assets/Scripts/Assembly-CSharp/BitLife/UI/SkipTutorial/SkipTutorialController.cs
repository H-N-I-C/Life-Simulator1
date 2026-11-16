using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.SkipTutorial
{
	public class SkipTutorialController : MonoBehaviour
	{
		private const string ButtonLabel = "Skip Tutorial";

		private const string ConfirmationCopy = "Skip the intro and go straight to creating a life?";

		private CandyButton skipButton;

		private AccessibleCandyButton accessibleSkipButton;

		private bool confirmationOpen;

		private bool skipConfirmed;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		private void ShowConfirmation()
		{
		}

		private void OnConfirmationSelected(int selectedIndex)
		{
		}

		private void DestroySkipButton()
		{
		}
	}
}
