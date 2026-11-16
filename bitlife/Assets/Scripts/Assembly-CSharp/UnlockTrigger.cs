public abstract class UnlockTrigger<T> : IUnlockTriggerWithContext<T>, IUnlockTriggerBase, IValidatable where T : struct
{
	public abstract string TriggerID { get; }

	public void TriggerUnlock(T context, Scenario scenario)
	{
	}

	public bool TriggerUnlock(T context, out Scenario scenario)
	{
		scenario = null;
		return false;
	}

	public abstract bool IsValid();

	protected abstract bool MeetsUnlockConditions(T context);

	protected abstract Scenario UnlockContent(T context);
}
