using System;
using System.Collections.Generic;
using Genesis.Entities;

namespace Genesis.FeatureContainers
{
	public static class GenesisSlotReference
	{
		public const string CandidateReferenceProperty = "candidate.entity_id";

		public static bool TryGetDeclaredReferenceValue(IGenesisEntity holderEntity, GenesisFeatureContainer holderContainer, string referenceProperty, out string value)
		{
			value = null;
			return false;
		}

		public static bool TrySetDeclaredReferenceValue(IGenesisEntity holderEntity, GenesisFeatureContainer holderContainer, string referenceProperty, string value)
		{
			return false;
		}

		public static IGenesisEntity ResolveUniqueDirectChildEntityReference(IGenesisEntity holderEntity, string slotPath, Func<string, IGenesisEntity> resolveEntityId)
		{
			return null;
		}

		private static IEnumerable<string> EnumerateDeclaredPropertyKeys(IGenesisEntity holderEntity)
		{
			return null;
		}
	}
}
