public class CatPurchaseCategory : UnlockTriggerCategory<PetPurchaseContext>
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

	private bool ValidateIsCat(PetPurchaseContext context)
	{
		return false;
	}

	private bool ValidateIsCatAndAdopted(PetPurchaseContext context)
	{
		return false;
	}
}
