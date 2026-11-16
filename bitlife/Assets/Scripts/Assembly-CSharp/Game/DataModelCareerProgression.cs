using System.Collections.Generic;

namespace Game
{
	public class DataModelCareerProgression
	{
		private MODEL_CAREER_TYPE ModelType__BackingField;

		private int JobsToPromotion__BackingField;

		private float MinLooks__BackingField;

		private float MinHealth__BackingField;

		private float MinProfile__BackingField;

		private bool CanBeFamous__BackingField;

		private int StressScore__BackingField;

		private float MinSalary__BackingField;

		private float MaxSalary__BackingField;

		private int MinAge__BackingField;

		private int MaxAge__BackingField;

		private int TooOldAge__BackingField;

		public List<DataDiseaseType> DiseaseArray;

		public MODEL_CAREER_TYPE ModelType
		{
			get
			{
				return ModelType__BackingField;
			}
			set
			{
				ModelType__BackingField = value;
			}
		}

		public int JobsToPromotion
		{
			get
			{
				return JobsToPromotion__BackingField;
			}
			set
			{
				JobsToPromotion__BackingField = value;
			}
		}

		public float MinLooks
		{
			get
			{
				return MinLooks__BackingField;
			}
			set
			{
				MinLooks__BackingField = value;
			}
		}

		public float MinHealth
		{
			get
			{
				return MinHealth__BackingField;
			}
			set
			{
				MinHealth__BackingField = value;
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

		public bool CanBeFamous
		{
			get
			{
				return CanBeFamous__BackingField;
			}
			set
			{
				CanBeFamous__BackingField = value;
			}
		}

		public int StressScore
		{
			get
			{
				return StressScore__BackingField;
			}
			set
			{
				StressScore__BackingField = value;
			}
		}

		public float MinSalary
		{
			get
			{
				return MinSalary__BackingField;
			}
			set
			{
				MinSalary__BackingField = value;
			}
		}

		public float MaxSalary
		{
			get
			{
				return MaxSalary__BackingField;
			}
			set
			{
				MaxSalary__BackingField = value;
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

		public int TooOldAge
		{
			get
			{
				return TooOldAge__BackingField;
			}
			set
			{
				TooOldAge__BackingField = value;
			}
		}
	}
}
