using System;
using System.Collections;

public class SaveSlotManager
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SaveSlot, int> _003C_003E9__11_0;

		internal int _003CSetupNewSlot_003Eb__11_0(SaveSlot t)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass13_0
	{
		public int indexZI;

		internal void _003CConfirmSwitchToSlotIndex_003Eb__0(int index)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass14_0
	{
		public SaveSlotManager _003C_003E4__this;

		public int indexZI;

		internal void _003CConfirmDeleteSlotIndex_003Eb__0(int index)
		{
		}
	}

	public static SaveSlotManager Manager;

	public ArrayList SlotArray;

	public int CurrentSlotIndexZI;

	public bool WillSetUpNewSlotForNextNewLife;

	private Action<int> m_OnDeleteSlot;

	public event Action<int> OnDeleteSlot
	{
		add
		{
		}
		remove
		{
		}
	}

	public void UpdateCurrentSlotWithPerson(SimPerson person)
	{
	}

	public void SaveData()
	{
	}

	public void LoadData()
	{
	}

	public void SetupNewSlot()
	{
	}

	public void DeleteSlotIndex(int indexZI)
	{
	}

	public void ConfirmSwitchToSlotIndex(int indexZI)
	{
	}

	public void ConfirmDeleteSlotIndex(int indexZI)
	{
	}

	public SaveSlot GetSlotForIndex(int index)
	{
		return null;
	}

	public ArrayList GetAllLiveSlots()
	{
		return null;
	}

	public string GetSaveFolderName()
	{
		return null;
	}

	public void DeleteFiles(string fileName)
	{
	}
}
