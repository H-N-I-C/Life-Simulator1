using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using Game.Seasons.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklySeasonsCommunityGoal : UIDarkly
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<int, Toggle, CommunityGoalDataModel> _003C_003E9__8_0;

			internal void _003CSetData_003Eb__8_0(int index, Toggle item, CommunityGoalDataModel data)
			{
			}
		}

		private sealed class _003CSetData_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklySeasonsCommunityGoal _003C_003E4__this;

			public SeasonModel data;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSetData_003Ed__8(int _003C_003E1__state)
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

		private Toggle itemTemplate;

		private GameObject stack;

		private TextMeshProUGUI infoDaysRemaining;

		private TextMeshProUGUI goalsCompletion;

		private CandyButton buttonCommunity;

		private List<Toggle> items;

		protected override void Awake()
		{
		}

		private void OnCommunityButtonClicked()
		{
		}

		public IEnumerator SetData(SeasonModel data)
		{
			return null;
		}
	}
}
