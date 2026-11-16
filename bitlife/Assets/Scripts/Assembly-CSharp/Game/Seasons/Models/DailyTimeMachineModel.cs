using System;

namespace Game.Seasons.Models
{
	[Serializable]
	public class DailyTimeMachineModel
	{
		private bool Enabled__BackingField;

		private int? Price__BackingField;

		public bool Enabled
		{
			get
			{
				return Enabled__BackingField;
			}
			set
			{
				Enabled__BackingField = value;
			}
		}

		public int? Price
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
