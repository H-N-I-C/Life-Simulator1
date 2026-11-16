using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenModulePhysics
	{
		private sealed class _003C_003Ec__DisplayClass10_0
		{
			public Transform trans;

			public Rigidbody target;

			internal Vector3 _003CDOLocalPath_003Eb__0()
			{
				return default;
			}

			internal void _003CDOLocalPath_003Eb__1(Vector3 x)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass9_0
		{
			public Rigidbody target;

			internal Vector3 _003CDOPath_003Eb__0()
			{
				return default;
			}
		}

		internal static TweenerCore<Vector3, Path, PathOptions> DOPath(Rigidbody target, Path path, float duration, PathMode pathMode = PathMode.Full3D)
		{
			return null;
		}

		internal static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(Rigidbody target, Path path, float duration, PathMode pathMode = PathMode.Full3D)
		{
			return null;
		}
	}
}
