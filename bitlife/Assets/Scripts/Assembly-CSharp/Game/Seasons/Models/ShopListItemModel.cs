namespace Game.Seasons.Models
{
	public class ShopListItemModel
	{
		public string ItemId;

		public string Title;

		public string Subtitle;

		public string Emoji;

		public int Cost;

		public string EventId;

		public string ItemType;

		public string TimeString;

		public bool IsDivider;

		public string GetEmojiString()
		{
			return null;
		}

		public string GetTimeLocalEmojiString()
		{
			return null;
		}

		public int GetCostForTier(int tier = 0)
		{
			return 0;
		}
	}
}
