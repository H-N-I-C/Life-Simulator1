namespace Genesis.FeatureContainers
{
	public enum GenesisFeaturePropertyLookupStatus
	{
		Bounded = 0,
		Unbounded = 1,
		PropertyAbsent = 2,
		ContainerUnregistered = 3,
		NoPropertySheet = 4,
		RegistryUnavailable = 5,
		PropertySheetUnavailable = 6,
		PropertySheetInvalid = 7
	}
}
