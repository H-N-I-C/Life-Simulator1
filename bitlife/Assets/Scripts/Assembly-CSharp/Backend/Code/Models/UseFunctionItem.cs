using System;
using Game;

namespace Backend.Code.Models
{
	public class UseFunctionItem : IMyStuffUseCheck
	{
		private const string Pets = "pets";

		private const string Parents = "parents";

		private const string Fame = "fame";

		private const string Lover = "lover";

		private const string VampireEssenceFull = "vampire_essence_full";

		private const string ActorReputationFull = "actor_reputation_full";

		private const string MusicianPopularityFull = "musician_popularity_full";

		private const string CultCharismaFull = "cult_charisma_full";

		private const string ProathleteFull = "proathlete_full";

		private readonly MyStuffFunction _function;

		private StuffItem Item__BackingField;

		public bool RequiresScenario => false;

		public StuffItem Item
		{
			get
			{
				return Item__BackingField;
			}
			set
			{
				Item__BackingField = value;
			}
		}

		public UseFunctionItem(MyStuffFunction functionIntValue)
		{
		}

		public void PreDownload(Action<bool> callback)
		{
		}

		public bool CanExecute()
		{
			return false;
		}

		public void ExecutePowerUp()
		{
		}

		public int GetLastIntResult()
		{
			return 0;
		}

		public string GetErrorMessage()
		{
			return null;
		}

		private string GtStatsPotionErrorMessage(string functionType)
		{
			return null;
		}

		private bool CheckFunction(string functionType)
		{
			return false;
		}
	}
}
