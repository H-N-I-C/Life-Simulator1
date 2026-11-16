namespace Backend.Code.Models.FunctionModels
{
	public class MyStuffPotionFunction : MyStuffFunction
	{
		private const string VampireEssenceFull = "vampire_essence_full";

		private const string ActorReputationFull = "actor_reputation_full";

		private const string MusicianPopularityFull = "musician_popularity_full";

		private const string CultCharismaFull = "cult_charisma_full";

		private const string ProathleteFull = "proathlete_full";

		private const string Lover = "lover";

		private const string Health = "health";

		private const string Happy = "happy";

		private const string Looks = "looks";

		private const string Smarts = "smarts";

		private const string All = "all";

		private const string Pets = "pets";

		private const string Parents = "parents";

		private const string Fame = "fame";

		public override void ExecuteWithData(string data)
		{
		}

		private T GetSimJob<T>() where T : SimSpecialJob
		{
			return null;
		}
	}
}
