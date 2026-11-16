using UnityEngine;

namespace Gadsme
{
	public class GadsmeInputBackend : IGadsmeInputBackend
	{
		public void Update()
		{
		}

		public bool PointerJustPressed()
		{
			return false;
		}

		public bool PointerJustReleased()
		{
			return false;
		}

		public Vector2 PointerPosition()
		{
			return default;
		}

		public GameObject CreateEventSystem()
		{
			return null;
		}
	}
}
