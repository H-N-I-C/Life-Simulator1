using System.Collections.Generic;

public class PushScenarioCollection
{
	private readonly List<IPushScenarioConfigEntry> _configEntries;

	private readonly Dictionary<string, List<IPushScenarioCategoryDraw>> _configurationToCategoryDraw;

	private readonly Dictionary<string, List<IPushScenarioSubcategoryDraw>> _subCategoryToSubCategoryDraw;

	private readonly Dictionary<string, List<IPushScenarioContent>> _subCategoryToContent;

	private readonly IPushScenarioConfig PushScenarioConfig__BackingField;

	public IPushScenarioConfig PushScenarioConfig => PushScenarioConfig__BackingField;

	public PushScenarioCollection(IPushScenarioConfig config)
	{
	}

	private void BuildConfigurationToDrawCategoryDictionary()
	{
	}

	private void BuildCategoryDrawToSubCategoryDictionary()
	{
	}

	private void BuildSubCategoryDrawToScenarioContentDictionary()
	{
	}

	public List<IPushScenarioCategoryDraw> GetCategoryDrawList(string categoryName)
	{
		return null;
	}

	public List<IPushScenarioSubcategoryDraw> GetSubCategoryDrawList(string categoryName)
	{
		return null;
	}

	public bool TryGetContentByIDFromCategory(string category, string categoryID, out IPushScenarioContent content)
	{
		content = null;
		return false;
	}

	public List<IPushScenarioContent> GetContentList(string subCategoryName)
	{
		return null;
	}
}
