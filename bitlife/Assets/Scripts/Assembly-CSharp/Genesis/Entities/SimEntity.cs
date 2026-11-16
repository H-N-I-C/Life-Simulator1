using System;
using System.Collections.Generic;
using Genesis.Controllers;
using Genesis.Lists.Models;

namespace Genesis.Entities
{
	[Serializable]
	public class SimEntity : SimLover, IGenesisEntity, IGenesisListFocusProvider
	{
		internal sealed class CustomPropertyTransactionSnapshot
		{
			internal readonly List<string> Keys;

			internal readonly List<string> Values;

			internal CustomPropertyTransactionSnapshot(List<string> keys, List<string> values)
			{
			}
		}

		private string _entityType;

		private string _entityId;

		private List<string> _customPropertyKeys;

		private List<string> _customPropertyValues;

		[NonSerialized]
		private Dictionary<string, string> _customProperties;

		private bool _genesisDeathNotificationPending;

		private static readonly string[] NativeFirstNameIdentityKeys;

		public string EntityType => null;

		public string EntityId => null;

		public bool IsAlive => false;

		public IReadOnlyList<string> CustomPropertyKeys => null;

		public SimEntity(string entityType)
			: base(initHere: false)
		{
		}

		public SimEntity(string entityType, SimPerson source)
			: base(initHere: false)
		{
		}

		public SimEntity(string entityType, string dataName, int dataAge)
			: base(initHere: false)
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

		internal bool TrySynchronizeNativeNameFromCustomIdentity()
		{
			return false;
		}

		internal bool CanSynchronizeNativeNameFromCustomIdentity()
		{
			return false;
		}

		public void SyncCustomIdentityFromNativeName()
		{
		}

		private bool TryGetNativeNameFromCustomIdentity(out string firstName, out string lastName, out bool hasCustomIdentity)
		{
			firstName = null;
			lastName = null;
			hasCustomIdentity = default;
			return false;
		}

		public override void IncrementAge()
		{
		}

		private void ApplyDefinitionAgeUpResets()
		{
		}

		internal bool UsesNativeRelationshipLifecycle()
		{
			return false;
		}

		private bool UsesNativeFriendLifecycle()
		{
			return false;
		}

		private bool UsesNativeLoverLifecycle()
		{
			return false;
		}

		internal bool TryConsumeGenesisDeathNotification()
		{
			return false;
		}

		private void ApplySubtypeAgeUp()
		{
		}

		private void AdjustAttribute(string attrKey, float adjustment)
		{
		}

		public override void Die()
		{
		}

		public override string OccupationString()
		{
			return null;
		}

		public override string GetRelationshipString()
		{
			return null;
		}

		public override string GetFormattedRelationshipString()
		{
			return null;
		}

		public void ApplyFocus(ScenarioController controller, string focusMemoryKey)
		{
		}

		public void ApplyFocusWithoutDefaultBackfill(ScenarioController controller, string focusMemoryKey)
		{
		}

		private void ApplyFocusCore(ScenarioController controller, string focusMemoryKey, bool backfillDefinitionDefaults)
		{
		}

		public SimEntity SetProperty(string key, string value)
		{
			return null;
		}

		public string GetProperty(string key, string fallback = "")
		{
			return null;
		}

		public bool HasProperty(string key)
		{
			return false;
		}

		internal CustomPropertyTransactionSnapshot CaptureCustomPropertyTransactionSnapshot()
		{
			return null;
		}

		internal void RestoreCustomPropertyTransactionSnapshot(CustomPropertyTransactionSnapshot snapshot)
		{
		}

		private Dictionary<string, string> GetCustomProperties()
		{
			return null;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
