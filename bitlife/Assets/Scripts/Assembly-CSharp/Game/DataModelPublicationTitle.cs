using System.Collections.Generic;

namespace Game
{
	public class DataModelPublicationTitle
	{
		private string Name__BackingField;

		private int Gender__BackingField;

		private MODEL_PUBLICATION_TYPE Type__BackingField;

		private bool LocalPublication__BackingField;

		private List<MODEL_INDUSTRY> Industry__BackingField;

		private float MinProfile__BackingField;

		private float MaxProfile__BackingField;

		private int CommonnessScore__BackingField;

		public string Name
		{
			get
			{
				return Name__BackingField;
			}
			set
			{
				Name__BackingField = value;
			}
		}

		public int Gender
		{
			get
			{
				return Gender__BackingField;
			}
			set
			{
				Gender__BackingField = value;
			}
		}

		public MODEL_PUBLICATION_TYPE Type
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

		public bool LocalPublication
		{
			get
			{
				return LocalPublication__BackingField;
			}
			set
			{
				LocalPublication__BackingField = value;
			}
		}

		public List<MODEL_INDUSTRY> Industry
		{
			get
			{
				return Industry__BackingField;
			}
			set
			{
				Industry__BackingField = value;
			}
		}

		public float MinProfile
		{
			get
			{
				return MinProfile__BackingField;
			}
			set
			{
				MinProfile__BackingField = value;
			}
		}

		public float MaxProfile
		{
			get
			{
				return MaxProfile__BackingField;
			}
			set
			{
				MaxProfile__BackingField = value;
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
	}
}
