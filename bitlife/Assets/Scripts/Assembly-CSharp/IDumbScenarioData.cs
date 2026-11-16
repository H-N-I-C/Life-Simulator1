using System.Collections.Generic;

public interface IDumbScenarioData
{
	bool RandomizeChoices { get; }

	string GenderBracket { get; }

	string Title { get; }

	string Body { get; }

	string CTA { get; }

	IReadOnlyList<IDumbChoice> Choices { get; }
}
