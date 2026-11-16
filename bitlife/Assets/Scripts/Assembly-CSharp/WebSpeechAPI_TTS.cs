public class WebSpeechAPI_TTS : UAP_CustomTTS
{
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

	protected override void StopSpeaking()
	{
	}

	protected override bool IsCurrentlySpeaking()
	{
		return false;
	}
}
