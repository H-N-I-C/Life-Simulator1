using System;
using CandyBusiness.Confidence.Flags;

namespace CandyBusiness.Confidence.Properties
{
	public class ConfidenceProperty<T> : IConfidenceProperty, IDisposable
	{
		private bool _loaded;

		private bool _disposed;

		private bool _resolved;

		private T _value;

		private Action<ConfidenceProperty<T>> m_OnValueChanged;

		private readonly ConfidenceFlag FlagRelation__BackingField;

		private readonly ConfidencePropertyName PropertyName__BackingField;

		private T DefaultValue__BackingField;

		public ConfidenceFlag FlagRelation => FlagRelation__BackingField;

		public ConfidencePropertyName PropertyName => PropertyName__BackingField;

		public T PropertyValue
		{
			get
			{
				return default;
			}
			private set
			{
			}
		}

		public string CacheKey => null;

		public object CacheValue => null;

		public T DefaultValue
		{
			get
			{
				return DefaultValue__BackingField;
			}
			set
			{
				DefaultValue__BackingField = value;
			}
		}

		public event Action<ConfidenceProperty<T>> OnValueChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		public ConfidenceProperty(ConfidenceFlag relatedTo, string propertyName)
		{
		}

		private ConfidenceProperty(ConfidenceFlag relatedTo, ConfidencePropertyName propertyName)
		{
		}

		public void Load(ConfidenceSnapshot snapshot)
		{
		}

		public void Resolve(ConfidenceRuntime runtime)
		{
		}

		private T ResolveEnum(ConfidenceRuntime runtime, string key)
		{
			return default;
		}

		public string GetPropertyKey()
		{
			return null;
		}

		public override string ToString()
		{
			return null;
		}

		public static implicit operator T(ConfidenceProperty<T> property)
		{
			return default;
		}

		public void Dispose()
		{
		}
	}
}
