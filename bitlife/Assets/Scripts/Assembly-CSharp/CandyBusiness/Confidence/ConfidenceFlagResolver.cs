using System;
using System.Collections.Generic;
using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence
{
	public static class ConfidenceFlagResolver
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<ConfidenceVariant, ConfidenceFlag> _003C_003E9__41_0;

			public static Func<ConfidenceVariant, ConfidenceVariant> _003C_003E9__41_1;

			internal ConfidenceFlag _003CGatherVariantsByFlag_003Eb__41_0(ConfidenceVariant x)
			{
				return null;
			}

			internal ConfidenceVariant _003CGatherVariantsByFlag_003Eb__41_1(ConfidenceVariant x)
			{
				return null;
			}
		}

		private static bool? LastResolutionSucceeded__BackingField;

		private static bool HasResolvedFlags__BackingField;

		private static bool HasLoadedFlags__BackingField;

		private static bool Initialized__BackingField;

		private static Dictionary<ConfidenceFlag, ConfidenceVariant> Variants__BackingField;

		private static Action OnFlagsUpdated;

		private static Action<bool> OnResolutionCompleted;

		private static readonly HashSet<IConfidenceProperty> RegisteredProperties;

		public static bool FlagsResolved => false;

		public static bool? LastResolutionSucceeded
		{
			get
			{
				return LastResolutionSucceeded__BackingField;
			}
			private set
			{
				LastResolutionSucceeded__BackingField = value;
			}
		}

		private static bool HasResolvedFlags
		{
			get
			{
				return HasResolvedFlags__BackingField;
			}
			set
			{
				HasResolvedFlags__BackingField = value;
			}
		}

		private static bool HasLoadedFlags
		{
			get
			{
				return HasLoadedFlags__BackingField;
			}
			set
			{
				HasLoadedFlags__BackingField = value;
			}
		}

		private static bool Initialized
		{
			get
			{
				return Initialized__BackingField;
			}
			set
			{
				Initialized__BackingField = value;
			}
		}

		public static Dictionary<ConfidenceFlag, ConfidenceVariant> Variants
		{
			get
			{
				return Variants__BackingField;
			}
			private set
			{
				Variants__BackingField = value;
			}
		}

		public static void RegisterProperty(IConfidenceProperty property)
		{
		}

		public static void UnregisterProperty(IConfidenceProperty property)
		{
		}

		internal static IReadOnlyList<IConfidenceProperty> GetRegisteredProperties()
		{
			return null;
		}

		public static void Initialize()
		{
		}

		public static void Release()
		{
		}

		public static void AddResolutionCompletedListener(Action<bool> callback)
		{
		}

		public static void RemoveResolutionCompletedListener(Action<bool> callback)
		{
		}

		internal static void ReportResolutionFailure()
		{
		}

		private static void CompleteResolution(bool succeeded)
		{
		}

		internal static void InvalidateLiveFlags()
		{
		}

		public static void AddFlagsResolvedListener(Action callback)
		{
		}

		public static void RemoveFlagsResolvedListener(Action callback)
		{
		}

		private static void OnRuntimeFlagsResolved(ConfidenceRuntime confidence)
		{
		}

		private static void ResolveProperties(ConfidenceRuntime confidence)
		{
		}

		public static void LoadFlagProperties(ConfidenceSnapshot snapshot)
		{
		}

		private static void UpdateVariants()
		{
		}

		private static void GatherVariantsByFlag()
		{
		}
	}
}
