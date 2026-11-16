using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MoreMountains.NiceVibrations
{
	public static class MMVibrationManager
	{
		public static long LightDuration;

		public static long MediumDuration;

		public static long HeavyDuration;

		public static int LightAmplitude;

		public static int MediumAmplitude;

		public static int HeavyAmplitude;

		private static int _sdkVersion;

		private static long[] _successPattern;

		private static int[] _successPatternAmplitude;

		private static long[] _warningPattern;

		private static int[] _warningPatternAmplitude;

		private static long[] _failurePattern;

		private static int[] _failurePatternAmplitude;

		private static AndroidJavaObject AndroidVibrator;

		private static AndroidJavaClass VibrationEffectClass;

		private static AndroidJavaObject VibrationEffect;

		private static IntPtr AndroidVibrateMethodRawClass;

		private static jvalue[] AndroidVibrateMethodRawClassParameters;

		private static bool iOSHapticsInitialized;

		public static bool Android()
		{
			return false;
		}

		public static bool iOS()
		{
			return false;
		}

		public static void Haptic(HapticTypes type)
		{
		}

		public static void AndroidVibrate(long milliseconds)
		{
		}

		public static void AndroidVibrate(long milliseconds, int amplitude)
		{
		}

		public static void AndroidVibrate(long[] pattern, int[] amplitudes, int repeat)
		{
		}

		private static void VibrationEffectClassInitialization()
		{
		}

		public static int AndroidSDKVersion()
		{
			return 0;
		}

		[PreserveSig]
		private static extern void InstantiateFeedbackGenerators();

		[PreserveSig]
		private static extern void SelectionHaptic();

		[PreserveSig]
		private static extern void SuccessHaptic();

		[PreserveSig]
		private static extern void WarningHaptic();

		[PreserveSig]
		private static extern void FailureHaptic();

		[PreserveSig]
		private static extern void LightImpactHaptic();

		[PreserveSig]
		private static extern void MediumImpactHaptic();

		[PreserveSig]
		private static extern void HeavyImpactHaptic();

		public static void iOSInitializeHaptics()
		{
		}

		public static bool HapticsSupported()
		{
			return false;
		}

		public static void iOSTriggerHaptics(HapticTypes type)
		{
		}
	}
}
