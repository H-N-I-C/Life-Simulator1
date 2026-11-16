using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Inst;

	public List<AudioClip> AudioClips;

	private AudioClip currentClip;

	private AudioSource audioSource;

	private void Awake()
	{
	}

	public bool IsPlayingSound()
	{
		return false;
	}

	public void PlayClipDirect(AudioClip clip)
	{
	}

	private void PlayClip(string clipName, bool allowDuplicateOverlap = false)
	{
	}

	public AudioClip GetAudioClipWithFileName(string clipName)
	{
		return null;
	}

	public void PlaySoundWithFileName(string fileName)
	{
	}

	public void PlaySoundWithFileNameAtPitch(string fileName, float pitch)
	{
	}

	public KeyValuePair<string, float>? PlayRandomAlienSound()
	{
		return null;
	}

	public void PlaySound(SOUND sound)
	{
	}

	public void PlaySoundWithIndex(int pIndex)
	{
	}

	public AudioClip GetSoundWithIndex(int pIndex)
	{
		return null;
	}

	private string GetSoundNameWithIndex(int pIndex)
	{
		return null;
	}

	public void PlayZooAnimalSound(SimZooAnimal animal)
	{
	}

	public static string SoundNameForInstrument(string instrumentDescription)
	{
		return null;
	}
}
