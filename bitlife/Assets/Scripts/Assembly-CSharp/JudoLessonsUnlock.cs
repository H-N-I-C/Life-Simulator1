public class JudoLessonsUnlock : UnlockTriggerCategory<MartialArtContext>
{
	private const string MARTIAL_ART_NAME = "judo";

	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<MartialArtContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private bool ValidateIsJudo(MartialArtContext context)
	{
		return false;
	}
}
