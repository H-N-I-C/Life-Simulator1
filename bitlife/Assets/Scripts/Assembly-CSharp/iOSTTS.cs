using System.Runtime.InteropServices;

public class iOSTTS
{
	private static bool m_VoiceOverRunningAtLastCheck;

	[PreserveSig]
	private static extern bool IsVoiceOverRunning();

	[PreserveSig]
	private static extern bool IsVoiceSpeaking();

	[PreserveSig]
	private static extern void StopVoiceSpeaking();

	[PreserveSig]
	private static extern void StartSpeakingVoiceOver(string textToSpeak);

	[PreserveSig]
	private static extern void StartSpeakingSynthesizer(string textToSpeak, int speechRate);

	[PreserveSig]
	private static extern void InitializeVoice();

	[PreserveSig]
	private static extern void ShutdownVoice();

	public static bool VoiceOverDetected()
	{
		return false;
	}

	public static bool IsSpeaking()
	{
		return false;
	}

	public static void StopSpeaking()
	{
	}

	public static void StartSpeaking(string textToSpeak, bool allowVoiceOver, int speechRate)
	{
	}

	public static void Init()
	{
	}

	public static void Shutdown()
	{
	}
}
