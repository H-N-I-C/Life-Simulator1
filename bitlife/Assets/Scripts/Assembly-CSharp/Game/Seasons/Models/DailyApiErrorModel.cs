using System;

namespace Game.Seasons.Models
{
	[Serializable]
	internal class DailyApiErrorModel
	{
		private string ErrorCode__BackingField;

		public string ErrorCode
		{
			get
			{
				return ErrorCode__BackingField;
			}
			set
			{
				ErrorCode__BackingField = value;
			}
		}
	}
}
