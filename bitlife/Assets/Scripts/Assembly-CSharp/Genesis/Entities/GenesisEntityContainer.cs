using System;
using System.Collections.Generic;

namespace Genesis.Entities
{
	[Serializable]
	public class GenesisEntityContainer
	{
		internal sealed class TransactionSnapshot
		{
			internal readonly List<SimEntity> Npcs;

			internal readonly List<GenesisDataEntity> Objects;

			internal readonly Dictionary<SimEntity, SimEntity.CustomPropertyTransactionSnapshot> NpcState;

			internal readonly Dictionary<GenesisDataEntity, GenesisDataEntity.TransactionSnapshot> ObjectState;

			internal TransactionSnapshot(List<SimEntity> npcs, List<GenesisDataEntity> objects, Dictionary<SimEntity, SimEntity.CustomPropertyTransactionSnapshot> npcState, Dictionary<GenesisDataEntity, GenesisDataEntity.TransactionSnapshot> objectState)
			{
			}
		}

		public struct PendingDeathScenario
		{
			public IGenesisEntity Entity;

			public string ScenarioFile;

			public PendingDeathScenario(IGenesisEntity entity, string scenarioFile)
			{
				Entity = null;
				ScenarioFile = null;
			}
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<SimEntity, bool> _003C_003E9__21_0;

			public static Func<SimEntity, bool> _003C_003E9__22_0;

			public static Func<SimEntity, bool> _003C_003E9__23_0;

			public static Func<SimEntity, bool> _003C_003E9__27_0;

			public static Func<GenesisDataEntity, bool> _003C_003E9__34_0;

			public static Func<GenesisDataEntity, bool> _003C_003E9__36_0;

			public static Func<GenesisDataEntity, bool> _003C_003E9__38_0;

			public static Func<SimEntity, bool> _003C_003E9__57_0;

			public static Func<SimEntity, bool> _003C_003E9__57_1;

			public static Func<GenesisDataEntity, bool> _003C_003E9__57_2;

			public static Func<GenesisDataEntity, bool> _003C_003E9__57_3;

			internal bool _003CGetNpcs_003Eb__21_0(SimEntity n)
			{
				return false;
			}

			internal bool _003CGetAllNpcs_003Eb__22_0(SimEntity n)
			{
				return false;
			}

			internal bool _003CGetDeadNpcs_003Eb__23_0(SimEntity n)
			{
				return false;
			}

			internal bool _003Cget_NpcCount_003Eb__27_0(SimEntity n)
			{
				return false;
			}

			internal bool _003CGetObjectEntities_003Eb__34_0(GenesisDataEntity e)
			{
				return false;
			}

			internal bool _003CGetObjectEntitiesByType_003Eb__36_0(GenesisDataEntity e)
			{
				return false;
			}

			internal bool _003Cget_ObjectEntityCount_003Eb__38_0(GenesisDataEntity e)
			{
				return false;
			}

			internal bool _003CToString_003Eb__57_0(SimEntity n)
			{
				return false;
			}

			internal bool _003CToString_003Eb__57_1(SimEntity n)
			{
				return false;
			}

			internal bool _003CToString_003Eb__57_2(GenesisDataEntity e)
			{
				return false;
			}

