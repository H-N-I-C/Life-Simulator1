namespace Backend.Code.Models.Catalog
{
	public class ProductItem
	{
		private string Id__BackingField;

		private Price Price__BackingField;

		public string Id
		{
			get
			{
				return Id__BackingField;
			}
			set
			{
				Id__BackingField = value;
			}
		}

		public Price Price
		{
			get
			{
				return Price__BackingField;
			}
			set
			{
				Price__BackingField = value;
			}
		}
	}
}
