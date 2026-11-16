using System.Collections.Generic;
using Game.Genesis.BluePrints;

namespace Genesis.Controllers
{
	public class ScenarioDefinitionModel
	{
		private string Title__BackingField;

		private string Body__BackingField;

		private string ExteriorTitle__BackingField;

		private bool HasDismissButton__BackingField;

		private bool FeedbackStyleScenario__BackingField;

		private string FeedbackScenarioId__BackingField;

		private string CustomSoundFileName__BackingField;

		private string Narrative__BackingField;

		private List<ScenarioChoice> ChoiceList__BackingField;

		private List<ScenarioAction> ActionList__BackingField;

		private string EmojiString__BackingField;

		private Dictionary<string, List<string>> InputList__BackingField;

		private List<TitleAndContent<string>> DropDownList__BackingField;

		private List<TitleAndContent<float>> SliderList__BackingField;

		private List<List<double>> SliderExactValues__BackingField;

		private List<string> SliderCurrencyIds__BackingField;

		private List<string> SliderPoolIds__BackingField;

		private List<double> SliderPoolTotals__BackingField;

		private List<string> SliderScoreFields__BackingField;

		private List<string> SliderIpFields__BackingField;

		private List<string> SliderPmFields__BackingField;

		private List<string> SliderCategoryIds__BackingField;

		private List<string> SliderMatchAgainstFields__BackingField;

		private List<string> SliderFeedbackReferences__BackingField;

		private List<string> SliderFeedbackCategoryFields__BackingField;

		private Dictionary<string, string> TableList__BackingField;

		private Dictionary<string, float> BarGraphList__BackingField;

		private Dictionary<string, BAR_GRAPH_COLOR_SCHEME> BarGraphStyles__BackingField;

		private int ColorScheme__BackingField;

		private string[] ImpactHeroChoices__BackingField;

		private string[] ResultNarratives__BackingField;

		private string GiveItem__BackingField;

		private object FocusObject__BackingField;

		private string BigImageString__BackingField;

		private List<string> ChoiceImpacts__BackingField;

		private List<string> ChoiceLogLine1__BackingField;

		private List<string> ChoiceLogLine2__BackingField;

		private string RewardsGranted__BackingField;

		private List<DesignerChoiceData> DesignerChoiceDataList__BackingField;

		private List<ChoiceCurrencyDataItem> ChoiceCurrencyData__BackingField;

		private Dictionary<string, BluePrintData> BluePrintPayload__BackingField;

		private bool IsAgeUpScenario__BackingField;

		private AgeUpPlacement AgeUpPlacement__BackingField;

		private bool DisallowRandomSelection__BackingField;

		public string Title
		{
			get
			{
				return Title__BackingField;
			}
			set
			{
				Title__BackingField = value;
			}
		}

		public string Body
		{
			get
			{
				return Body__BackingField;
			}
			set
			{
				Body__BackingField = value;
			}
		}

		public string ExteriorTitle
		{
			get
			{
				return ExteriorTitle__BackingField;
			}
			set
			{
				ExteriorTitle__BackingField = value;
			}
		}

		public bool HasDismissButton
		{
			get
			{
				return HasDismissButton__BackingField;
			}
			set
			{
				HasDismissButton__BackingField = value;
			}
		}

		public bool FeedbackStyleScenario
		{
			get
			{
				return FeedbackStyleScenario__BackingField;
			}
			set
			{
				FeedbackStyleScenario__BackingField = value;
			}
		}

		public string FeedbackScenarioId
		{
			get
			{
				return FeedbackScenarioId__BackingField;
			}
			set
			{
				FeedbackScenarioId__BackingField = value;
			}
		}

		public string CustomSoundFileName
		{
			get
			{
				return CustomSoundFileName__BackingField;
			}
			set
			{
				CustomSoundFileName__BackingField = value;
			}
		}

		public string Narrative
		{
			get
			{
				return Narrative__BackingField;
			}
			set
			{
				Narrative__BackingField = value;
			}
		}

		public List<ScenarioChoice> ChoiceList
		{
			get
			{
				return ChoiceList__BackingField;
			}
			set
			{
				ChoiceList__BackingField = value;
			}
		}

		public List<ScenarioAction> ActionList
		{
			get
			{
				return ActionList__BackingField;
			}
			set
			{
				ActionList__BackingField = value;
			}
		}

		public string EmojiString
		{
			get
			{
				return EmojiString__BackingField;
			}
			set
			{
				EmojiString__BackingField = value;
			}
		}

		public Dictionary<string, List<string>> InputList
		{
			get
			{
				return InputList__BackingField;
			}
			set
			{
				InputList__BackingField = value;
			}
		}

		public List<TitleAndContent<string>> DropDownList
		{
			get
			{
				return DropDownList__BackingField;
			}
			set
			{
				DropDownList__BackingField = value;
			}
		}

		public List<TitleAndContent<float>> SliderList
		{
			get
			{
				return SliderList__BackingField;
			}
			set
			{
				SliderList__BackingField = value;
			}
		}

		public List<List<double>> SliderExactValues
		{
			get
			{
				return SliderExactValues__BackingField;
			}
			set
			{
				SliderExactValues__BackingField = value;
			}
		}

		public List<string> SliderCurrencyIds
		{
			get
			{
				return SliderCurrencyIds__BackingField;
			}
			set
			{
				SliderCurrencyIds__BackingField = value;
			}
		}

