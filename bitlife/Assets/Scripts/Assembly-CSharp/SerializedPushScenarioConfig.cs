using System;
using System.Collections.Generic;

[Serializable]
public class SerializedPushScenarioConfig : IPushScenarioConfig
{
	public List<SerializedPushScenarioConfigEntry> ConfigEntries;

	public List<SerializedPushScenarioCategoryDraw> CategoryDrawEntries;

	public List<SerializedPushScenarioSubcategoryDraw> SubcategoryDrawEntries;

	public List<SerializedPushScenarioContent> ContentEntries;

	public IEnumerable<IPushScenarioConfigEntry> Entries => null;

	public IEnumerable<IPushScenarioCategoryDraw> CategoriesToDraw => null;

	public IEnumerable<IPushScenarioSubcategoryDraw> SubCategoriesToDraw => null;

	public IEnumerable<IPushScenarioContent> Contents => null;
}
