namespace Game.Genesis.BluePrints
{
	public class BluePrintSendHeroToPrison : BluePrintHeroPrisonRouteBase
	{
		private string _securityLevel;

		public override void Setup(BluePrintData data)
		{
		}

		public override Scenario GetNextScenario(string followupScenario)
		{
			return null;
		}
	}
}
