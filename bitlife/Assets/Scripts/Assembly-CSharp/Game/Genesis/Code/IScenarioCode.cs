namespace Game.Genesis.Code
{
	public interface IScenarioCode
	{
		string GetStringResult();

		Scenario GetScenarioResult();

		void Run();
	}
}
