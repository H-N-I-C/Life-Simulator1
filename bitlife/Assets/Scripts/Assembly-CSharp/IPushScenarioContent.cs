public interface IPushScenarioContent
{
	string ScenarioId { get; }

	string Iap { get; }

	string PlayerFilterCriteriaForScenarioEligibility { get; }

	string GenderBracket { get; }

	string ExteriorTitle { get; }

	string Title { get; }

	string Body { get; }

	string Cta { get; }

	string Choice1 { get; }

	string Narrative1 { get; }

	string Choice2 { get; }

	string Narrative2 { get; }

	string Emoji { get; }

	string Notes { get; }
}
