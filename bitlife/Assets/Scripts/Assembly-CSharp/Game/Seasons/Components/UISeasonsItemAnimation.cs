using System.Collections.Generic;
using Game.Seasons.Controllers;
using UnityEngine;

namespace Game.Seasons.Components
{
	public class UISeasonsItemAnimation : MonoBehaviour
	{
		private const string pulseAnimationName = "pulse";

		private Animator animator;

		private UISeasonsItem uiPanel;

		private bool blockUi;

		private ParticleSystem particleSystem;

		private bool forceRender;

		private Dictionary<SeasonsItemType, string> animationActions;

		private bool _forceRenderEnabled;

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		private void OnPanelAnimationAction(SeasonsItemType panelType, SeasonPanelAnimationAction action)
		{
		}

		private void BitsAnimation(SeasonPanelAnimationAction action)
		{
		}

		private void Update()
		{
		}

		private void CancelForceRender()
		{
		}

		private void AnimationEnd()
		{
		}

		private void PlayParticles()
		{
		}

		private void PulseEnd()
		{
		}
	}
}