		public List<string> SliderPoolIds
		{
			get
			{
				return SliderPoolIds__BackingField;
			}
			set
			{
				SliderPoolIds__BackingField = value;
			}
		}

		public List<double> SliderPoolTotals
		{
			get
			{
				return SliderPoolTotals__BackingField;
			}
			set
			{
				SliderPoolTotals__BackingField = value;
			}
		}

		public List<string> SliderScoreFields
		{
			get
			{
				return SliderScoreFields__BackingField;
			}
			set
			{
				SliderScoreFields__BackingField = value;
			}
		}

		public List<string> SliderIpFields
		{
			get
			{
				return SliderIpFields__BackingField;
			}
			set
			{
				SliderIpFields__BackingField = value;
			}
		}

		public List<string> SliderPmFields
		{
			get
			{
				return SliderPmFields__BackingField;
			}
			set
			{
				SliderPmFields__BackingField = value;
			}
		}

		public List<string> SliderCategoryIds
		{
			get
			{
				return SliderCategoryIds__BackingField;
			}
			set
			{
				SliderCategoryIds__BackingField = value;
			}
		}

		public List<string> SliderMatchAgainstFields
		{
			get
			{
				return SliderMatchAgainstFields__BackingField;
			}
			set
			{
				SliderMatchAgainstFields__BackingField = value;
			}
		}

		public List<string> SliderFeedbackReferences
		{
			get
			{
				return SliderFeedbackReferences__BackingField;
			}
			set
			{
				SliderFeedbackReferences__BackingField = value;
			}
		}

		public List<string> SliderFeedbackCategoryFields
		{
			get
			{
				return SliderFeedbackCategoryFields__BackingField;
			}
			set
			{
				SliderFeedbackCategoryFields__BackingField = value;
			}
		}

		public Dictionary<string, string> TableList
		{
			get
			{
				return TableList__BackingField;
			}
			set
			{
				TableList__BackingField = value;
			}
		}

		public Dictionary<string, float> BarGraphList
		{
			get
			{
				return BarGraphList__BackingField;
			}
			set
			{
				BarGraphList__BackingField = value;
			}
		}

		public Dictionary<string, BAR_GRAPH_COLOR_SCHEME> BarGraphStyles
		{
			get
			{
				return BarGraphStyles__BackingField;
			}
			set
			{
				BarGraphStyles__BackingField = value;
			}
		}

		public int ColorScheme
		{
			get
			{
				return ColorScheme__BackingField;
			}
			set
			{
				ColorScheme__BackingField = value;
			}
		}

		public string[] ImpactHeroChoices
		{
			get
			{
				return ImpactHeroChoices__BackingField;
			}
			set
			{
				ImpactHeroChoices__BackingField = value;
			}
		}

		public string[] ResultNarratives
		{
			get
			{
				return ResultNarratives__BackingField;
			}
			set
			{
				ResultNarratives__BackingField = value;
			}
		}

		public string GiveItem
		{
			get
			{
				return GiveItem__BackingField;
			}
			set
			{
				GiveItem__BackingField = value;
			}
		}

		public object FocusObject
		{
			get
			{
				return FocusObject__BackingField;
			}
			set
			{
				FocusObject__BackingField = value;
			}
		}

		public string BigImageString
		{
			get
			{
				return BigImageString__BackingField;
			}
			set
			{
				BigImageString__BackingField = value;
			}
		}

		public List<string> ChoiceImpacts
		{
			get
			{
				return ChoiceImpacts__BackingField;
			}
			set
			{
				ChoiceImpacts__BackingField = value;
			}
		}

		public List<string> ChoiceLogLine1
		{
			get
			{
				return ChoiceLogLine1__BackingField;
			}
			set
			{
				ChoiceLogLine1__BackingField = value;
			}
		}

		public List<string> ChoiceLogLine2
		{
			get
			{
				return ChoiceLogLine2__BackingField;
			}
			set
			{
				ChoiceLogLine2__BackingField = value;
			}
		}

		public string RewardsGranted
		{
			get
			{
				return RewardsGranted__BackingField;
			}
			set
			{
				RewardsGranted__BackingField = value;
			}
		}

		public List<DesignerChoiceData> DesignerChoiceDataList
		{
			get
			{
				return DesignerChoiceDataList__BackingField;
			}
			set
			{
				DesignerChoiceDataList__BackingField = value;
			}
		}

		public List<ChoiceCurrencyDataItem> ChoiceCurrencyData
		{
			get
			{
				return ChoiceCurrencyData__BackingField;
			}
			set
			{
				ChoiceCurrencyData__BackingField = value;
			}
		}

		public Dictionary<string, BluePrintData> BluePrintPayload
		{
			get
			{
				return BluePrintPayload__BackingField;
			}
			set
			{
				BluePrintPayload__BackingField = value;
			}
		}

		public bool IsAgeUpScenario
		{
			get
			{
				return IsAgeUpScenario__BackingField;
			}
			set
			{
				IsAgeUpScenario__BackingField = value;
			}
		}

		public AgeUpPlacement AgeUpPlacement
		{
			get
			{
				return AgeUpPlacement__BackingField;
			}
			set
			{
				AgeUpPlacement__BackingField = value;
			}
		}

		public bool DisallowRandomSelection
		{
			get
			{
				return DisallowRandomSelection__BackingField;
			}
			set
			{
				DisallowRandomSelection__BackingField = value;
			}
		}
	}
}
