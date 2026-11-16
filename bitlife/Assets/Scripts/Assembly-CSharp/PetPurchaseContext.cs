public struct PetPurchaseContext
{
	private readonly SimPet PetPurchased__BackingField;

	private readonly PET_SOURCE FromSource__BackingField;

	public SimPet PetPurchased => PetPurchased__BackingField;

	public PET_SOURCE FromSource => FromSource__BackingField;

	public PetPurchaseContext(SimPet petPurchased, PET_SOURCE petSource)
	{
		PetPurchased__BackingField = null;
		FromSource__BackingField = PET_SOURCE.WILD;
	}
}
