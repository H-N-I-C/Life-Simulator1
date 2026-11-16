public class ScenarioSecretAgentHackingMissionFile : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgentHackingMission _mission;

	private readonly string _file;

	private readonly string _drive;

	private readonly bool _correctFile;

	private readonly string _fileColor;

	public ScenarioSecretAgentHackingMissionFile(SimSecretAgentHackingMission mission, string fileName, string fileColor, string drive)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
