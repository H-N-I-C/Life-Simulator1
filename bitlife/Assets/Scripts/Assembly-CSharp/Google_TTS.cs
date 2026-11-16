using System;
using UnityEngine;
using UnityEngine.Networking;

public class Google_TTS : UAP_CustomTTS
{
	[Serializable]
	public class GoogleTTSSynthesizeResponse
	{
		public string audioContent;
	}

	private AudioSource m_AudioPlayer;

	private UnityWebRequest m_CurrentRequest;

	private bool m_IsWaitingForSynth;

	protected override void Initialize()
	{
	}

	protected override TTSInitializationState GetInitializationStatus()
	{
		return TTSInitializationState.NotInitialized;
	}

	protected override void SpeakText(string textToSay, float speakRate)
	{
	}

	private void Update()
	{
	}

	protected override void StopSpeaking()
	{
	}

	protected override bool IsCurrentlySpeaking()
	{
		return false;
	}
}
