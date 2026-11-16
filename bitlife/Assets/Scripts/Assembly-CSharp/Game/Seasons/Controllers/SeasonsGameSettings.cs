using System;
using Backend.Authentication;

namespace Game.Seasons.Controllers
{
	public class SeasonsGameSettings
	{
		private static Action OnGameSettingsAvailable__BackingField;

		private string ActiveSeasonId__BackingField;

		private string NextSeasonId__BackingField;

		private string NextSeasonMessage__BackingField;

		private string NextSeasonNameLong__BackingField;

		private string NextSeasonName__BackingField;

		private string SeasonName__BackingField;

		private string SeasonNameLong__BackingField;

		private bool SeasonsControl__BackingField;

		private bool IsReady__BackingField;

		private int CoolDownMessages__BackingField;

		public static Action OnGameSettingsAvailable
		{
			get
			{
				return OnGameSettingsAvailable__BackingField;
			}
			set
			{
				OnGameSettingsAvailable__BackingField = value;
			}
		}

		public string ActiveSeasonId
		{
			get
			{
				return ActiveSeasonId__BackingField;
			}
			private set
			{
				ActiveSeasonId__BackingField = value;
			}
		}

		public string NextSeasonId
		{
			get
			{
				return NextSeasonId__BackingField;
			}
			private set
			{
				NextSeasonId__BackingField = value;
			}
		}

		public string NextSeasonMessage
		{
			get
			{
				return NextSeasonMessage__BackingField;
			}
			private set
			{
				NextSeasonMessage__BackingField = value;
			}
		}

		public string NextSeasonNameLong
		{
			get
			{
				return NextSeasonNameLong__BackingField;
			}
			private set
			{
				NextSeasonNameLong__BackingField = value;
			}
		}

		public string NextSeasonName
		{
			get
			{
				return NextSeasonName__BackingField;
			}
			private set
			{
				NextSeasonName__BackingField = value;
			}
		}

		public string SeasonName
		{
			get
			{
				return SeasonName__BackingField;
			}
			private set
			{
				SeasonName__BackingField = value;
			}
		}

		public string SeasonNameLong
		{
			get
			{
				return SeasonNameLong__BackingField;
			}
			private set
			{
				SeasonNameLong__BackingField = value;
			}
		}

		public bool SeasonsControl
		{
			get
			{
				return SeasonsControl__BackingField;
			}
			private set
			{
				SeasonsControl__BackingField = value;
			}
		}

		public bool IsReady
		{
			get
			{
				return IsReady__BackingField;
			}
			private set
			{
				IsReady__BackingField = value;
			}
		}

		public int CoolDownMessages
		{
			get
			{
				return CoolDownMessages__BackingField;
			}
			private set
			{
				CoolDownMessages__BackingField = value;
			}
		}

		private void _003C_002Ector_003Eb__44_0(GetConfigResponse data)
		{
		}
	}
}
