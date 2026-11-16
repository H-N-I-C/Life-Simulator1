using System.Collections.Generic;

namespace Game
{
	public class DataCultCommunardSpendTime
	{
		private string Pastime__BackingField;

		private List<CULT_PHILOSOPHY> Philosophy__BackingField;

		public string Pastime
		{
			get
			{
				return Pastime__BackingField;
			}
			set
			{
				Pastime__BackingField = value;
			}
		}

		public List<CULT_PHILOSOPHY> Philosophy
		{
			get
			{
				return Philosophy__BackingField;
			}
			set
			{
				Philosophy__BackingField = value;
			}
		}
	}
}
