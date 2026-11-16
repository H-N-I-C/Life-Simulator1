public class KarateLessonsUnlock : UnlockTriggerCategory<MartialArtContext>
{
	private const string MARTIAL_ART_NAME = "karate";

	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<MartialArtContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private bool ValidateIsKarate(MartialArtContext context)
	{
		return false;
	}
}
