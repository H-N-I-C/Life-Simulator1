using System;

namespace Game
{
	[Serializable]
	public class SimCelebrityLover : SimLover
	{
		private bool HasGainFameFromThisLover__BackingField;

		private string CelebrityFameType__BackingField;

		private string Genre__BackingField;

		private int FameScore__BackingField;

		private string Company__BackingField;

		private string Platform__BackingField;

		private string Sport__BackingField;

		private string SportsTeam__BackingField;

		private int RetirementAge__BackingField;

		private float MinSalary__BackingField;

		private float MaxSalary__BackingField;

		private MODEL_CAREER_TYPE ModelCareerLevel__BackingField;

		public bool HasGainFameFromThisLover
		{
			get
			{
				return HasGainFameFromThisLover__BackingField;
			}
			set
			{
				HasGainFameFromThisLover__BackingField = value;
			}
		}

		public string CelebrityFameType
		{
			get
			{
				return CelebrityFameType__BackingField;
			}
			set
			{
				CelebrityFameType__BackingField = value;
			}
		}

		public string Genre
		{
			get
			{
				return Genre__BackingField;
			}
			set
			{
				Genre__BackingField = value;
			}
		}

		public int FameScore
		{
			get
			{
				return FameScore__BackingField;
			}
			set
			{
				FameScore__BackingField = value;
			}
		}

		public string Company
		{
			get
			{
				return Company__BackingField;
			}
			set
			{
				Company__BackingField = value;
			}
		}

		public string Platform
		{
			get
			{
				return Platform__BackingField;
			}
			set
			{
				Platform__BackingField = value;
			}
		}

		public string Sport
		{
			get
			{
				return Sport__BackingField;
			}
			set
			{
				Sport__BackingField = value;
			}
		}

		public string SportsTeam
		{
			get
			{
				return SportsTeam__BackingField;
			}
			set
			{
				SportsTeam__BackingField = value;
			}
		}

		public int RetirementAge
		{
			get
			{
				return RetirementAge__BackingField;
			}
			set
			{
				RetirementAge__BackingField = value;
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

		public MODEL_CAREER_TYPE ModelCareerLevel
		{
			get
			{
				return ModelCareerLevel__BackingField;
			}
			set
			{
				ModelCareerLevel__BackingField = value;
			}
		}

		public SimCelebrityLover(bool initHere)
			: base(initHere: false)
		{
		}

		public SimCelebrityLover(SimPerson otherPerson)
			: base(initHere: false)
		{
		}

		public new bool IsRoyal()
		{
			return false;
		}

		public float GetSalaryForNewCelebrityLover()
		{
			return 0f;
		}

		public string GetCelebrityOccupationString()
		{
			return null;
		}
	}
}
