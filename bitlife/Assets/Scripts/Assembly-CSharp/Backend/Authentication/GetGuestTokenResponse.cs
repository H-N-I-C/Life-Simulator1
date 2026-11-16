namespace Backend.Authentication
{
	public class GetGuestTokenResponse
	{
		private string IdentityId__BackingField;

		private string Token__BackingField;

		public string IdentityId
		{
			get
			{
				return IdentityId__BackingField;
			}
			set
			{
				IdentityId__BackingField = value;
			}
		}

		public string Token
		{
			get
			{
				return Token__BackingField;
			}
			set
			{
				Token__BackingField = value;
			}
		}
	}
}
