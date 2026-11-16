using System.Collections.Generic;

namespace Tracking
{
	public class TempBitLife_Ufm_WinLossResult : TempBitLifeUfm
	{
		private string Result__BackingField;

		public string Result
		{
			get
			{
				return Result__BackingField;
			}
			set
			{
				Result__BackingField = value;
			}
		}

		public static bool TrySetResult<T>(FIGHT_CLUB_FIGHTING_MINI_GAME_OUTCOME outcome, out T result) where T : TempBitLife_Ufm_WinLossResult, new()
		{
			result = null;
			return false;
		}

		public override Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
