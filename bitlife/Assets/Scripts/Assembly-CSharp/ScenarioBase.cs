using System;
using System.Collections;
using System.Collections.Generic;
using Game.Genesis.BluePrints;
using Genesis.Controllers;
using Genesis.Entities;

public class ScenarioBase : Scenario
{
	private sealed class DesignerEntityDropdownValue
	{
		private string DisplayValue__BackingField;

		private SimEntity Entity__BackingField;

		public string DisplayValue
		{
			get
			{
				return DisplayValue__BackingField;
			}
			set
			{
				DisplayValue__BackingField = value;
			}
		}

		public SimEntity Entity
		{
			get
			{
				return Entity__BackingField;
			}
			set
			{
				Entity__BackingField = value;
			}
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<float, double> _003C_003E9__23_0;

		public static Func<DesignerTableRowEntry, int> _003C_003E9__23_1;

		public static Func<DesignerBarGraphEntry, int> _003C_003E9__23_2;

		public static Func<DesignerDropdown, bool> _003C_003E9__27_0;

		public static Func<GenesisMasterTags.TagValue, string> _003C_003E9__27_1;

		public static Func<DesignerEntityDropdownValue, string> _003C_003E9__28_0;

		public static Func<ChoiceCurrencyDataItem, bool> _003C_003E9__33_0;

		public static Func<ScenarioAction, bool> _003C_003E9__38_0;

		public static Func<ScenarioAction, bool> _003C_003E9__38_1;

		public static Func<float, double> _003C_003E9__38_2;

		public static Func<ScenarioAction, bool> _003C_003E9__61_0;

		internal double _003CLoadDesignerFormat_003Eb__23_0(float value)
		{
			return 0.0;
		}

		internal int _003CLoadDesignerFormat_003Eb__23_1(DesignerTableRowEntry e)
		{
			return 0;
		}

		internal int _003CLoadDesignerFormat_003Eb__23_2(DesignerBarGraphEntry e)
		{
			return 0;
		}

		internal bool _003CPopulateDesignerDropdowns_003Eb__27_0(DesignerDropdown dropdown)
		{
			return false;
		}

		internal string _003CPopulateDesignerDropdowns_003Eb__27_1(GenesisMasterTags.TagValue item)
		{
			return null;
		}

		internal string _003CPopulateDesignerEntityDropdown_003Eb__28_0(DesignerEntityDropdownValue value)
		{
			return null;
		}

		internal bool _003CParseDesignerChoiceCurrencyData_003Eb__33_0(ChoiceCurrencyDataItem t)
		{
			return false;
		}

		internal bool _003CLoadLegacyFormat_003Eb__38_0(ScenarioAction x)
		{
			return false;
		}

		internal bool _003CLoadLegacyFormat_003Eb__38_1(ScenarioAction x)
		{
			return false;
		}

		internal double _003CLoadLegacyFormat_003Eb__38_2(float value)
		{
			return 0.0;
		}

		internal bool _003CDismissSelected_003Eb__61_0(ScenarioAction t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass48_0
	{
		public int choiceIndex;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(ScenarioAction t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass57_0
	{
		public int choiceIndex;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(ScenarioAction t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass57_1
	{
		public string value;

		internal bool _003CChoiceSelectedAtIndex_003Eb__1(DesignerEntityDropdownValue item)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__3(GenesisMasterTags.TagValue tv)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__4(string s)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__2(string s)
		{
			return false;
		}
	}

	private readonly ScenarioDefinitionModel _model;

	private string _narrative;

	private bool IsValidForPresentation__BackingField;

	private Action<string, Scenario> _giveItemCallback;

	private ScenarioController _owningController;

	private string _overrideTitle;

	private string _overrideBody;

	private Dictionary<string, List<GenesisMasterTags.TagValue>> _designerDropdownValues;

	private Dictionary<string, List<DesignerEntityDropdownValue>> _designerEntityDropdownValues;

	private GenesisEntityTransientMemoryScope _designerEntityDropdownTransientScope;

	private SimPerson _designerEntityDropdownPriorPerson;

	private SimPerson _designerEntityDropdownSelectedPerson;

	private bool _designerEntityDropdownSelectionIsActive;

	public bool IsValidForPresentation
	{
		get
		{
			return IsValidForPresentation__BackingField;
		}
		private set
		{
			IsValidForPresentation__BackingField = value;
		}
	}

	public ScenarioController Controller => null;

	public ScenarioBase(ScenarioDefinitionModel model)
	{
	}

	public ScenarioBase(ScenarioController controller, string targetID)
	{
	}

	public void Load(ScenarioController controller, string targetID)
	{
	}

	public void SetPresentationOverride(string title, string body)
	{
	}

	private void LoadDesignerFormat(ScenarioCSVData csvData, string targetID)
	{
	}

	private bool TryApplyDesignerPresentationFocusSetup(string[] row, string targetID)
	{
		return false;
	}

	private bool TryApplyDesignerObjectDataFocus(string[] row, string targetID)
	{
		return false;
	}

	private object ResolveDesignerFocusObject(string authoredReference)
	{
		return null;
	}

	private void PopulateDesignerDropdowns(string json)
	{
	}

	private void PopulateDesignerEntityDropdown(DesignerDropdown dropdown, int dropdownOrdinal)
	{
	}

	private void InvalidateDesignerEntityDropdown(string reason)
	{
	}

	private void StoreDropdownTagValues(string tagId, List<GenesisMasterTags.TagValue> values)
	{
	}

	private ScenarioCondition CreateDesignerScenarioCondition(string scenarioId)
	{
		return null;
	}

	private Dictionary<string, BluePrintData> ParseDesignerBluePrintPayload(string rawPayload)
	{
		return null;
	}

	private List<ChoiceCurrencyDataItem> ParseDesignerChoiceCurrencyData(string rawChoiceCurrencyCost, List<int> visibleChoiceNumbers)
	{
		return null;
	}

	private static List<ChoiceCurrencyDataItem> BuildEmptyChoiceCurrencyData(int count)
	{
		return null;
	}

	private string ParseChoiceCurrencyField(string value)
	{
		return null;
	}

	private static bool ParseDesignerBool(string value)
	{
		return false;
	}

	private void AddDesignerBluePrintPayload(Dictionary<string, BluePrintData> payloadMap, DesignerBlueprintPayloadForRoute payloadForRoute)
	{
	}

	private void LoadLegacyFormat(ScenarioCSVData csvData, string targetID)
	{
	}

	private string CheckButtonType(string buttonTitle, out bool isDefault, out CHOICE_BUTTON_ICON buttonType)
	{
		isDefault = default;
		buttonType = default;
		return null;
	}

	private static string GetDesignerCol(string[] row, DesignerCSVHeaders col)
	{
		return null;
	}

	private static string GetDesignerColByIndex(string[] row, int index)
	{
		return null;
	}

	private bool IsMoneySliderCurrencyId(int sliderIndex)
	{
		return false;
	}

	private bool IsIndependentMoneySlider(int sliderIndex)
	{
		return false;
	}

	private long? GetExactMoneySliderValue(int sliderIndex, object submittedValue)
	{
		return null;
	}

	private string CheckDesignerButtonStyle(string text, string style, out bool isDefault, out CHOICE_BUTTON_ICON buttonType)
	{
		isDefault = default;
		buttonType = default;
		return null;
	}

	private void InjectFilters(string filterString, string targetID)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void ProcessCurrencyImpact(int index)
	{
	}

	private static string NormalizeCurrencyCostAmount(string costAmount)
	{
		return null;
	}

	private GenesisDataEntity GetFocusedPooledSliderEntity()
	{
		return null;
	}

	private void ApplyPooledSliderScores(ArrayList sliders)
	{
	}

	private void ApplyPooledSliderOutcome(ArrayList sliders)
	{
	}

	private void ApplyResultData(DesignerChoiceData choiceData)
	{
	}

	private string ResolveDesignerLogLine(string template)
	{
		return null;
	}

	private void CallGiveBack(string command)
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliders)
	{
	}

	private bool TryApplyDesignerEntityDropdownSelection(DesignerEntityDropdownValue selected)
	{
		return false;
	}

	private void ClearDesignerEntityDropdownSelection()
	{
	}

	private void ReleaseDesignerEntityDropdownSelection()
	{
	}

	public override void DismissSelected()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private static bool TrySplitSliderDefinition(string sliderValue, out string sliderId, out string sliderRange)
	{
		sliderId = null;
		sliderRange = null;
		return false;
	}

	private static string[] SplitSliderRangeValues(string sliderRange)
	{
		return null;
	}

	private static string[] SplitTopLevel(string value, char splitter)
	{
		return null;
	}

	private bool IsDesignerTableRowVisible(string authoredCondition)
	{
		return false;
	}

	private bool TryResolveDesignerTableRowValue(DesignerTableRowEntry entry, out string resolvedValue)
	{
		resolvedValue = null;
		return false;
	}

	private string ParseDesignerTableValueForFallback(string text, out bool isBlank)
	{
		isBlank = default;
		return null;
	}

	private static bool IsBlankDesignerTableValue(string text)
	{
		return false;
	}

	private string ParseGenesisAndBody(string text)
	{
		return null;
	}

	private string ParseGenesisAndNarrative(string text)
	{
		return null;
	}

	private string _003CRefreshScenario_003Eb__47_0(string t)
	{
		return null;
	}

	private string _003CRefreshScenario_003Eb__47_1(string t)
	{
		return null;
	}
}
