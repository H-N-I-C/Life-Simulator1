using System;
using System.Threading;
using UnityEngine;

namespace CandyBusiness.Confidence.Dependencies
{
	public abstract class ConfidenceDependencyAction : IDisposable
	{
		public abstract bool IsComplete { get; }

		public abstract Awaitable WaitAsync(CancellationToken token);

		public virtual void Dispose()
		{
		}
	}
}
