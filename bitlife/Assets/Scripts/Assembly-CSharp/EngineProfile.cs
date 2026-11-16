using UnityEngine;

public class EngineProfile : ScriptableObject
{
	public float EngineStartPitch;

	public float EngineEndPitch;

	public AudioClip Clip;

	public AnimationCurve curve;

	public float GetNormalizedSpeed(int speed)
	{
		return 0f;
	}
}
