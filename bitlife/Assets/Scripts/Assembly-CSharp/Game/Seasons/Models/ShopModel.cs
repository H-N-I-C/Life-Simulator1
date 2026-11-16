using System.Collections.Generic;

namespace Game.Seasons.Models
{
	public class ShopModel
	{
		public string Title;

		private List<ShopListItemModel> ShopList__BackingField;

		public List<ShopListItemModel> ShopList
		{
			get
			{
				return ShopList__BackingField;
			}
			set
			{
				ShopList__BackingField = value;
			}
		}
	}
}
