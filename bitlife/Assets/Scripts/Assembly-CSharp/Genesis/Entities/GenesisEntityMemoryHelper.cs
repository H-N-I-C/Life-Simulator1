using System.Collections.Generic;
using System.Runtime.InteropServices;
using Genesis.Controllers;

namespace Genesis.Entities
{
	public static class GenesisEntityMemoryHelper
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003Ec__DisplayClass3_0
		{
			public GenesisEntityTransientMemoryScope transientScope;

			public GenesisMemory memory;
		}

		private static readonly HashSet<string> ReservedPersonProjectionKeys;

		public static bool IsReservedPersonProjectionKey(string propertyKey)
		{
			return false;
		}

		public static void PopulatePersonMemory(ScenarioController controller, SimPerson person, string prefix)
		{
		}

		private static void PopulatePersonMemoryCore(ScenarioController controller, SimPerson person, string prefix, GenesisEntityTransientMemoryScope transientScope)
		{
		}

		public static void PopulateSimEntityMemory(ScenarioController controller, SimEntity entity, string prefix)
		{
		}

		public static GenesisEntityTransientMemoryScope PopulateSimEntityTransientMemory(ScenarioController controller, SimEntity entity, string[] prefixes)
		{
			return null;
		}

		public static void PopulatePersonMemoryFromData(ScenarioController controller, string prefix, string fullName, int age, float relationship, string pronounSubject, string pronounObject, string pronounPossessive, string emoji)
		{
		}

		internal static void _003CPopulatePersonMemoryCore_003Eg__WriteValue_007C3_0(string key, object value, ref _003C_003Ec__DisplayClass3_0 P_2)
		{
		}
	}
}
