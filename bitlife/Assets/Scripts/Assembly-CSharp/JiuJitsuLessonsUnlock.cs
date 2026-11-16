public class JiuJitsuLessonsUnlock : UnlockTriggerCategory<MartialArtContext>
{
	private const string MARTIAL_ART_NAME = "jiu-jitsu";

	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<MartialArtContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private bool ValidateIsJiuJitsu(MartialArtContext context)
	{
		return false;
	}
}
