using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.ScenarioObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIGravestone : MonoBehaviour
	{
		private sealed class _003C_003Ec__DisplayClass23_0
		{
			public string activeLanguageAtRequest;

			public Grave data;

			public UIGravestone _003C_003E4__this;

			public List<TranslationEntry> translationEntries;

			internal void _003CSetupGravestone_003Eb__0(List<string> result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass31_0
		{
			public string activeLanguageAtRequest;

			public UIGravestone _003C_003E4__this;

			public Grave data;

			public List<(string, string, bool)> rows;

			internal void _003CSetupTable_003Eb__0(List<string> results)
			{
			}
		}

		private const float TABLE_VERTICAL_INSET = 0f;

		private const float TABLE_HORIZONTAL_INSET = 60f;

		private const float SHOW_DURATION = 0.2f;

		private RectTransform rect;

		private CandyImage background;

		private CandyButton dismiss;

		private CandyImage sash;

		private CandyButton shareButton;

		private CandyButton bulldozeButton;

		private new CandyText name;

		private CandyText date;

		private CandyScroller scroller;

		private CandyTable table;

		private GameObject tableToObitSpace;

		private CandyText obituary;

		private GameObject obitToBarSpace;

		private UIScenarioBarGraphs barGraphs;

		private CandyButton journalButton;

		private CandyImage grass;

		private RectTransform contentRect;

		public Grave data;

		private Color fontColor;

		private void OnDisable()
		{
		}

		public void SetupGravestone(Grave data, bool doesIncludeJournal, bool doesIncludeBulldozer)
		{
		}

		private void Update()
		{
		}

		private List<TranslationEntry> GetTranslationEntries(Grave data)
		{
			return null;
		}

		private void SetupHeight()
		{
		}

		public void SetupDeathLog(Grave data)
		{
		}

		private void SetupBarGraphs(Grave data)
		{
		}

		private void SetupBarGraphsFromScratch(Grave data)
		{
		}

		private void SetupObituary(Grave data)
		{
		}

		private void SetupTable(Grave data)
		{
		}

		public void OpenJournal()
		{
		}

		public void ExitGrave()
		{
		}

		public void Share()
		{
		}

		public void Bulldoze()
		{
		}

		private void _003CBulldoze_003Eb__35_0(int index)
		{
		}
	}
}
