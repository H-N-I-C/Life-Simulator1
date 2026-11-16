using System;
using System.Threading;

namespace Game.Translation
{
	public sealed class TranslationRequestContext : IDisposable
	{
		private readonly CancellationTokenSource cancellation;

		private readonly CancellationToken token;

		private bool disposed;

		private readonly Func<bool> owner;

		private readonly double deadline;

		private readonly int generation;

		private readonly Life life;

		public CancellationToken Token => default;

		public double RemainingSeconds => 0.0;

		internal bool ConfigurationIsCurrent => false;

		internal bool OwnerIsCurrent => false;

		public bool IsCurrent => false;

		internal TranslationRequestContext(Func<bool> owner = null)
		{
		}

		internal TranslationRequestContext RenewForRetry()
		{
			return null;
		}

		public void Dispose()
		{
		}
	}
}
