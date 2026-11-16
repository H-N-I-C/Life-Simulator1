using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitySingleton : BaseBehaviour
{
	private sealed class _003C_003Ec__DisplayClass14_0<S> where S : UnitySingleton
	{
		public Action<S> whatToDoWithInstanceWhenItsReady;

		internal void _003CDoWithExistingInstance_003Eb__0(UnitySingleton singleton)
		{
		}
	}

	private static readonly Dictionary<Type, UnitySingleton> instances;

	private static readonly Dictionary<Type, Dictionary<MonoBehaviour, Action<UnitySingleton>>> instanceListeners;

	private bool IsInstanceReady__BackingField;

	protected bool IsInstanceReady
	{
		get
		{
			return IsInstanceReady__BackingField;
		}
		private set
		{
			IsInstanceReady__BackingField = value;
		}
	}

	protected virtual bool DontDestroySingleton => false;

	private void RegisterAsSingleInstanceAndInit()
	{
	}

	private void InnerInit()
	{
	}

	private static S GetOrCreateInstanceOnGameObject<S>(Type type) where S : CodeGeneratedSingleton
	{
		return null;
	}

	private void NotifyInstanceListeners()
	{
	}

	protected void DeclareAsReady()
	{
	}

	protected static S GetSynchronousCodeGeneratedInstance<S>() where S : CodeGeneratedSingleton
	{
		return null;
	}

	public static void DoWithCodeGeneratedInstance<C>(MonoBehaviour sender, Action<C> whatToDoWithInstanceWhenItsReady) where C : CodeGeneratedSingleton
	{
	}

	public static void DoWithSceneInstance<S>(MonoBehaviour sender, Action<S> whatToDoWithInstanceWhenItsReady) where S : SceneSingleton
	{
	}

	private static void DoWithExistingInstance<S>(MonoBehaviour sender, Action<S> whatToDoWithInstanceWhenItsReady) where S : UnitySingleton
	{
	}

	protected sealed override void Start()
	{
	}

	protected virtual void SetReadyAndNotifyAfterRegistering()
	{
	}

	protected override void OnDestroy()
	{
	}

	protected virtual void InitAfterRegisteringAsSingleInstance()
	{
	}
}
