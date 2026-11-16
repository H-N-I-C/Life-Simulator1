public struct DrainBloodContext
{
	private readonly bool FullyDrainedPerson__BackingField;

	public bool FullyDrainedPerson => FullyDrainedPerson__BackingField;

	public DrainBloodContext(bool fullDrain)
	{
		FullyDrainedPerson__BackingField = false;
	}
}
