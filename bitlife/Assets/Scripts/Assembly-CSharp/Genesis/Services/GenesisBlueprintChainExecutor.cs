using System;
using System.Collections.Generic;
using BitLife.UI.Managers;
using Game.Genesis.BluePrints;
using Genesis.Controllers;
using Genesis.Entities;

namespace Genesis.Services
{
	public static class GenesisBlueprintChainExecutor
	{
		private sealed class PreparedStep
		{
			internal string Raw;

			internal IBluePrint Blueprint;

			internal bool IsPlainScenarioRoute;

			internal bool IsTerminalRoutingBlueprint;

			internal bool IsDeferredCommitBlueprint;

			internal bool IsFireAndForgetDeferred;
		}

		public sealed class Result
		{
			private bool Succeeded__BackingField;

			private bool Aborted__BackingField;

			private bool CompletedPureBlueprintChain__BackingField;

			private bool HandledTerminalRouting__BackingField;

			private Scenario Scenario__BackingField;

			private string FailureSegment__BackingField;

			private string FailureReason__BackingField;

			public bool Succeeded
			{
				get
				{
					return Succeeded__BackingField;
				}
				internal set
				{
					Succeeded__BackingField = value;
				}
			}

			public bool Aborted
			{
				get
				{
					return Aborted__BackingField;
				}
				internal set
				{
					Aborted__BackingField = value;
				}
			}

			public bool CompletedPureBlueprintChain
			{
				get
				{
					return CompletedPureBlueprintChain__BackingField;
				}
				internal set
				{
					CompletedPureBlueprintChain__BackingField = value;
				}
			}

			public bool HandledTerminalRouting
			{
				get
				{
					return HandledTerminalRouting__BackingField;
				}
				internal set
				{
					HandledTerminalRouting__BackingField = value;
				}
			}

			public Scenario Scenario
			{
				get
				{
					return Scenario__BackingField;
				}
				internal set
				{
					Scenario__BackingField = value;
				}
			}

			public string FailureSegment
			{
				get
				{
					return FailureSegment__BackingField;
				}
				internal set
				{
					FailureSegment__BackingField = value;
				}
			}

			public string FailureReason
			{
				get
				{
					return FailureReason__BackingField;
				}
				internal set
				{
					FailureReason__BackingField = value;
				}
			}
		}

		private sealed class StateSnapshot
		{
			internal Life Life;

			internal Life.GenesisFeatureContainerTransactionSnapshot FeatureContainers;

			internal GenesisEntityContainer EntityContainer;

			internal GenesisEntityContainer.TransactionSnapshot Entities;

			internal GenesisMemory Memory;

			internal GenesisMemory.TransactionSnapshot MemoryState;

			internal string FocusedEntityId;

			internal string FocusedContainerId;

			internal string FocusedCollectionName;

			internal UIBackButtonManager BackButtonManager;

			internal List<string> Breadcrumbs;

			internal bool RecordedBreadcrumbs;

			internal static StateSnapshot Capture(ScenarioController controller)
			{
				return null;
			}

			internal void Restore()
			{
			}
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Predicate<PreparedStep> _003C_003E9__12_0;

			internal bool _003CTryPrepare_003Eb__12_0(PreparedStep candidate)
			{
				return false;
			}
		}

		private static readonly HashSet<Type> SupportedMutationBlueprintTypes;

		private static readonly HashSet<Type> SupportedTerminalRoutingBlueprintTypes;

		private static readonly HashSet<Type> SupportedCommitOnlyMutationBlueprintTypes;

		private static readonly HashSet<Type> SupportedDeferredCommitBlueprintTypes;

		private static readonly HashSet<Type> SupportedFireAndForgetDeferredBlueprintTypes;

		public static bool TryExecuteAtomic(string chain, string scenarioFile, ScenarioController controller, string followUpScenario, out Result result)
		{
			result = null;
			return false;
		}

		private static bool ExecuteBlueprintStep(PreparedStep step, string followUpScenario, Result execution)
		{
			return false;
		}

		private static Result FlushRollbackCapableDeferredSteps(List<PreparedStep> deferredSteps, StateSnapshot snapshot, string followUpScenario, Result execution, ref string currentSegment, Action<string> onStepSucceeded, Action<string, string> onRolledBack)
		{
			return null;
		}

		private static void FlushFireAndForgetDeferredSteps(List<PreparedStep> deferredSteps, string followUpScenario, Result execution, ref string currentSegment)
		{
		}

		private static bool TryPrepare(string chain, ScenarioController controller, out List<PreparedStep> steps, out bool isTransactional, out Result failure)
		{
			steps = null;
			isTransactional = default;
			failure = null;
			return false;
		}

		private static Result RollBack(StateSnapshot snapshot, Result result, string segment, string reason, Action<string, string> onRolledBack)
		{
			return null;
		}

		private static Result Failure(string segment, string reason)
		{
			return null;
		}
	}
}
