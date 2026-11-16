using System;
using UnityEngine;

public class VampireTransformation : MonoBehaviour
{
	private static VampireTransformation Instance;

	private static Scenario NextScenario;

	private static Action transitionStateRelease;

	private bool completed;

	private static void DefaultTransitionStateRelease()
	{
	}

	public static void InstantiateAtSibling(Transform bloodCurtain, Scenario nextScenario)
	{
	}

	private static void RetireActiveInstanceForReplacement()
	{
	}

	public void OnAnimationShowNextScenario()
	{
	}

	public void OnAnimationEnd()
	{
	}

	private void OnDestroy()
	{
	}

	private void Complete(bool destroyObject, bool releaseTransitionState)
	{
	}
}