			internal bool _003CToString_003Eb__57_3(GenesisDataEntity e)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass21_0
		{
			public string entityType;

			internal bool _003CGetNpcs_003Eb__1(SimEntity n)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass22_0
		{
			public string entityType;

			internal bool _003CGetAllNpcs_003Eb__1(SimEntity n)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass23_0
		{
			public string entityType;

			internal bool _003CGetDeadNpcs_003Eb__1(SimEntity n)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass24_0
		{
			public string entityId;

			internal bool _003CGetNpcById_003Eb__0(SimEntity n)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass28_0
		{
			public bool includeDeadRecent;

			public Func<SimEntity, bool> predicate;

			internal bool _003CGetNpcs_003Eb__0(SimEntity n)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass29_0
		{
			public string entityType;

			public int minAge;

			public int maxAge;

			internal bool _003CGetNpcsByAge_003Eb__0(SimEntity n)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass30_0
		{
			public string entityType;

			public float minRelationship;

			internal bool _003CGetNpcsByRelationship_003Eb__0(SimEntity n)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass31_0
		{
			public string entityType;

			public string propertyKey;

			public string propertyValue;

			internal bool _003CGetNpcByProperty_003Eb__0(SimEntity n)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass33_0
		{
			public string entityId;

			internal bool _003CRemoveObjectEntity_003Eb__0(GenesisDataEntity e)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass35_0
		{
			public string entityId;

			internal bool _003CGetObjectEntity_003Eb__0(GenesisDataEntity e)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass36_0
		{
			public string entityType;

			internal bool _003CGetObjectEntitiesByType_003Eb__1(GenesisDataEntity e)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass41_0
		{
			public Func<GenesisDataEntity, bool> predicate;

			internal bool _003CGetObjectEntities_003Eb__0(GenesisDataEntity e)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass42_0
		{
			public string key;

			public string value;

			internal bool _003CGetObjectEntitiesByProperty_003Eb__0(GenesisDataEntity e)
			{
				return false;
			}
		}

		[NonSerialized]
		public Action<SimEntity> OnEntityDied;

		[NonSerialized]
		public Action<GenesisDataEntity> OnObjectEntityExpired;

		private List<SimEntity> _npcs;

		private List<GenesisDataEntity> _objectEntities;

		private int _deadRetentionYears;

		private List<string> _autoFillEntityTypes;

		private List<int> _autoFillMinCounts;

		private List<string> _deathScenarioEntityTypes;

		private List<string> _deathScenarioFiles;

		private List<string> _deadRetentionEntityTypes;

		private List<int> _deadRetentionYears_perType;

		[NonSerialized]
		private List<PendingDeathScenario> _pendingDeathScenarios;

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

		public int NpcCount => 0;

		public int ObjectEntityCount => 0;

		public void SetDeadRetention(string entityType, int years)
		{
		}

		public int GetDeadRetention(string entityType)
		{
			return 0;
		}

		public void AddNpc(SimEntity npc)
		{
		}

		public bool RemoveNpc(SimEntity npc)
		{
			return false;
		}

		public List<SimEntity> GetNpcs(string entityType = null)
		{
			return null;
		}

		public List<SimEntity> GetAllNpcs(string entityType = null)
		{
			return null;
		}

		public List<SimEntity> GetDeadNpcs(string entityType = null)
		{
			return null;
		}

		public SimEntity GetNpcById(string entityId)
		{
			return null;
		}

		public IGenesisEntity GetEntityById(string entityId)
		{
			return null;
		}

		public List<SimEntity> GetNpcs(Func<SimEntity, bool> predicate, bool includeDeadRecent = false)
		{
			return null;
		}

		public List<SimEntity> GetNpcsByAge(string entityType, int minAge, int maxAge)
		{
			return null;
		}

		public List<SimEntity> GetNpcsByRelationship(string entityType, float minRelationship)
		{
			return null;
		}

		public SimEntity GetNpcByProperty(string entityType, string propertyKey, string propertyValue)
		{
			return null;
		}

		public void AddObjectEntity(GenesisDataEntity entity)
		{
		}

		public bool RemoveObjectEntity(string entityId)
		{
			return false;
		}

		public List<GenesisDataEntity> GetObjectEntities()
		{
			return null;
		}

		public GenesisDataEntity GetObjectEntity(string entityId)
		{
			return null;
		}

		public List<GenesisDataEntity> GetObjectEntitiesByType(string entityType)
		{
			return null;
		}

		internal TransactionSnapshot CaptureTransactionSnapshot()
		{
			return null;
		}

		internal void RestoreTransactionSnapshot(TransactionSnapshot snapshot)
		{
		}

		public List<GenesisDataEntity> GetObjectEntities(Func<GenesisDataEntity, bool> predicate)
		{
			return null;
		}

		public List<GenesisDataEntity> GetObjectEntitiesByProperty(string key, string value)
		{
			return null;
		}

		public void IncrementAge()
		{
		}

		private void IncrementAgeNpcs()
		{
		}

		private void OnNpcDied(SimEntity npc)
		{
		}

		private void IncrementAgeObjectEntities()
		{
		}

		private void HandleObjectEntityExpired(GenesisDataEntity entity)
		{
		}

		public void Clear()
		{
		}

		public void SetAutoFill(string entityType, int minPopulation)
		{
		}

		public void RemoveAutoFill(string entityType)
		{
		}

		public int GetAutoFillMin(string entityType)
		{
			return 0;
		}

		private void AutoFillNpcs()
		{
		}

		public void SetDeathScenario(string entityType, string scenarioFileName)
		{
		}

		public void RemoveDeathScenario(string entityType)
		{
		}

		public string GetDeathScenario(string entityType)
		{
			return null;
		}

		public List<PendingDeathScenario> GetAndClearPendingDeathScenarios()
		{
			return null;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
