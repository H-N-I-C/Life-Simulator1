using UnityEngine;

namespace BitLife.UI.Interfaces
{
	public interface IguiObject
	{
		Vector2 GetActualSize();

		void Constructor(IguiOptions boxedOptions);

		void SetHeightConstraint();

		void SetSize(Vector2 size);
	}
}
