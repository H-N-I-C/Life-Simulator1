using System.Collections;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public class ScenarioTimeMachine : Scenario
{
	private sealed class _003C_003Ec__DisplayClass7_0
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003CLoadData_003Eb__0_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public _003C_003Ec__DisplayClass7_0 _003C_003E4__this;

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

		public ScenarioTimeMachine _003C_003E4__this;

		public int heroAge;

		public string path;

		public string folder;

		internal Task _003CLoadData_003Eb__0()
		{
			return null;
		}
	}

	public TIME_MACHINE_ORIGIN TimeMachineOrigin;

	private ConcurrentDictionary<int, bool> _timeMachineAvailable;

	private bool _cancelLoading;

	private readonly object _lock;

	public static int lastHeroAge;

	private static ScenarioTimeMachine _scenarioAgeDown;

	public ScenarioTimeMachine(TIME_MACHINE_ORIGIN origin, bool loadData = true)
	{
	}

	private void LoadData()
	{
	}

	public override void RefreshScenario()
	{
	}

	private bool CheckTimeMachineAge(int idx)
	{
		return false;
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public static bool IsTimeMachineAvailable()
	{
		return false;
	}

	public static void LoadTimeMachine(TIME_MACHINE_ORIGIN origin)
	{
	}

	public static ScenarioTimeMachine GetAgeDownTimeMachine()
	{
		return null;
	}
}
