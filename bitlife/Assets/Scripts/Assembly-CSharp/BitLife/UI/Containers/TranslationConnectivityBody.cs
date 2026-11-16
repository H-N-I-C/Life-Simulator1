using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public sealed class TranslationConnectivityBody : MonoBehaviour, IScenarioComponentEvent
	{
		public const string EventKey = "translation-connectivity";

		public const string Placeholder = "\u200b";

		private const string OfflineMessage = "You're offline. Some text may appear in English until you reconnect.";

		private ScenarioComponentEvent ScenarioEventData__BackingField;

		private CandyText label;

		private UIScenarioCard card;

		private RectTransform bodyNode;

		private Transform leadingSpacer;

		public ScenarioComponentEvent ScenarioEventData
		{
			get
			{
				return ScenarioEventData__BackingField;
			}
			private set
			{
				ScenarioEventData__BackingField = value;
			}
		}

		public void SetScenarioEventData(ScenarioComponentEvent data)
		{
		}

		private void OnConnectivityChanged()
		{
		}

		private void UpdateBody(bool refreshLayout)
		{
		}

		private void RefreshLayout()
		{
		}

		private void OnDestroy()
		{
		}
	}
}
