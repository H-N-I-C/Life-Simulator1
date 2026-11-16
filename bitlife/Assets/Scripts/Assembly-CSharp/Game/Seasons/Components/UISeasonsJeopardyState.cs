using System;
using Backend;
using UnityEngine;

namespace Game.Seasons.Components
{
	public class UISeasonsJeopardyState : MonoBehaviour
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<bool> _003C_003E9__6_1;

			internal void _003CPlayAnimation_003Eb__6_1(bool _)
			{
			}
		}

		private bool _coolDownActive;

		private GameObject _animationObject;

		private GameObject _normalState;

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnSeasonTimeChanged(SeasonTimeInfo info)
		{
		}

		private void PlayAnimation()
		{
		}

		private void ResetCoolDown()
		{
		}

		private void ResetAnimation()
		{
		}

		private void _003CPlayAnimation_003Eb__6_0(MYSTUFF_USE_STATE useStatus)
		{
		}
	}
}
