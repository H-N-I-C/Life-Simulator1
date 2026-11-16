using System.Collections.Generic;

namespace Genesis.Entities
{
	public static class GenesisEntityFactory
	{
		public static SimEntity CreateRandomNpc(string entityType, int? overrideAge = null, int? overrideGender = null, Dictionary<string, string> customProperties = null)
		{
			return null;
		}

		private static int GetVoltronAppearanceGender(int gender)
		{
			return 0;
		}

		public static SimEntity CreateNpcFromPerson(string entityType, SimPerson source, Dictionary<string, string> customProperties = null)
		{
			return null;
		}

		public static SimEntity CreateAndRegisterRandomNpc(string entityType, int? overrideAge = null, int? overrideGender = null, Dictionary<string, string> customProperties = null)
		{
			return null;
		}

		public static SimEntity CreateAndRegisterNpcFromPerson(string entityType, SimPerson source, Dictionary<string, string> customProperties = null)
		{
			return null;
		}

		public static GenesisDataEntity CreateObjectEntity(string entityType, Dictionary<string, string> properties = null, string variantFilterKey = null, string variantFilterValue = null)
		{
			return null;
		}

		public static GenesisDataEntity CreateAndRegisterObjectEntity(string entityType, Dictionary<string, string> properties = null)
		{
			return null;
		}

		internal static void ApplySubtypeToEntity(SimEntity entity, GenesisEntitySubtypeDefinition subtype)
		{
		}

		private static void ApplySubtypeAttributes(SimEntity entity, GenesisEntitySubtypeDefinition subtype)
		{
		}

		private static void ApplySubtypeBinaryProperties(SimEntity entity, GenesisEntitySubtypeDefinition subtype)
		{
		}

		private static void ApplySubtypeStringProperties(SimEntity entity, GenesisEntitySubtypeDefinition subtype)
		{
		}

		private static void ApplySubtypeNumericProperties(SimEntity entity, GenesisEntitySubtypeDefinition subtype)
		{
		}

		private static int DrawInclusiveSubtypeRange(int min, int max, string subtypeName, string propertyName)
		{
			return 0;
		}

		internal static void ApplyVariantToObjectEntity(GenesisDataEntity entity, GenesisEntitySubtypeDefinition variant)
		{
		}

		private static void ApplyVariantBinaryProperties(GenesisDataEntity entity, GenesisEntitySubtypeDefinition variant)
		{
		}

		private static void ApplyVariantStringProperties(GenesisDataEntity entity, GenesisEntitySubtypeDefinition variant)
		{
		}

		private static void ApplyVariantNumericProperties(GenesisDataEntity entity, GenesisEntitySubtypeDefinition variant)
		{
		}

		private static void ApplyVariantAttributes(GenesisDataEntity entity, GenesisEntitySubtypeDefinition variant)
		{
		}
	}
}
