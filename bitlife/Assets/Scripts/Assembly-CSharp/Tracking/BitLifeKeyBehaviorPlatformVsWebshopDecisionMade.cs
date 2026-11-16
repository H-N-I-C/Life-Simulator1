using System.Collections.Generic;

namespace Tracking
{
	public class BitLifeKeyBehaviorPlatformVsWebshopDecisionMade : IGameTelemetryEvent
	{
		private WebShopTypeReceived PlayerDecision__BackingField;

		private readonly string PlayerExperience__BackingField;

		public WebShopTypeReceived PlayerDecision
		{
			get
			{
				return PlayerDecision__BackingField;
			}
			set
			{
				PlayerDecision__BackingField = value;
			}
		}

		public string PlayerExperience => PlayerExperience__BackingField;

		private static string GetPlayerExperience()
		{
			return null;
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
