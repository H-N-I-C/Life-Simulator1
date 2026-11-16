using System;
using System.Collections.Generic;
using Genesis.Controllers;
using Genesis.Lists.Models;

namespace Genesis.Entities
{
	[Serializable]
	public sealed class GenesisDataEntity : IGenesisEntity, IGenesisListFocusProvider
	{
		internal sealed class TransactionSnapshot
		{
			internal readonly List<string> Keys;

			internal readonly List<string> Values;

			internal TransactionSnapshot(List<string> keys, List<string> values)
			{
			}
		}

		private string _entityType;

		private string _entityId;

		private List<string> _keys;

		private List<string> _values;

		[NonSerialized]
		private Dictionary<string, string> _cache;

		public string EntityType => null;

		public string EntityId => null;

		public int Count => 0;

		public IReadOnlyList<string> PropertyKeys => null;

		public bool IsAlive => false;

		public GenesisDataEntity(string entityType)
		{
		}

		public GenesisDataEntity Set(string key, string value)
		{
			return null;
		}

		public GenesisDataEntity Set(string key, int value)
		{
			return null;
		}

		public GenesisDataEntity Set(string key, float value)
		{
			return null;
		}

		public GenesisDataEntity Set(string key, bool value)
		{
			return null;
		}

		public string GetString(string key, string fallback = "")
		{
			return null;
		}

		public int GetInt(string key, int fallback = 0)
		{
			return 0;
		}

		public float GetFloat(string key, float fallback = 0f)
		{
			return 0f;
		}

		public bool GetBool(string key, bool fallback = false)
		{
			return false;
		}

		public bool Has(string key)
		{
			return false;
		}

		internal TransactionSnapshot CaptureTransactionSnapshot()
		{
			return null;
		}

		internal void RestoreTransactionSnapshot(TransactionSnapshot snapshot)
		{
		}

		public string GetName()
		{
			return null;
		}

		public string GetPronoun(int pronounType)
		{
			return null;
		}

		public float GetCondition()
		{
			return 0f;
		}

		public string GetEmoji()
		{
			return null;
		}

		public int GetAge()
		{
			return 0;
		}

		public int GetYearsSinceDeath()
		{
			return 0;
		}

		public void IncrementAge()
		{
		}

		public void Die()
		{
		}

		public void ApplyFocus(ScenarioController controller, string focusMemoryKey)
		{
		}

		private Dictionary<string, string> GetCache()
		{
			return null;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
