using System;

public sealed class SROptionEntry
{
	public enum EntryKind
	{
		Action = 0,
		Value = 1,
		Collection = 2
	}

	private sealed class _003C_003Ec__DisplayClass36_0<T>
	{
		public Func<T> getter;

		public Action<T> setter;

		internal object _003CValue_003Eb__0()
		{
			return null;
		}

		internal void _003CValue_003Eb__1(object value)
		{
		}
	}

	private readonly string Name__BackingField;

	private readonly EntryKind Kind__BackingField;

	private readonly int? Sort__BackingField;

	private readonly Action Action__BackingField;

	private readonly Type ValueType__BackingField;

	private readonly Func<object> Getter__BackingField;

	private readonly Action<object> Setter__BackingField;

	private readonly bool IsReadOnly__BackingField;

	private readonly double? Min__BackingField;

	private readonly double? Max__BackingField;

	private readonly double? Increment__BackingField;

	public string Name => Name__BackingField;

	public EntryKind Kind => Kind__BackingField;

	public int? Sort => Sort__BackingField;

	public Action Action => Action__BackingField;

	public Type ValueType => ValueType__BackingField;

	public Func<object> Getter => Getter__BackingField;

	public Action<object> Setter => Setter__BackingField;

	public bool IsReadOnly => IsReadOnly__BackingField;

	public double? Min => Min__BackingField;

	public double? Max => Max__BackingField;

	public double? Increment => Increment__BackingField;

	private SROptionEntry(string name, EntryKind kind, Action action, Type valueType, Func<object> getter, Action<object> setter, bool isReadOnly, double? min, double? max, double? increment, int? sort)
	{
	}

	public static SROptionEntry Button(string name, Action callback, int? sort = null)
	{
		return null;
	}

	public static SROptionEntry Value<T>(string name, Func<T> getter, Action<T> setter = null, double? min = null, double? max = null, double? increment = null, bool isReadOnly = false, int? sort = null)
	{
		return null;
	}

	public static SROptionEntry Value(string name, Type valueType, Func<object> getter, Action<object> setter = null, double? min = null, double? max = null, double? increment = null, bool isReadOnly = false, int? sort = null)
	{
		return null;
	}

	public static SROptionEntry Collection(string name, Type collectionType, Func<object> getter, bool isReadOnly = false, int? sort = null)
	{
		return null;
	}
}
