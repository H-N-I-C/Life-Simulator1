namespace Genesis.Lists.Runtime
{
	internal struct GenesisListPresentationResolution
	{
		private readonly UILIST_TYPE ListType__BackingField;

		private readonly bool DismissAllFirst__BackingField;

		public UILIST_TYPE ListType => ListType__BackingField;

		public bool DismissAllFirst => DismissAllFirst__BackingField;

		public GenesisListPresentationResolution(UILIST_TYPE listType, bool dismissAllFirst)
		{
			ListType__BackingField = UILIST_TYPE.LIST_TYPE_NORMAL;
			DismissAllFirst__BackingField = false;
		}
	}
}
