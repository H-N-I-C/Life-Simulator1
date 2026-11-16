public class TaekwondoLessonsUnlock : UnlockTriggerCategory<MartialArtContext>
{
	private const string MARTIAL_ART_NAME = "taekwondo";

	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<MartialArtContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private bool ValidateIsTaekwondo(MartialArtContext context)
	{
		return false;
	}
}
