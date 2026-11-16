using System.Collections.Generic;
using Genesis.Controllers;

namespace Genesis.Entities
{
	public sealed class GenesisEntityTransientMemoryScope
	{
		private struct PriorValue
		{
			internal readonly bool Existed;

			internal readonly object Value;

			internal PriorValue(bool existed, object value)
			{
				Existed = false;
				Value = null;
			}
		}

		private readonly GenesisMemory _memory;

		private readonly Dictionary<string, PriorValue> _priorValues;

		private bool _restored;

		public IReadOnlyCollection<string> Keys => null;

		internal GenesisEntityTransientMemoryScope(GenesisMemory memory)
		{
		}

		internal void SetValue(string key, object value)
		{
		}

		public void Restore()
		{
		}
	}
}
