using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Backend.Data;
using Backend.Service;
using Game.Seasons.Models;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Purchasing;

namespace Backend.Aws
{
	public class AwsApi
	{
		private sealed class _003C_003Ec__DisplayClass21_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CGetAllPurchases_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass21_0 _003C_003E4__this;

				private string _003CjsonString_003E5__2;

				private bool _003Csuccess_003E5__3;

				private TaskAwaiter<(string, bool)> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public string userId;

			public Action<List<TransactionItemModel>> callback;

			internal Task _003CGetAllPurchases_003Eb__0()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass22_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CPutAllPurchases_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass22_0 _003C_003E4__this;

				private string _003Cmessage_003E5__2;

				private TaskAwaiter<(bool, string)> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public PutAllReceiptsRequest request;

			public Action<bool> callback;

			public string userId;

			internal Task _003CPutAllPurchases_003Eb__0()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass23_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CPutPurchase_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass23_0 _003C_003E4__this;

				private string _003Cmessage_003E5__2;

				private TaskAwaiter<(bool, string)> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public PutAllReceiptsRequest request;

			public bool isPurchase;

			public Action<bool> callback;

			public string userId;

			internal Task _003CPutPurchase_003Eb__0()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass51_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CGetSeasonData_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass51_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass51_1 _003C_003E8__1;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public Action<(string, bool)> result;

			public string seasonId;

			public bool needsMyStuffLoading;

			public Action _003C_003E9__1;

			internal Task _003CGetSeasonData_003Eb__0()
			{
				return null;
			}

