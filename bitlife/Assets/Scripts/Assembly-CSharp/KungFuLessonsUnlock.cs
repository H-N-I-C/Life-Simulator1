public class KungFuLessonsUnlock : UnlockTriggerCategory<MartialArtContext>
{
	private const string MARTIAL_ART_NAME = "kung fu";

	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<MartialArtContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private bool ValidateIsKungFu(MartialArtContext context)
	{
		return false;
	}
}
