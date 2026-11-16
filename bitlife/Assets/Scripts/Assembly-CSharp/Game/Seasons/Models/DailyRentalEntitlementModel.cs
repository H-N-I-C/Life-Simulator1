using System;

namespace Game.Seasons.Models
{
	[Serializable]
	public class DailyRentalEntitlementModel
	{
		private string ItemId__BackingField;

		private string WalletItemId__BackingField;

		private string CanonicalItemId__BackingField;

		private string RentalWindowId__BackingField;

		private string Source__BackingField;

		private string ExpiresAt__BackingField;

		private string StartedAt__BackingField;

		private bool IsDebug__BackingField;

		public string ItemId
		{
			get
			{
				return ItemId__BackingField;
			}
			set
			{
				ItemId__BackingField = value;
			}
		}

		public string WalletItemId
		{
			get
			{
				return WalletItemId__BackingField;
			}
			set
			{
				WalletItemId__BackingField = value;
			}
		}

		public string CanonicalItemId
		{
			get
			{
				return CanonicalItemId__BackingField;
			}
			set
			{
				CanonicalItemId__BackingField = value;
			}
		}

		public string RentalWindowId
		{
			get
			{
				return RentalWindowId__BackingField;
			}
			set
			{
				RentalWindowId__BackingField = value;
			}
		}

		public string Source
		{
			get
			{
				return Source__BackingField;
			}
			set
			{
				Source__BackingField = value;
			}
		}

		public string ExpiresAt
		{
			get
			{
				return ExpiresAt__BackingField;
			}
			set
			{
				ExpiresAt__BackingField = value;
			}
		}

		public string StartedAt
		{
			get
			{
				return StartedAt__BackingField;
			}
			set
			{
				StartedAt__BackingField = value;
			}
		}

		public bool IsDebug
		{
			get
			{
				return IsDebug__BackingField;
			}
			set
			{
				IsDebug__BackingField = value;
			}
		}
	}
}