			internal void _003CGetSeasonData_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass51_1
		{
			public string url;

			public Dictionary<string, string> headers;

			public _003C_003Ec__DisplayClass51_0 CS_0024_003C_003E8__locals1;

			internal void _003CGetSeasonData_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass51_2
		{
			public UnityWebRequest request;

			public _003C_003Ec__DisplayClass51_1 CS_0024_003C_003E8__locals2;

			internal void _003CGetSeasonData_003Eb__3(AsyncOperation _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass52_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CGetNarrativeContent_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass52_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass52_1 _003C_003E8__1;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public Action<(string, bool)> result;

			public string url;

			public Action _003C_003E9__1;

			internal Task _003CGetNarrativeContent_003Eb__0()
			{
				return null;
			}

			internal void _003CGetNarrativeContent_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass52_1
		{
			public Dictionary<string, string> headers;

			public _003C_003Ec__DisplayClass52_0 CS_0024_003C_003E8__locals1;

			internal void _003CGetNarrativeContent_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass52_2
		{
			public UnityWebRequest request;

			public _003C_003Ec__DisplayClass52_1 CS_0024_003C_003E8__locals2;

			internal void _003CGetNarrativeContent_003Eb__3(AsyncOperation _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass53_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CCompleteCommunityGoal_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass53_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass53_1 _003C_003E8__1;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public Action<bool> result;

			public string seasonId;

			public string objectiveId;

			public Action _003C_003E9__1;

			internal Task _003CCompleteCommunityGoal_003Eb__0()
			{
				return null;
			}

			internal void _003CCompleteCommunityGoal_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass53_1
		{
			public string url;

			public Dictionary<string, string> headers;

			public _003C_003Ec__DisplayClass53_0 CS_0024_003C_003E8__locals1;

			internal void _003CCompleteCommunityGoal_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass53_2
		{
			public UnityWebRequest request;

			public _003C_003Ec__DisplayClass53_1 CS_0024_003C_003E8__locals2;

			internal void _003CCompleteCommunityGoal_003Eb__3(AsyncOperation _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass54_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CSaveSeasonPlayerData_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass54_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass54_1 _003C_003E8__1;

				private HttpClient _003Cclient_003E5__2;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

				private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public Action<bool> result;

			public string seasonId;

			public string data;

			public Action _003C_003E9__1;

			internal Task _003CSaveSeasonPlayerData_003Eb__0()
			{
				return null;
			}

			internal void _003CSaveSeasonPlayerData_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass54_1
		{
			public HttpResponseMessage response;

			public _003C_003Ec__DisplayClass54_0 CS_0024_003C_003E8__locals1;

			internal void _003CSaveSeasonPlayerData_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass55_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CPurchaseSeasonProduct_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass55_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass55_1 _003C_003E8__1;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public Action<string> result;

			public string seasonId;

			public string productId;

			public int quantity;

			public Action _003C_003E9__1;

			internal Task _003CPurchaseSeasonProduct_003Eb__0()
			{
				return null;
			}

			internal void _003CPurchaseSeasonProduct_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass55_1
		{
			public string url;

			public Dictionary<string, string> headers;

			public _003C_003Ec__DisplayClass55_0 CS_0024_003C_003E8__locals1;

			internal void _003CPurchaseSeasonProduct_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass55_2
		{
			public UnityWebRequest request;

			public _003C_003Ec__DisplayClass55_1 CS_0024_003C_003E8__locals2;

			internal void _003CPurchaseSeasonProduct_003Eb__3(AsyncOperation _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass56_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CRedeemTrackItem_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass56_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass56_1 _003C_003E8__1;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public Action<string> result;

			public string seasonId;

			public string redeemId;

			public bool isPremium;

			public Action _003C_003E9__1;

			internal Task _003CRedeemTrackItem_003Eb__0()
			{
				return null;
			}

			internal void _003CRedeemTrackItem_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass56_1
		{
			public string url;

			public Dictionary<string, string> headers;

			public _003C_003Ec__DisplayClass56_0 CS_0024_003C_003E8__locals1;

			internal void _003CRedeemTrackItem_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass56_2
		{
			public UnityWebRequest request;

			public _003C_003Ec__DisplayClass56_1 CS_0024_003C_003E8__locals2;

			internal void _003CRedeemTrackItem_003Eb__3(AsyncOperation _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass64_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CSendDailyRequest_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass64_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass64_1 _003C_003E8__1;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public string method;

			public string path;

			public string body;

			public string configuration;

			public Action<DailyApiResult> result;

			public int? debugMinutesOverride;

			public Action _003C_003E9__1;

			public Action _003C_003E9__4;

			internal Task _003CSendDailyRequest_003Eb__0()
			{
				return null;
			}

			internal void _003CSendDailyRequest_003Eb__1()
			{
			}

			internal void _003CSendDailyRequest_003Eb__4()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass64_1
		{
			public string url;

			public Dictionary<string, string> headers;

			public (string, string) configured;

			public _003C_003Ec__DisplayClass64_0 CS_0024_003C_003E8__locals1;

			internal void _003CSendDailyRequest_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass64_2
		{
			public UnityWebRequest request;

			public _003C_003Ec__DisplayClass64_1 CS_0024_003C_003E8__locals2;

			internal void _003CSendDailyRequest_003Eb__3(AsyncOperation _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass66_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CAddCurrentStreak_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass66_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass66_1 _003C_003E8__1;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public Action<string> result;

			public Action _003C_003E9__1;

			internal Task _003CAddCurrentStreak_003Eb__0()
			{
				return null;
			}

			internal void _003CAddCurrentStreak_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass66_1
		{
			public string url;

			public Dictionary<string, string> headers;

			public _003C_003Ec__DisplayClass66_0 CS_0024_003C_003E8__locals1;

			internal void _003CAddCurrentStreak_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass66_2
		{
			public UnityWebRequest request;

			public _003C_003Ec__DisplayClass66_1 CS_0024_003C_003E8__locals2;

			internal void _003CAddCurrentStreak_003Eb__3(AsyncOperation _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass67_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CSetSeasonsBattlePass_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass67_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass67_1 _003C_003E8__1;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public Action<bool> result;

			public string seasonId;

			public Action _003C_003E9__1;

			internal Task _003CSetSeasonsBattlePass_003Eb__0()
			{
				return null;
			}

			internal void _003CSetSeasonsBattlePass_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass67_1
		{
			public string url;

			public Dictionary<string, string> headers;

			public _003C_003Ec__DisplayClass67_0 CS_0024_003C_003E8__locals1;

			internal void _003CSetSeasonsBattlePass_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass67_2
		{
			public UnityWebRequest request;

			public _003C_003Ec__DisplayClass67_1 CS_0024_003C_003E8__locals2;

			internal void _003CSetSeasonsBattlePass_003Eb__3(AsyncOperation _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass68_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CDebugSeasons_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass68_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass68_1 _003C_003E8__1;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public Action<bool> result;

			public string seasonId;

			public string commandString;

			public Action _003C_003E9__1;

			internal Task _003CDebugSeasons_003Eb__0()
			{
				return null;
			}

			internal void _003CDebugSeasons_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass68_1
		{
			public string url;

			public Dictionary<string, string> headers;

			public _003C_003Ec__DisplayClass68_0 CS_0024_003C_003E8__locals1;

			internal void _003CDebugSeasons_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass68_2
		{
			public UnityWebRequest request;

			public _003C_003Ec__DisplayClass68_1 CS_0024_003C_003E8__locals2;

			internal void _003CDebugSeasons_003Eb__3(AsyncOperation _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass72_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CGetTranslation_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass72_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass72_2 _003C_003E8__1;

				private CancellationTokenSource _003Cbudget_003E5__2;

				private Task<ImmutableCredentials> _003Crefresh_003E5__3;

				private HttpRequestMessage _003Crequest_003E5__4;

				private TaskAwaiter<Task> _003C_003Eu__1;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__2;

				private TaskAwaiter<HttpResponseMessage> _003C_003Eu__3;

				private TaskAwaiter<string> _003C_003Eu__4;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public CancellationToken cancellationToken;

			public int timeoutSeconds;

			public Action<string, HttpStatusCode> callback;

			public string url;

			public string jsonPayload;

			public Action _003C_003E9__2;

			public Action _003C_003E9__1;

			public Action _003C_003E9__5;

			internal Task _003CGetTranslation_003Eb__0()
			{
				return null;
			}

			internal void _003CGetTranslation_003Eb__2()
			{
			}

			internal void _003CGetTranslation_003Eb__1()
			{
			}

			internal void _003CGetTranslation_003Eb__5()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass72_1
		{
			public string text;

			public _003C_003Ec__DisplayClass72_0 CS_0024_003C_003E8__locals1;

			internal void _003CGetTranslation_003Eb__4()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass72_2
		{
			public HttpResponseMessage response;

			public _003C_003Ec__DisplayClass72_1 CS_0024_003C_003E8__locals2;

			internal void _003CGetTranslation_003Eb__3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass74_0
		{
			public Action<(string, bool)> result;

			public string url;

			public Dictionary<string, string> headers;

			internal void _003CGetCatalog_003Eb__0()
			{
			}

			internal void _003CGetCatalog_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass74_1
		{
			public UnityWebRequest request;

			public _003C_003Ec__DisplayClass74_0 CS_0024_003C_003E8__locals1;

			internal void _003CGetCatalog_003Eb__2(AsyncOperation _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass80_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CPostJsonToUrl_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass80_0 _003C_003E4__this;

				private HttpClient _003Cclient_003E5__2;

				private HttpResponseMessage _003Cresponse_003E5__3;

				private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

				private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

				private TaskAwaiter<string> _003C_003Eu__3;

				private void MoveNext()
				{
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			public string callerMemberName;

			public Action<JObject, bool> result;

			public string jsonData;

			public string apiKey;

			public string url;

			internal Task _003CPostJsonToUrl_003Eb__0()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass84_0
		{
			public Action<JObject, bool> result;

			public JObject jObject;

			public bool success;

			internal void _003CInvokeOnMainThread_003Eb__0()
			{
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetAppleRefreshToken_003Ed__48 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

			public string refreshToken;

			private TaskAwaiter<string> _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetAppleToken_003Ed__46 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

			public string authCode;

			private TaskAwaiter<string> _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetCatalog_003Ed__74 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public Action<(string, bool)> result;

			private _003C_003Ec__DisplayClass74_0 _003C_003E8__1;

			public string userId;

			public string catalogVersion;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetCheckoutUrl_003Ed__73 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<(string, bool)> _003C_003Et__builder;

			public string offerAlias;

			private string _003CshopUrl_003E5__2;

			private HttpClient _003Cclient_003E5__3;

			private HttpResponseMessage _003Cresponse_003E5__4;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private TaskAwaiter<string> _003C_003Eu__3;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetConfigRequest_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<(string, bool)> _003C_003Et__builder;

			public string version;

			private HttpClient _003Cclient_003E5__2;

			private HttpResponseMessage _003Cresponse_003E5__3;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

			private TaskAwaiter<string> _003C_003Eu__2;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetDeviceIdToken_003Ed__47 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

			public string deviceId;

			private TaskAwaiter<string> _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetFileRequest_003Ed__27 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponseMessage> _003C_003Et__builder;

			public string fileName;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetFileRequest_003Ed__28 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponseMessage> _003C_003Et__builder;

			public string fileName;

			public string feature;

			private HttpClient _003Cclient_003E5__2;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private TaskAwaiter<string> _003C_003Eu__3;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetGoogleRefreshToken_003Ed__50 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

			public string refreshToken;

			private TaskAwaiter<string> _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetGoogleToken_003Ed__49 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

			public string authCode;

			public string uri;

			private TaskAwaiter<string> _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetItemsFromStorage_003Ed__69 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<(string, bool)> _003C_003Et__builder;

			public string gameVersion;

			private HttpClient _003Cclient_003E5__2;

			private HttpResponseMessage _003Cresponse_003E5__3;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private TaskAwaiter<string> _003C_003Eu__3;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetRequest_003Ed__40 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<(string, bool)> _003C_003Et__builder;

			public string userId;

			public string userName;

			public ImmutableCredentials credentials;

			private TaskAwaiter<(string, bool)> _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetRequest_003Ed__41 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<(string, bool)> _003C_003Et__builder;

			public string userId;

			public bool mergeCurrentGuest;

			public string guestUserGuid;

			public string guestUserId;

			public string userName;

			public bool? isMarketingChecked;

			public ImmutableCredentials credentials;

			private TaskAwaiter<(string, bool)> _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetUserReceiptsCall_003Ed__25 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<(string, bool)> _003C_003Et__builder;

			public string userId;

			private HttpClient _003Cclient_003E5__2;

			private HttpResponseMessage _003Cresponse_003E5__3;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private TaskAwaiter<string> _003C_003Eu__3;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetUserSlotsCall_003Ed__39 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<(string, bool)> _003C_003Et__builder;

			public ImmutableCredentials optionalCredentials;

			public Dictionary<string, string> headers;

			public string method;

			private HttpClient _003Cclient_003E5__2;

			private HttpResponseMessage _003Cresponse_003E5__3;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private TaskAwaiter<string> _003C_003Eu__3;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CPostUserSlots_003Ed__44 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponseMessage> _003C_003Et__builder;

			public PostSlotsRequest data;

			private HttpClient _003Cclient_003E5__2;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CPutAccountDelete_003Ed__43 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			public PutEmailRequest data;

			private HttpClient _003Cclient_003E5__2;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CPutFileRequest_003Ed__29 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponseMessage> _003C_003Et__builder;

			public HttpContent content;

			public string fileName;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CPutFileRequest_003Ed__30 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponseMessage> _003C_003Et__builder;

			public string fileName;

			public string feature;

			public HttpContent content;

			private HttpClient _003Cclient_003E5__2;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CPutReceiptsCall_003Ed__24 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<(bool, string)> _003C_003Et__builder;

			public PutAllReceiptsRequest request;

			public bool isPurchase;

			private string _003Cjson_003E5__2;

			private StringContent _003Ccontent_003E5__3;

			private HttpClient _003Cclient_003E5__4;

			private ImmutableCredentials _003Ccredentials_003E5__5;

			private HttpResponseMessage _003Cresponse_003E5__6;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private TaskAwaiter<string> _003C_003Eu__3;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CPutUserEmail_003Ed__42 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponseMessage> _003C_003Et__builder;

			public PutEmailRequest data;

			private HttpClient _003Cclient_003E5__2;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CRedeemItem_003Ed__71 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<(bool, string)> _003C_003Et__builder;

			public string itemId;

			public string transactionId;

			private HttpClient _003Cclient_003E5__2;

			private HttpResponseMessage _003Cresponse_003E5__3;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private TaskAwaiter<string> _003C_003Eu__3;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CRefreshTokenCall_003Ed__45 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

			public string service;

			public string serviceType;

			public string token;

			public string uri;

			private HttpClient _003Cclient_003E5__2;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

			private TaskAwaiter<string> _003C_003Eu__2;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CSetRedeemed_003Ed__75 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			public string transactionId;

			private HttpClient _003Cclient_003E5__2;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CUseItemFromStorage_003Ed__70 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			public string itemId;

			private HttpClient _003Cclient_003E5__2;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		public static RegionEndpoint Region;

		private static readonly string region;

		private static readonly string service;

		private static SettingsService _settings;

		private static ILoginMethod _login;

		private static bool _telemetryActive;

		private const string DevMode = "production";

		private static readonly HttpClient SharedClient;

		private static string VersionNumber__BackingField;

		private static string IDFV__BackingField;

		private static AwsWrappedSettings _wrappedSettings;

		public static string VersionNumber
		{
			get
			{
				return VersionNumber__BackingField;
			}
			set
			{
				VersionNumber__BackingField = value;
			}
		}

		public static string IDFV
		{
			get
			{
				return IDFV__BackingField;
			}
			set
			{
				IDFV__BackingField = value;
			}
		}

		public static Task<(string, bool)> GetConfigRequest(string version)
		{
			return null;
		}

		public static void SetAwsKeys(string awsc, ConfigDataModel dataModel)
		{
		}

		public static void SetAwsLogin(ILoginMethod login)
		{
		}

		public static ConfigDataModel GetConfig()
		{
			return null;
		}

		private static string PlatformHeader()
		{
			return null;
		}

		private static HttpClient CreateDefault()
		{
			return null;
		}

		private static HttpRequestMessage CreateRequest(HttpMethod method, string url, HttpContent content = null)
		{
			return null;
		}

		public static UnityWebRequest CreateDefaultGet(string url, Dictionary<string, string> headers)
		{
			return null;
		}

		private static UnityWebRequest CreateDefaultPut(string url, byte[] bodyData, Dictionary<string, string> headers)
		{
			return null;
		}

		private static UnityWebRequest CreateDefaultPost(string url, string bodyData, Dictionary<string, string> headers)
		{
			return null;
		}

		public static void SetTelemetryActive(bool active)
		{
		}

		public static void TrackErrorEvent(Exception ex, string awsId)
		{
		}

		public static void TrackErrorEvent(Exception ex, string extra, string awsId)
		{
		}

		public static void GetAllPurchases(string userId, Action<List<TransactionItemModel>> callback)
		{
		}

		public static void PutAllPurchases(string userId, List<Product> products, Action<bool> callback)
		{
		}

		public static void PutPurchase(string userId, ProductModel product, bool isPurchase, Action<bool> callback)
		{
		}

		public static Task<(bool, string)> PutReceiptsCall(PutAllReceiptsRequest request, bool isPurchase)
		{
			return null;
		}

		private static Task<(string, bool)> GetUserReceiptsCall(string userId, string method = "", string id = "")
		{
			return null;
		}

		public static string GetGoogleTransactionId(Product product)
		{
			return null;
		}

		public static Task<HttpResponseMessage> GetFileRequest(string fileName)
		{
			return null;
		}

		public static Task<HttpResponseMessage> GetFileRequest(string feature, string fileName)
		{
			return null;
		}

		public static Task<HttpResponseMessage> PutFileRequest(HttpContent content, string fileName)
		{
			return null;
		}

		public static Task<HttpResponseMessage> PutFileRequest(string feature, HttpContent content, string fileName)
		{
			return null;
		}

		private static Task<(string, bool)> GetUserSlotsCall(string method, Dictionary<string, string> headers, ImmutableCredentials optionalCredentials = null)
		{
			return null;
		}

		public static Task<(string, bool)> GetRequest(string userId, string userName, ImmutableCredentials credentials)
		{
			return null;
		}

		public static Task<(string, bool)> GetRequest(string userId, string userName, bool? isMarketingChecked, bool mergeCurrentGuest = false, string guestUserGuid = "", string guestUserId = "", ImmutableCredentials credentials = null)
		{
			return null;
		}

		public static Task<HttpResponseMessage> PutUserEmail(PutEmailRequest data)
		{
			return null;
		}

		public static Task<bool> PutAccountDelete(PutEmailRequest data)
		{
			return null;
		}

		public static Task<HttpResponseMessage> PostUserSlots(PostSlotsRequest data)
		{
			return null;
		}

		private static Task<string> RefreshTokenCall(string token, string service, string serviceType, string uri = "")
		{
			return null;
		}

		public static Task<string> GetAppleToken(string authCode)
		{
			return null;
		}

		public static Task<string> GetDeviceIdToken(string deviceId)
		{
			return null;
		}

		public static Task<string> GetAppleRefreshToken(string refreshToken)
		{
			return null;
		}

		public static Task<string> GetGoogleToken(string authCode, string uri)
		{
			return null;
		}

		public static Task<string> GetGoogleRefreshToken(string refreshToken)
		{
			return null;
		}

		public static void GetSeasonData(string seasonId, bool needsMyStuffLoading, Action<(string, bool)> result)
		{
		}

		public static void GetNarrativeContent(string url, Action<(string, bool)> result)
		{
		}

		public static void CompleteCommunityGoal(string seasonId, string objectiveId, Action<bool> result)
		{
		}

		public static void SaveSeasonPlayerData(string seasonId, string data, Action<bool> result)
		{
		}

		public static void PurchaseSeasonProduct(string seasonId, string productId, int quantity, Action<string> result)
		{
		}

		public static void RedeemTrackItem(string seasonId, string redeemId, bool isPremium, Action<string> result)
		{
		}

		public static void ReturnDailyReward(Action<DailyApiResult> result)
		{
		}

		public static void GetDailyRewardState(Action<DailyApiResult> result)
		{
		}

		public static void ClaimDailyRewards(string requestId, int stateVersion, Action<DailyApiResult> result)
		{
		}

		public static void RedeemDailyLife(string lifeId, Action<DailyApiResult> result)
		{
		}

		public static void PurchaseDailyMarketplaceOffer(string offerId, Action<DailyApiResult> result)
		{
		}

		public static void PurchaseDailyMarketplaceOffer(string offerId, string catalogId, int expectedPrice, Action<DailyApiResult> result)
		{
		}

		public static void PurchaseDailyTimeMachine(string requestId, string lifeId, int years, int expectedPrice, Action<DailyApiResult> result)
		{
		}

		private static void SendDailyRequest(string method, string path, string body, Action<DailyApiResult> result, int? debugMinutesOverride = null)
		{
		}

		private static (string, string) ConfigureDailyRequest(string method, string path, string body, string configuration)
		{
			return default;
		}

		public static void AddCurrentStreak(Action<string> result)
		{
		}

		public static void SetSeasonsBattlePass(string seasonId, Action<bool> result)
		{
		}

		public static void DebugSeasons(string seasonId, string commandString, Action<bool> result)
		{
		}

		public static Task<(string, bool)> GetItemsFromStorage(string gameVersion)
		{
			return null;
		}

		public static Task<bool> UseItemFromStorage(string itemId)
		{
			return null;
		}

		public static Task<(bool, string)> RedeemItem(string itemId, string transactionId)
		{
			return null;
		}

		public static void GetTranslation(string url, string jsonPayload, Action<string, HttpStatusCode> callback, CancellationToken cancellationToken = default(CancellationToken), int timeoutSeconds = 15)
		{
		}

		public static Task<(string, bool)> GetCheckoutUrl(string offerAlias)
		{
			return null;
		}

		public static Task GetCatalog(string userId, string catalogVersion, Action<(string, bool)> result)
		{
			return null;
		}

		public static Task<bool> SetRedeemed(string transactionId)
		{
			return null;
		}

		public static void SendUserStatsToBackend(string jsonData, Action<JObject, bool> result)
		{
		}

		public static void CheckWrappedVideoStatus(string jsonData, Action<JObject, bool> result)
		{
		}

		private static void PostWrappedJsonToUrl(string method, string jsonData, Action<JObject, bool> result, string callerMemberName = null)
		{
		}

		private static void PostJsonToUrl(string jsonData, string url, string apiKey, Action<JObject, bool> result, string callerMemberName)
		{
		}

		private static JObject TrackErrorJObject(string reason)
		{
			return null;
		}

		private static JObject FormatStatusReason(HttpResponseMessage response, string details)
		{
			return null;
		}

		private static JObject CreateReasonJObject(string reason)
		{
			return null;
		}

		private static void InvokeOnMainThread(Action<JObject, bool> result, JObject jObject, bool success)
		{
		}

		private static void Log(string message)
		{
		}
	}
}
