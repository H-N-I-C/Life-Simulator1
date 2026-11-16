using System.Collections;

public class DataAstronautFitnessTraining
{
	private string Scenario__BackingField;

	private string LogVersion__BackingField;

	private bool TrainingProgramOnly__BackingField;

	private ArrayList TrainingType__BackingField;

	private int ImpactScore__BackingField;

	private int CommonnessScore__BackingField;

	public string Scenario
	{
		get
		{
			return Scenario__BackingField;
		}
		set
		{
			Scenario__BackingField = value;
		}
	}

	public string LogVersion
	{
		get
		{
			return LogVersion__BackingField;
		}
		set
		{
			LogVersion__BackingField = value;
		}
	}

	public bool TrainingProgramOnly
	{
		get
		{
			return TrainingProgramOnly__BackingField;
		}
		set
		{
			TrainingProgramOnly__BackingField = value;
		}
	}

	public ArrayList TrainingType
	{
		get
		{
			return TrainingType__BackingField;
		}
		set
		{
			TrainingType__BackingField = value;
		}
	}

	public int ImpactScore
	{
		get
		{
			return ImpactScore__BackingField;
		}
		set
		{
			ImpactScore__BackingField = value;
		}
	}

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}
}
