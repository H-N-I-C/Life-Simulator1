namespace Game.Genesis.BluePrints
{
	public interface IBluePrint
	{
		BluePrintType BluePrintType { get; set; }

		void Setup(BluePrintData data);

		bool ConditionSelected();

		Scenario GetNextScenario(string followupScenario);
	}
}
