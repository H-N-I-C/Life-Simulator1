using System;
using System.Collections.Generic;

namespace Genesis.Entities
{
	[Serializable]
	public class GenesisEntitySubtypeDefinition
	{
		private string _subtypeName;

		private int _weight;

		private List<string> _attributeKeys;

		private List<int> _attributeMinValues;

		private List<int> _attributeMaxValues;

		private List<string> _binaryPropertyKeys;

		private List<int> _binaryPropertyWeightTrue;

		private List<int> _binaryPropertyWeightFalse;

		private List<string> _stringPropertyKeys;

		private List<string> _stringPropertyValues;

		private List<int> _stringPropertyWeights;

		private List<string> _numericPropertyKeys;

		private List<int> _numericPropertyMinValues;

		private List<int> _numericPropertyMaxValues;

		private string _heroRelationshipType;

		private int _heroRelationshipMin;

		private int _heroRelationshipMax;

		private string _deathScenarioId;

		private int _ageupHeroRelationshipMin;

		private int _ageupHeroRelationshipMax;

		private List<string> _ageupAttributeKeys;

		private List<int> _ageupAttributeMinValues;

		private List<int> _ageupAttributeMaxValues;

		private List<string> _ageupNumericKeys;

		private List<int> _ageupNumericMinValues;

		private List<int> _ageupNumericMaxValues;

		public string SubtypeName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public int Weight
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public string HeroRelationshipType
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public int HeroRelationshipMin
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int HeroRelationshipMax
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public string DeathScenarioId
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public int AttributeCount => 0;

		public int BinaryPropertyCount => 0;

		public int StringPropertyCount => 0;

		public int NumericPropertyCount => 0;

		public bool HasAgeUpConfig => false;

		public int AgeUpHeroRelationshipMin
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int AgeUpHeroRelationshipMax
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int AgeUpAttributeCount => 0;

		public int AgeUpNumericCount => 0;

		public void AddAttributeRange(string attributeKey, int min, int max)
		{
		}

		public string GetAttributeKey(int index)
		{
			return null;
		}

		public int GetAttributeMin(int index)
		{
			return 0;
		}

		public int GetAttributeMax(int index)
		{
			return 0;
		}

		public void AddBinaryProperty(string key, int weightTrue, int weightFalse)
		{
		}

		public string GetBinaryPropertyKey(int index)
		{
			return null;
		}

		public int GetBinaryPropertyWeightTrue(int index)
		{
			return 0;
		}

		public int GetBinaryPropertyWeightFalse(int index)
		{
			return 0;
		}

		public void AddStringProperty(string key, string value, int weight)
		{
		}

		public string GetStringPropertyKey(int index)
		{
			return null;
		}

		public string GetStringPropertyValue(int index)
		{
			return null;
		}

		public int GetStringPropertyWeight(int index)
		{
			return 0;
		}

		public bool HasStringPropertyValue(string key, string expectedValue)
		{
			return false;
		}

		public void AddNumericProperty(string key, int min, int max)
		{
		}

		public string GetNumericPropertyKey(int index)
		{
			return null;
		}

		public int GetNumericPropertyMin(int index)
		{
			return 0;
		}

		public int GetNumericPropertyMax(int index)
		{
			return 0;
		}

		public void AddAgeUpAttributeRange(string attributeKey, int min, int max)
		{
		}

		public string GetAgeUpAttributeKey(int index)
		{
			return null;
		}

		public int GetAgeUpAttributeMin(int index)
		{
			return 0;
		}

		public int GetAgeUpAttributeMax(int index)
		{
			return 0;
		}

		public void AddAgeUpNumericRange(string key, int min, int max)
		{
		}

		public string GetAgeUpNumericKey(int index)
		{
			return null;
		}

		public int GetAgeUpNumericMin(int index)
		{
			return 0;
		}

		public int GetAgeUpNumericMax(int index)
		{
			return 0;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
