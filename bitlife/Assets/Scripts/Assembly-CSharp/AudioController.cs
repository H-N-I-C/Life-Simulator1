using UnityEngine;

public class AudioController : MonoBehaviour
{
	private AudioSource _audioSource;

	private EngineProfile _profile;

	private int _currentSpeed;

	private void Init(AudioSource source)
	{
	}

	public void SetProfile(string profileName)
	{
	}

	public void SetSoundLevel(float level)
	{
	}

	public void StartSound(int speed)
	{
	}

	private void CalculateSpeed()
	{
	}

	public void StopSound()
	{
	}

	public void SetSpeed(int speed)
	{
	}

	public static AudioController CreateInstance(string gameObjectName)
	{
		return null;
	}
}
