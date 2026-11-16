using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioModelGoSeesInterviewQuestion : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataInterviewAnswer, bool> _003C_003E9__9_0;

		public static Func<DataInterviewAnswer, bool> _003C_003E9__9_1;

		internal bool _003CRefreshScenario_003Eb__9_0(DataInterviewAnswer answer)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__9_1(DataInterviewAnswer answer)
		{
			return false;
		}
	}

	private string _narrative;

	private List<SimModelingAgencyClient> _clients;

	private List<float> _clientRelationshipDiffs;

	private int _clientIndex;

	private SimModelingAgencyClient _theClient;

	private SimSpecialJobModel _modelJob;

	private DataModelGoSeeInterviewQuestion _theQuestion;

	private ArrayList _answerBank;

	public ScenarioModelGoSeesInterviewQuestion(List<SimModelingAgencyClient> clients, List<float> clientRelationshipDiffs, int clientIndex)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
