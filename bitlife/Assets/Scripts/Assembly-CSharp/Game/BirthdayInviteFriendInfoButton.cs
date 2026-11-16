using BitLife.UI;
using UnityEngine;

namespace Game
{
	public class BirthdayInviteFriendInfoButton : MonoBehaviour, IInfoButton
	{
		private const string PipNotifPrefName = "BirthdayInvitePipNotifClicked";

		private const string PrefabName = "infobuttons/Info_button_birthday_invite_friend";

		private static bool _telemetryReported;

		private static bool _alreadyClicked;

		private GameObject pipNotif;

		private void Start()
		{
		}

		public void OnClick()
		{
		}

		public void OnRemove()
		{
		}

		public void OnAdd()
		{
		}

		public static void ShowButton()
		{
		}

		public static void HideButton()
		{
		}
	}
}
