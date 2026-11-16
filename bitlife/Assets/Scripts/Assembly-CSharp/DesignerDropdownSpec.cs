using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class DesignerDropdownSpec
{
	private sealed class _003CGetAll_003Ed__16 : IEnumerable<DesignerDropdown>, IEnumerable, IEnumerator<DesignerDropdown>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private DesignerDropdown _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public DesignerDropdownSpec _003C_003E4__this;

		DesignerDropdown IEnumerator<DesignerDropdown>.Current => null;

		object IEnumerator.Current => null;

		public _003CGetAll_003Ed__16(int _003C_003E1__state)
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

		IEnumerator<DesignerDropdown> IEnumerable<DesignerDropdown>.GetEnumerator()
		{
			return null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
	}

	private DesignerDropdown Dropdown1__BackingField;

	private DesignerDropdown Dropdown2__BackingField;

	private DesignerDropdown Dropdown3__BackingField;

	private DesignerDropdown Dropdown4__BackingField;

	public DesignerDropdown Dropdown1
	{
		get
		{
			return Dropdown1__BackingField;
		}
		set
		{
			Dropdown1__BackingField = value;
		}
	}

	public DesignerDropdown Dropdown2
	{
		get
		{
			return Dropdown2__BackingField;
		}
		set
		{
			Dropdown2__BackingField = value;
		}
	}

	public DesignerDropdown Dropdown3
	{
		get
		{
			return Dropdown3__BackingField;
		}
		set
		{
			Dropdown3__BackingField = value;
		}
	}

	public DesignerDropdown Dropdown4
	{
		get
		{
			return Dropdown4__BackingField;
		}
		set
		{
			Dropdown4__BackingField = value;
		}
	}

	public IEnumerable<DesignerDropdown> GetAll()
	{
		return null;
	}
}
