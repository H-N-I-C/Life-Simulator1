using System.Collections.Generic;

public static class AgeUpScenarioQueueGuard
{
	public static bool ShouldAdvanceAfterClose(Scenario scenario)
	{
		return false;
	}

	public static void MarkPresented(ICollection<Scenario> presented, Scenario scenario)
	{
	}

	public static Scenario GetNextPending(List<Scenario> queue, ICollection<Scenario> presented, Scenario justDismissed)
	{
		return null;
	}

	public static Scenario GetNextPendingForRun(bool runInProgress, List<Scenario> queue, ICollection<Scenario> presented, Scenario justDismissed)
	{
		return null;
	}

	public static Scenario ResolveNativeContinuation(Scenario chainNext, List<Scenario> queue, ICollection<Scenario> presented, Scenario justDismissed)
	{
		return null;
	}

	public static Scenario ResolveNativeContinuationForRun(bool runInProgress, Scenario chainNext, List<Scenario> queue, ICollection<Scenario> presented, Scenario justDismissed)
	{
		return null;
	}

	public static bool CanCompleteRun(List<Scenario> queue, ICollection<Scenario> presented, Scenario justDismissed)
	{
		return false;
	}
}
