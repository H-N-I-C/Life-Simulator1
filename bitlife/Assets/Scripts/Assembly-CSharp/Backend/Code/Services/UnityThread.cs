using System;
using System.Threading;

namespace Backend.Code.Services
{
	public static class UnityThread
	{
		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public Action action;

			internal void _003CRunOnMainThread_003Eb__0(object _)
			{
			}
		}

		private static SynchronizationContext _context;

		private static void Initialize()
		{
		}

		public static void RunOnMainThread(Action action)
		{
		}
	}
}
