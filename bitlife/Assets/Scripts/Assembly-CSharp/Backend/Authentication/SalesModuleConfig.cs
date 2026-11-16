namespace Backend.Authentication
{
	public class SalesModuleConfig
	{
		private IAP_TYPE IapType__BackingField;

		private float Discount__BackingField;

		private bool IsWebShop__BackingField;

		public IAP_TYPE IapType
		{
			get
			{
				return IapType__BackingField;
			}
			set
			{
				IapType__BackingField = value;
			}
		}

		public float Discount
		{
			get
			{
				return Discount__BackingField;
			}
			set
			{
				Discount__BackingField = value;
			}
		}

		public bool IsWebShop
		{
			get
			{
				return IsWebShop__BackingField;
			}
			set
			{
				IsWebShop__BackingField = value;
			}
		}
	}
}
