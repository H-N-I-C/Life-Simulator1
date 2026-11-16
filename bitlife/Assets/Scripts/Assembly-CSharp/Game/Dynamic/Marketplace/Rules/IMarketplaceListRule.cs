using System.Collections.Generic;

namespace Game.Dynamic.Marketplace.Rules
{
	public interface IMarketplaceListRule
	{
		void Apply(List<MarketPlaceEntry> list);
	}
}
