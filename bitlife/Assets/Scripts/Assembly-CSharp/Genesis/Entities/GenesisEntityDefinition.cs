using System;
using System.Collections.Generic;

namespace Genesis.Entities
{
	[Serializable]
	public class GenesisEntityDefinition
	{
		private sealed class _003C_003Ec__DisplayClass103_0
		{
			public string propertyKey;

			public string expectedValue;

			internal bool _003CSelectRandomSubtype_003Eb__0(GenesisEntitySubtypeDefinition subtype)
			{
				return false;
			}
		}

		private string _entityType;

		private string _kind;

		private int _autoFillMin;

		private string _deathScenarioFile;

		private int _deadRetentionYears;

		private int _minAge;

		private int _maxAge;

		private List<string> _defaultPropertyKeys;

		private List<string> _defaultPropertyValues;

		[NonSerialized]
		private Dictionary<string, string> _defaultPropertyCache;

		private List<string> _ageUpResetPropertyKeys;

		private List<string> _ageUpResetPropertyValues;

		[NonSerialized]
		private Dictionary<string, string> _ageUpResetPropertyCache;

		private List<string> _godModePropertyKeys;

		private List<string> _godModePropertyValues;

		[NonSerialized]
		private Dictionary<string, string> _godModePropertyCache;

		private List<string> _genderKeys;

		private List<int> _genderWeights;

		private List<string> _namePoolFemale;

		private List<string> _namePoolMale;

		private List<string> _namePoolOther;

		private List<string> _skinToneFemale;

		private List<string> _skinToneMale;

		private List<string> _skinToneOther;

		private bool _locationFollowsHero;

		private string _displayStringUpper;

		private string _displayStringLower;

		private List<string> _maritalKeys;

		private List<int> _maritalWeights;

		private List<string> _childrenKeys;

		private List<int> _childrenWeights;

		private List<GenesisEntitySubtypeDefinition> _subtypes;

		public string EntityType
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string Kind
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public bool IsNpc => false;

		public bool IsObject => false;

		public int AutoFillMin
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public string DeathScenarioFile
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public int DeadRetentionYears
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int MinAge
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int MaxAge
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int DefaultPropertyCount => 0;

		public bool LocationFollowsHero
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public string DisplayStringUpper
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string DisplayStringLower
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public List<string> NamePoolFemale => null;

		public List<string> NamePoolMale => null;

		public List<string> NamePoolOther => null;

		public List<string> SkinToneFemale => null;

		public List<string> SkinToneMale => null;

		public List<string> SkinToneOther => null;

		public bool HasGenderWeights => false;

		public List<GenesisEntitySubtypeDefinition> Subtypes => null;

		public bool HasSubtypes => false;

		public Dictionary<string, string> GetDefaultProperties()
		{
			return null;
		}

		public void SetDefaultProperty(string key, string value)
		{
		}

		public string GetDefaultProperty(string key, string fallback = "")
		{
			return null;
		}

		public bool HasDefaultProperty(string key)
		{
			return false;
		}

		public Dictionary<string, string> GetAgeUpResetProperties()
		{
			return null;
		}

		public void SetAgeUpResetProperty(string key, string value)
		{
		}

		public Dictionary<string, string> GetGodModeProperties()
		{
			return null;
		}

		public void SetGodModeProperty(string key, string value)
		{
		}

		public void SetGenderWeights(List<string> keys, List<int> weights)
		{
		}

		public void SetMaritalStatusWeights(List<string> keys, List<int> weights)
		{
		}

		public void SetNumChildrenWeights(List<string> keys, List<int> weights)
		{
		}

		public int SelectWeightedGender()
		{
			return 0;
		}

		public string SelectNameFromPool(int gender)
		{
			return null;
		}

		public string SelectWeightedMaritalStatus()
		{
			return null;
		}

		public string SelectWeightedNumChildren()
		{
			return null;
		}

		private string SelectWeightedFromParallel(List<string> keys, List<int> weights)
		{
			return null;
		}

		public GenesisEntitySubtypeDefinition SelectRandomSubtype()
		{
			return null;
		}

		public GenesisEntitySubtypeDefinition SelectRandomSubtype(string propertyKey, string expectedValue)
		{
			return null;
		}

		private static GenesisEntitySubtypeDefinition SelectWeightedSubtype(List<GenesisEntitySubtypeDefinition> candidates)
		{
			return null;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
