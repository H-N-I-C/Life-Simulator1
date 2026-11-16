using System;
using System.Collections.Generic;
using BitLife.UI.Data;
using Genesis.Controllers;
using Genesis.Lists.Models;
using UnityEngine;

namespace Genesis.Lists.Runtime
{
	public static class GenesisListBuilder
	{
		private enum IapGateOutcome
		{
			Normal = 0,
			Hide = 1,
			Seal = 2
		}

		private static class GenesisRemoteImageBinder
		{
			private static readonly Dictionary<string, List<WeakReference<ListItem>>> PendingByContentId;

			private static bool _subscribed;

			public static void Bind(ListItem item, string contentId)
			{
			}

			private static void EnsureSubscribed()
			{
			}

			private static void OnTextureDownloaded(string contentId)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass12_0
		{
			public GenesisListItemRuntimeContext runtimeContext;

			public string listResourceName;

			public string[] capturedSetupRoutes;

			internal List _003CTryBuildDeferredSelectionList_003Eb__0()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass3_0
		{
			public GenesisListItemRuntimeContext runtimeContext;

			internal void _003CBuildListItem_003Eb__0()
			{
			}
		}

		private static readonly Dictionary<string, THEME_IMAGE> PresetImageIds;

		public static List Build(GenesisListDefinition definition, ScenarioController controller, GenesisListInvocationSource invocationSource = GenesisListInvocationSource.RootShow)
		{
			return null;
		}

		private static void BuildSectioned(List list, GenesisListDefinition definition, ScenarioController controller)
		{
		}

		private static GenesisListDefinition CreateSectionDataSourceDefinition(GenesisListDefinition parent, GenesisListSection section)
		{
			return null;
		}

		private static ListItem BuildListItem(GenesisListItemDefinition definition, ScenarioController controller, GenesisListDefinition listDefinition, object focusObject = null, string focusMemoryKey = null)
		{
			return null;
		}

		private static IapGateOutcome EvaluateIapGate(GenesisListItemDefinition definition, out int sealOrigin)
		{
			sealOrigin = default;
			return IapGateOutcome.Normal;
		}

		private static void ApplySeal(ListItem item, int sealOrigin)
		{
		}

		private static int ResolveSealOrigin(string paywallIapOrigin, string gate)
		{
			return 0;
		}

		private static bool TryParseIapOrigin(string raw, out int origin)
		{
			origin = default;
			return false;
		}

		private static bool TryBuildSpecialItem(GenesisListItemDefinition definition, ScenarioController controller, string injectedTitle, object focusObject, string focusMemoryKey, out ListItem item)
		{
			item = null;
			return false;
		}

		private static void BuildDataSourceItems(List list, GenesisListDefinition definition, ScenarioController controller)
		{
		}

		private static bool TryBuildEagerSelectionList(IList<ScenarioAction> actionList, ScenarioController controller, GenesisListDefinition currentListDefinition, out List selectionList)
		{
			selectionList = null;
			return false;
		}

		private static bool TryBuildDeferredSelectionList(IList<ScenarioAction> actionList, GenesisListDefinition currentListDefinition, GenesisListItemRuntimeContext runtimeContext, out DeferredSelectionList selectionListProvider)
		{
			selectionListProvider = null;
			return false;
		}

		private static bool TryClassifySelectionListRoute(IList<ScenarioAction> actionList, GenesisListDefinition currentListDefinition, out string listResourceName, out List<string> focusSetupRoutes)
		{
			listResourceName = null;
			focusSetupRoutes = null;
			return false;
		}

		private static bool CanAttemptEagerSelectionListBuild(GenesisListItemDefinition definition, bool sealRow, object focusObject, string focusMemoryKey)
		{
			return false;
		}

		private static bool CanAttemptDeferredSelectionListBuild(GenesisListItemDefinition definition, bool sealRow)
		{
			return false;
		}

		private static bool RequiresDeferredSelectionListBuild(ICollection<string> focusSetupRoutes, GenesisListItemRuntimeContext runtimeContext)
		{
			return false;
		}

		private static bool IsSimpleListRoute(ScenarioCondition condition)
		{
			return false;
		}

		private static bool TryGetDeterministicFocusSetupRoute(ScenarioCondition condition, out string setupRoute)
		{
			setupRoute = null;
			return false;
		}

		private static bool IsDeterministicFocusOnlyRoute(string route)
		{
			return false;
		}

		private static bool IsValidFocusSource(string source)
		{
			return false;
		}

		private static bool IsSafeFocusToken(string value)
		{
			return false;
		}

		private static bool IsNullOrEmpty<T>(ICollection<T> collection)
		{
			return false;
		}

		private static void PrepareActionList(IEnumerable<ScenarioAction> actions, ScenarioController controller)
		{
		}

		private static void ApplyBarGraph(ListItem item, GenesisListBarGraphDefinition barGraph, ScenarioController controller)
		{
		}

		private static void ApplyButtonStyleOverride(ListItem item, GenesisListItemDefinition definition, ScenarioController controller)
		{
		}

		private static void ApplyImageOverride(ListItem item, string overrideId, ScenarioController controller)
		{
		}

		private static bool TryResolvePresetImage(string overrideId, out Sprite sprite)
		{
			sprite = null;
			return false;
		}

		private static bool TryResolveRealEmoji(string emojiExpression, out Sprite sprite)
		{
			sprite = null;
			return false;
		}

		private static Sprite EmojiPlaceholder()
		{
			return null;
		}

		private static void ApplyRemoteImageOverride(ListItem item, string contentId)
		{
		}

		private static Sprite CreateRemoteSprite(string contentId)
		{
			return null;
		}

		private static void ApplyEmojiButtonStyle(ListItem item, string emojiExpression)
		{
		}

		private static float EvaluateFloat(ScenarioController controller, string valueExpression)
		{
			return 0f;
		}

		private static BAR_GRAPH_COLOR_SCHEME ParseColorScheme(string style)
		{
			return BAR_GRAPH_COLOR_SCHEME.LOW_IS_RED;
		}

		private static string ResolveItemIcon(ScenarioController controller, string icon)
		{
			return null;
		}

		private static string InjectString(ScenarioController controller, string value)
		{
			return null;
		}

		private static SimPerson ResolveFocusedPerson(ScenarioController controller, object focusObject, string focusMemoryKey)
		{
			return null;
		}
	}
}
