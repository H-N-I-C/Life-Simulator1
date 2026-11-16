using System;
using System.Collections.Generic;

namespace Game.Dynamic.Marketplace.Rules
{
	public class MarketplaceRuleFighter : IMarketplaceListRule
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<MarketPlaceEntry, bool> _003C_003E9__0_0;

			internal bool _003CApply_003Eb__0_0(MarketPlaceEntry t)
			{
				return false;
			}
		}

		public void Apply(List<MarketPlaceEntry> list)
		{
		}
	}
}
