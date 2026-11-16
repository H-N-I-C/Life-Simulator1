using System.Collections.Generic;

namespace Game
{
	public class DataCultCommunePropertyType
	{
		private CULT_PROPERTY_TYPE Type__BackingField;

		private List<string> Descriptors__BackingField;

		private int PriceScore__BackingField;

		private int MinAge__BackingField;

		private int MaxAge__BackingField;

		private int CommonnessScore__BackingField;

		private int StreetType__BackingField;

		public CULT_PROPERTY_TYPE Type
		{
			get
			{
				return Type__BackingField;
			}
			set
			{
				Type__BackingField = value;
			}
		}

		public List<string> Descriptors
		{
			get
			{
				return Descriptors__BackingField;
			}
			set
			{
				Descriptors__BackingField = value;
			}
		}

		public int PriceScore
		{
			get
			{
				return PriceScore__BackingField;
			}
			set
			{
				PriceScore__BackingField = value;
			}
		}

		public int MinAge
		{
			get
			{
				return MinAge__BackingField;
			}
			set
			{
				MinAge__BackingField = value;
			}
		}

		public int MaxAge
		{
			get
			{
				return MaxAge__BackingField;
			}
			set
			{
				MaxAge__BackingField = value;
			}
		}

		public int CommonnessScore
		{
			get
			{
				return CommonnessScore__BackingField;
			}
			set
			{
				CommonnessScore__BackingField = value;
			}
		}

		public int StreetType
		{
			get
			{
				return StreetType__BackingField;
			}
			set
			{
				StreetType__BackingField = value;
			}
		}
	}
}
