namespace Backend.Code.Models.Catalog
{
	public class Price
	{
		private int Amount__BackingField;

		private string Currency__BackingField;

		public int Amount
		{
			get
			{
				return Amount__BackingField;
			}
			set
			{
				Amount__BackingField = value;
			}
		}

		public string Currency
		{
			get
			{
				return Currency__BackingField;
			}
			set
			{
				Currency__BackingField = value;
			}
		}
	}
}
