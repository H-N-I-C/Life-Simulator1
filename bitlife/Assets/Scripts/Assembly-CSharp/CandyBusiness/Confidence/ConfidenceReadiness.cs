using System;
using System.Collections.Generic;
using UnityOpenFeature.Client;
using UnityOpenFeature.Core;

namespace CandyBusiness.Confidence
{
	public sealed class ConfidenceReadiness
	{
		private interface ISubscription
		{
			string AccountId { get; }

			void Deliver(IFeatureClient resolvedClient);
		}

		private sealed class Subscription<T> : ISubscription, IDisposable
		{
			private readonly ConfidenceReadiness owner;

			private readonly Func<IFeatureClient, ResolutionDetails<T>> evaluate;

			private readonly Func<T, bool> validate;

			private readonly Action<T> ready;

			private bool disposed;

			private readonly string AccountId__BackingField;

			public string AccountId => AccountId__BackingField;

			public Subscription(ConfidenceReadiness owner, string accountId, Func<IFeatureClient, ResolutionDetails<T>> evaluate, Func<T, bool> validate, Action<T> ready)
			{
			}

			public void Deliver(IFeatureClient resolvedClient)
			{
			}

			public void Dispose()
			{
			}
		}

		private static readonly ConfidenceReadiness Shared__BackingField;

		private readonly List<ISubscription> subscriptions;

		private IFeatureClient client;

		private string accountId;

		private int generation;

		private string LastFailure__BackingField;

		public static ConfidenceReadiness Shared => Shared__BackingField;

		public string LastFailure
		{
			get
			{
				return LastFailure__BackingField;
			}
			private set
			{
				LastFailure__BackingField = value;
			}
		}

		public int BeginResolution(string playerId)
		{
			return 0;
		}

		public void Invalidate(string reason = null)
		{
		}

		public void Fail(int requestGeneration, string reason)
		{
		}

		public void Publish(int requestGeneration, string playerId, IFeatureClient resolvedClient)
		{
		}

		public IDisposable WhenResolved<T>(string playerId, Func<IFeatureClient, ResolutionDetails<T>> evaluate, Func<T, bool> validate, Action<T> ready)
		{
			return null;
		}

		private void TryDeliver(ISubscription subscription)
		{
		}
	}
}
