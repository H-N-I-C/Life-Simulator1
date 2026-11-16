using UnityEngine;

namespace BitLife.UI.Managers
{
	public class UIEmoticonEmitterManager : MonoBehaviour
	{
		private const float WIDTH = 1242f;

		private const float MARGIN = 100f;

		private float TOP;

		private float BOTTOM;

		private UIEmoticon[] icons;

		private UIEmoticon iconTemplate;

		private Sprite spriteA;

		private Sprite spriteB;

		private float lifeTime;

		private float maxSpeed;

		private float minSpeed;

		private float maxRotationSpeed;

		private float startRotationSpeed;

		private float endRotationSpeed;

		private float maxScale;

		private float minScale;

		private int count;

		private void Start()
		{
		}

		private void SetupForConfetti()
		{
		}

		public void StartEmitting(SCENARIO_BACKGROUND_EFFECT bgEffect)
		{
		}

		private void EmitConfetti()
		{
		}
	}
}
