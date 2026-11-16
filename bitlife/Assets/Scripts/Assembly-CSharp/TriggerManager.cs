using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[Serializable]
public class TriggerManager
{
	private sealed class ValueAccessor
	{
		private readonly Func<object, int> _valueGetter;

		private string Name__BackingField;

		public string Name
		{
			get
			{
				return Name__BackingField;
			}
			private set
			{
				Name__BackingField = value;
			}
		}

		public ValueAccessor(string name, Func<object, int> valueGetter)
		{
		}

		public int GetValue(object trackedObject)
		{
			return 0;
		}
	}

	public interface ITriggerScenario
	{
		Scenario GetScenario(List<TriggerEvent> triggers);
	}

	private sealed class _003C_003Ec__DisplayClass16_0
	{
		public PropertyInfo cachedPropertyInfo;

		internal int _003CBuildAccessorsForType_003Eb__0(object trackedObject)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass16_1
	{
		public FieldInfo cachedFieldInfo;

		internal int _003CBuildAccessorsForType_003Eb__1(object trackedObject)
		{
			return 0;
		}
	}

	private sealed class _003CGetTrackedObjects_003Ed__14 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public TriggerManager _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CGetTrackedObjects_003Ed__14(int _003C_003E1__state)
		{
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
		}

		IEnumerator<object> IEnumerable<object>.GetEnumerator()
		{
			return null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
	}

	[NonSerialized]
	private Life _life;

	private static readonly object AccessorCacheLock;

	private static readonly Dictionary<Type, List<ValueAccessor>> AccessorCacheByType;

	private static readonly HashSet<string> ValidTriggerNames;

	private static readonly List<ITriggerScenario> TriggerScenarios;

	public TriggerManager()
	{
	}

	public TriggerManager(Life life)
	{
	}

	public void Attach(Life life)
	{
	}

	public void SeedCurrentValues()
	{
	}

	public void RegisterScenarioListener(ITriggerScenario trigger)
	{
	}

	public void RemoveScenarioListener(ITriggerScenario trigger)
	{
	}

	public List<Scenario> GetTriggerScenarios(List<TriggerEvent> evts)
	{
		return null;
	}

	public List<TriggerEvent> GetTriggers()
	{
		return null;
	}

	private Dictionary<string, int> CollectCurrentValues()
	{
		return null;
	}

	private IEnumerable<object> GetTrackedObjects()
	{
		return null;
	}

	private static List<ValueAccessor> GetAccessorsForType(Type type)
	{
		return null;
	}

	private static List<ValueAccessor> BuildAccessorsForType(Type type)
	{
		return null;
	}
}
