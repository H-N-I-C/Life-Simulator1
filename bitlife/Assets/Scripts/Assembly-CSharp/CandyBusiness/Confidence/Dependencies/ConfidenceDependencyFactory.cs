using System;

namespace CandyBusiness.Confidence.Dependencies
{
	public static class ConfidenceDependencyFactory
	{
		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public ConfidenceAttribute attribute;

			internal void _003CBuildAttributeDependencySet_003Eb__0(Action<ConfidenceAttribute> h)
			{
			}

			internal void _003CBuildAttributeDependencySet_003Eb__1(Action<ConfidenceAttribute> h)
			{
			}

			internal bool _003CBuildAttributeDependencySet_003Eb__2()
			{
				return false;
			}
		}

		private const float GameReadyTimeoutSeconds = 20f;

		public static ConfidenceDependencySet BuildGameReadyDependencySet()
		{
			return null;
		}

		public static ConfidenceDependencySet BuildAttributeDependencySet()
		{
			return null;
		}

		private static bool IsGameReady()
		{
			return false;
		}
	}
}
