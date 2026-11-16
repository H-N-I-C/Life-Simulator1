using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Backend.Code.Controllers
{
	public static class FileController
	{
		private sealed class _003C_003Ec__DisplayClass0_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CUploadFile_003Eb__1_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass0_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass0_1 _003C_003E8__1;

				private string _003CidName_003E5__2;

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

			public Action<bool> result;

			public string content;

			public FILE_FEATURE feature;

			public string id;

			public Action _003C_003E9__3;

			internal void _003CUploadFile_003Eb__0()
			{
			}

			internal Task _003CUploadFile_003Eb__1()
			{
				return null;
			}

			internal void _003CUploadFile_003Eb__3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass0_1
		{
			public HttpResponseMessage request;

			public _003C_003Ec__DisplayClass0_0 CS_0024_003C_003E8__locals1;

			internal void _003CUploadFile_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass1_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CDownloadFile_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass1_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass1_1 _003C_003E8__1;

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

			public FILE_FEATURE feature;

			public string id;

			public Action<string> result;

			public Action _003C_003E9__2;

			public Action _003C_003E9__1;

			public Action _003C_003E9__4;

			internal Task _003CDownloadFile_003Eb__0()
			{
				return null;
			}

			internal void _003CDownloadFile_003Eb__2()
			{
			}

			internal void _003CDownloadFile_003Eb__1()
			{
			}

			internal void _003CDownloadFile_003Eb__4()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass1_1
		{
			public string textContent;

			public _003C_003Ec__DisplayClass1_0 CS_0024_003C_003E8__locals1;

			internal void _003CDownloadFile_003Eb__3()
			{
			}
		}

		public static void UploadFile(FILE_FEATURE feature, string id, string content, Action<bool> result)
		{
		}

		public static void DownloadFile(FILE_FEATURE feature, string id, Action<string> result)
		{
		}

		private static string GetFileName(FILE_FEATURE feature, string id)
		{
			return null;
		}
	}
}
