using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityOpenFeature.Client;
using UnityOpenFeature.Providers;

namespace CandyBusiness.Confidence
{
	public sealed class ConfidenceRuntime : IDisposable
	{
		private sealed class _003C_003Ec__DisplayClass23_0
		{
			public ConfidenceRuntime _003C_003E4__this;

			public int requestId;

			public string requestAccount;

			public TaskCompletionSource<bool> completion;

			internal void _003CResolveAsync_003Eg__Callback_007C0(bool success, string error)
			{
			}
		}

		private static Action<ConfidenceRuntime> m_OnFlagsResolved;

		private IFeatureClient Client__BackingField;

		private bool HasResolved__BackingField;

		private ConfidenceProvider _provider;

		private bool _disposed;

		private int _requestId;

		private int _readinessGeneration;

		private string _requestAccount;

		private TaskCompletionSource<bool> _pendingResolution;

		public IFeatureClient Client
		{
			get
			{
				return Client__BackingField;
			}
			private set
			{
				Client__BackingField = value;
			}
		}

		public bool HasResolved
		{
			get
			{
				return HasResolved__BackingField;
			}
			private set
			{
				HasResolved__BackingField = value;
			}
		}

		public static event Action<ConfidenceRuntime> OnFlagsResolved
		{
			add
			{
			}
			remove
			{
			}
		}

		public bool GetBool(string flagKey, bool defaultValue = false)
		{
			return false;
		}

		public string GetString(string flagKey, string defaultValue = "")
		{
			return null;
		}

		public int GetInt(string flagKey, int defaultValue = 0)
		{
			return 0;
		}

		public float GetFloat(string flagKey, float defaultValue = 0f)
		{
			return 0f;
		}

		public T GetObject<T>(string flagKey, T defaultValue = default(T))
		{
			return default;
		}

		public Dictionary<string, T> GetDictionary<T>(string flagKey)
		{
			return null;
		}

		public Task<bool> ResolveAsync(Dictionary<string, object> attributes, bool allowRepeat = false)
		{
			return null;
		}

		internal void PublishReadiness()
		{
		}

		public void CancelPendingResolution()
		{
		}

		public void Dispose()
		{
		}

		private void ResolveFlags(bool success, string error)
		{
		}
	}
}
