using System;
using System.Collections.Generic;

namespace CandyBusiness.Confidence
{
	public abstract class ConfidenceAttribute : IEquatable<ConfidenceAttribute>
	{
		private Action<ConfidenceAttribute> m_OnAttributeValueChanged;

		private readonly ConfidenceAttributeName Name__BackingField;

		private bool HasValue__BackingField;

		private bool HasExplicitValue__BackingField;

		private Action<ConfidenceAttribute> m_OnValueAssigned;

		public ConfidenceAttributeName Name => Name__BackingField;

		public abstract object ValueObject { get; }

		public abstract Type ValueType { get; }

		public bool HasValue
		{
			get
			{
				return HasValue__BackingField;
			}
			protected set
			{
				HasValue__BackingField = value;
			}
		}

		public bool HasExplicitValue
		{
			get
			{
				return HasExplicitValue__BackingField;
			}
			protected set
			{
				HasExplicitValue__BackingField = value;
			}
		}

		public event Action<ConfidenceAttribute> OnAttributeValueChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		public event Action<ConfidenceAttribute> OnValueAssigned
		{
			add
			{
			}
			remove
			{
			}
		}

		protected ConfidenceAttribute(string name)
		{
		}

		protected ConfidenceAttribute(ConfidenceAttributeName name)
		{
		}

		public abstract object CopyValueObject();

		public abstract bool Hydrate(ConfidenceSnapshot.LocalState state);

		public abstract void ResetState();

		protected void InvokeAttributeChanged()
		{
		}

		protected void MarkExplicitlyAssigned(bool notify)
		{
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public bool Equals(ConfidenceAttribute other)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}
	}
	public sealed class ConfidenceAttribute<T> : ConfidenceAttribute
	{
		private sealed class StringSetEqualityComparer : IEqualityComparer<T>
		{
			public bool Equals(T x, T y)
			{
				return false;
			}

			public int GetHashCode(T obj)
			{
				return 0;
			}
		}

		private Action<T, T> m_OnValueChanged;

		private readonly IEqualityComparer<T> _comparer;

		private readonly Func<T, bool> _isUnknown;

		private readonly bool _hasInitialValue;

		private readonly T _initialValue;

		private T _backingValue;

		public T Value
		{
			get
			{
				return default;
			}
			set
			{
			}
		}

		public override object ValueObject => null;

		public override Type ValueType => null;

		public event Action<T, T> OnValueChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		public ConfidenceAttribute(string attributeName, IEqualityComparer<T> comparer = null, Func<T, bool> isUnknown = null)
			: base((string)null)
		{
		}

		public ConfidenceAttribute(ConfidenceAttributeName attributeName, IEqualityComparer<T> comparer = null, Func<T, bool> isUnknown = null)
			: base((string)null)
		{
		}

		public ConfidenceAttribute(string attributeName, T initialValue, IEqualityComparer<T> comparer = null, Func<T, bool> isUnknown = null)
			: base((string)null)
		{
		}

		public ConfidenceAttribute(ConfidenceAttributeName attributeName, T initialValue, IEqualityComparer<T> comparer = null, Func<T, bool> isUnknown = null)
			: base((string)null)
		{
		}

		public bool IsUnknown(T value)
		{
			return false;
		}

		public bool TrySet(T newValue)
		{
			return false;
		}

		public void SetValueWithoutNotify(T newValue)
		{
		}

		public bool Hydrate(T value)
		{
			return false;
		}

		public override bool Hydrate(ConfidenceSnapshot.LocalState state)
		{
			return false;
		}

		public override object CopyValueObject()
		{
			return null;
		}

		public override void ResetState()
		{
		}

		private void ApplyInitialState()
		{
		}

		private void InvokeValueChanged(T oldValue, T newValue)
		{
		}

		private static IEqualityComparer<T> CreateDefaultComparer()
		{
			return null;
		}
	}
}
