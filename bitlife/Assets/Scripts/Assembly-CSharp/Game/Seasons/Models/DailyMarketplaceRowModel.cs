namespace Game.Seasons.Models
{
	public sealed class DailyMarketplaceRowModel
	{
		private readonly DailyMarketplaceOfferModel Offer__BackingField;

		private readonly bool IsOwnedRow__BackingField;

		public DailyMarketplaceOfferModel Offer => Offer__BackingField;

		public bool IsOwnedRow => IsOwnedRow__BackingField;

		public DailyMarketplaceRowModel(DailyMarketplaceOfferModel offer, bool isOwnedRow)
		{
		}
	}
}
