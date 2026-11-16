using Genesis.Lists.Models;

namespace Genesis.Lists.Runtime
{
	internal static class GenesisListPresentationResolver
	{
		public static GenesisListPresentationResolution Resolve(GenesisListDefinition definition, GenesisListInvocationSource invocationSource, bool isListCurrentlyVisible, bool dismissAllFirstRequested = false)
		{
			return default;
		}

		public static UILIST_TYPE ResolveListType(GenesisListDefinition definition, GenesisListInvocationSource invocationSource)
		{
			return UILIST_TYPE.LIST_TYPE_NORMAL;
		}
	}
}
