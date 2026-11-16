using System;
using System.Collections;
using System.Collections.Generic;
using Game.Seasons.Models;
using TMPro;
using UnityEngine;

namespace Game.Seasons.Components
{
	public class UISeasonsBar : MonoBehaviour
	{
		[Serializable]
		public class ItemSection
		{
			[Serializable]
			private sealed class _003C_003Ec
			{
				public static readonly _003C_003Ec _003C_003E9;

				public static Action<UISeasonsItem> _003C_003E9__3_0;

				internal void _003CSetActive_003Eb__3_0(UISeasonsItem i)
				{
				}
			}

			private sealed class _003C_003Ec__DisplayClass4_0
			{
				public THEME theme;

				internal void _003CSetActiveTheme_003Eb__0(UISeasonsItem i)
				{
				}
			}

			private sealed class _003C_003Ec__DisplayClass5_0
			{
				public bool enabled;

				internal void _003CEnableItems_003Eb__0(UISeasonsItem i)
				{
				}
			}

			private sealed class _003CEnableItemsAfterDelay_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
			{
				private int _003C_003E1__state;

				private object _003C_003E2__current;

				public float seconds;

				public ItemSection _003C_003E4__this;

				object IEnumerator<object>.Current => null;

				object IEnumerator.Current => null;

				public _003CEnableItemsAfterDelay_003Ed__6(int _003C_003E1__state)
				{
				}

				void IDisposable.Dispose()
				{
				}

				private bool MoveNext()
				{
					return false;
				}

				bool IEnumerator.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					return this.MoveNext();
				}

				void IEnumerator.Reset()
				{
				}
			}

			public SectionType type;

			public GameObject parent;

			public List<UISeasonsItem> items;

			public void SetActive(SectionType active)
			{
			}

			public void SetActiveTheme(THEME theme)
			{
			}

			public void EnableItems(bool enabled)
			{
			}

			public IEnumerator EnableItemsAfterDelay(float seconds)
			{
				return null;
			}
		}

		public enum SectionType
		{
			ActiveSeason = 0,
			NextSeason = 1,
			SeasonNotStarted = 2
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action _003C_003E9__15_0;

			internal void _003CUpdateUiCountDown_003Eb__15_0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_0
		{
			public SectionType sectionType;

			internal void _003CShowSection_003Eb__0(ItemSection i)
			{
			}
		}

		private sealed class _003CTransitionAnimation_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UISeasonsBar _003C_003E4__this;

			public SectionType type;

			private int _003Ccount_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CTransitionAnimation_003Ed__17(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		private SectionType activeSection;

		private List<ItemSection> sections;

		private TextMeshProUGUI nextSeasonInfoBarLabel;

		private Animator animator;

		private THEME activeTheme;

		private bool _animationActive;

		private bool _countDownActive;

		private int _realTimeSeconds;

		public void OnItemClicked(UISeasonsItem item)
		{
		}

		public void UpdateScreen()
		{
		}

		private void OnEnable()
		{
		}

		private void OnSeasonTimeChanged(SeasonTimeInfo info)
		{
		}

		private void OnDisable()
		{
		}

		private void OnUpdateCustomLifeStatus(Life life)
		{
		}

		public void RefreshData(SeasonModel _)
		{
		}

		private void UpdateUiCountDown()
		{
		}

		private void SetNextSeasonMessage(string seasonName, TimeSpan? timeUntilStart)
		{
		}

		private IEnumerator TransitionAnimation(SectionType type)
		{
			return null;
		}

		private bool AnimationCondition()
		{
			return false;
		}

		private void ShowSection(SectionType sectionType)
		{
		}

		private void ShowTheme(THEME theme)
		{
		}

		private bool _003COnItemClicked_003Eb__8_0(ItemSection i)
		{
			return false;
		}

		private void _003CShowTheme_003Eb__20_0(ItemSection i)
		{
		}
	}
}
