public interface IUnlockTriggerWithContext<in T> : IUnlockTriggerBase, IValidatable
{
	void TriggerUnlock(T context, Scenario scenario);

	bool TriggerUnlock(T context, out Scenario scenario);
}
