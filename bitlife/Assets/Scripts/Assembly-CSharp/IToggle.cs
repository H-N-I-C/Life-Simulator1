using UnityEngine.Events;

public interface IToggle
{
	void SetOnClickDelegateMethodChoiceIndex(int index);

	void SetOnClickDelegateMethod(UnityAction<int> callback);

	void SetButtonSound(SOUND sound);

	void Select();

	void Unselect();

	void Setup();
}
