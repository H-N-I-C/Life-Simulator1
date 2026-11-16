using System.Collections.Generic;

namespace Game
{
	public class DataCultWorkshopProduct
	{
		private string ProductName__BackingField;

		private string ProductNamePlural__BackingField;

		private CULT_PHILOSOPHY Philosophy__BackingField;

		private float AverageUnitPrice__BackingField;

		private float MaxUnitPrice__BackingField;

		private float MinUnitCost__BackingField;

		private float MaxUnitCost__BackingField;

		private int EngagementScore__BackingField;

		private int ControversyScore__BackingField;

		private List<string> OptimalLocation__BackingField;

		private int DifficultyScore__BackingField;

		public string ProductName
		{
			get
			{
				return ProductName__BackingField;
			}
			set
			{
				ProductName__BackingField = value;
			}
		}

		public string ProductNamePlural
		{
			get
			{
				return ProductNamePlural__BackingField;
			}
			set
			{
				ProductNamePlural__BackingField = value;
			}
		}

		public CULT_PHILOSOPHY Philosophy
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

		public float AverageUnitPrice
		{
			get
			{
				return AverageUnitPrice__BackingField;
			}
			set
			{
				AverageUnitPrice__BackingField = value;
			}
		}

		public float MaxUnitPrice
		{
			get
			{
				return MaxUnitPrice__BackingField;
			}
			set
			{
				MaxUnitPrice__BackingField = value;
			}
		}

		public float MinUnitCost
		{
			get
			{
				return MinUnitCost__BackingField;
			}
			set
			{
				MinUnitCost__BackingField = value;
			}
		}

		public float MaxUnitCost
		{
			get
			{
				return MaxUnitCost__BackingField;
			}
			set
			{
				MaxUnitCost__BackingField = value;
			}
		}

		public int EngagementScore
		{
			get
			{
				return EngagementScore__BackingField;
			}
			set
			{
				EngagementScore__BackingField = value;
			}
		}

		public int ControversyScore
		{
			get
			{
				return ControversyScore__BackingField;
			}
			set
			{
				ControversyScore__BackingField = value;
			}
		}

		public List<string> OptimalLocation
		{
			get
			{
				return OptimalLocation__BackingField;
			}
			set
			{
				OptimalLocation__BackingField = value;
			}
		}

		public int DifficultyScore
		{
			get
			{
				return DifficultyScore__BackingField;
			}
			set
			{
				DifficultyScore__BackingField = value;
			}
		}
	}
}
