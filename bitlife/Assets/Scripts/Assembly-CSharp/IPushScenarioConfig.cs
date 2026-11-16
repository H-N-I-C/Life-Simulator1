using System.Collections.Generic;

public interface IPushScenarioConfig
{
	IEnumerable<IPushScenarioConfigEntry> Entries { get; }

	IEnumerable<IPushScenarioCategoryDraw> CategoriesToDraw { get; }

	IEnumerable<IPushScenarioSubcategoryDraw> SubCategoriesToDraw { get; }

	IEnumerable<IPushScenarioContent> Contents { get; }
}
