public class DogPurchaseCategory : UnlockTriggerCategory<PetPurchaseContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<PetPurchaseContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private static FightClubMoveContext Convert(PetPurchaseContext context)
	{
		return default;
	}

	private bool ValidateIsDog(PetPurchaseContext context)
	{
		return false;
	}

	private bool ValidateIsDogAdopted(PetPurchaseContext context)
	{
		return false;
	}
}
