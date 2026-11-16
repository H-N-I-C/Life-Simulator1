using System;
using System.Collections;
using System.Net.Mail;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public class DiagnosticsEmail
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static RemoteCertificateValidationCallback _003C_003E9__1_1;

		internal bool _003CSend_003Eb__1_1(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass1_0
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003CSend_003Eb__0_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public _003C_003Ec__DisplayClass1_0 _003C_003E4__this;

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

		public Exception e;

		public ArrayList DataFiles;

		public MailMessage mail;

		public string tempID;

		public string deviceSpecifics;

		internal Task _003CSend_003Eb__0()
		{
			return null;
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CSend_003Ed__1 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public Exception e;

		public string customError;

		private _003C_003Ec__DisplayClass1_0 _003C_003E8__1;

		public bool sendEntireFolder;

		private TaskAwaiter _003C_003Eu__1;

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

	public static string CWUS;

	public static Task Send(Exception e = null, string customError = "", bool sendEntireFolder = false)
	{
		return null;
	}

	public static void SendEmail(Exception e = null, bool awakeOverride = false, string customError = "", bool sendEntireFolder = false)
	{
	}
}
