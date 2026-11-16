using System;
using System.Collections;

[Serializable]
public class SimDisease
{
	private int YearsSuffered__BackingField;

	public string DiseaseTypeName;

	private string BodyPart__BackingField;

	private bool IsUndiagnosed__BackingField;

	private int AgeAtContraction__BackingField;

	private ArrayList _SufferedSymptomArray__BackingField;

	public int YearsSuffered
	{
		get
		{
			return YearsSuffered__BackingField;
		}
		set
		{
			YearsSuffered__BackingField = value;
		}
	}

	public DataDiseaseType DiseaseType
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string BodyPart
	{
		get
		{
			return BodyPart__BackingField;
		}
		set
		{
			BodyPart__BackingField = value;
		}
	}

	public bool IsUndiagnosed
	{
		get
		{
			return IsUndiagnosed__BackingField;
		}
		set
		{
			IsUndiagnosed__BackingField = value;
		}
	}

	public int AgeAtContraction
	{
		get
		{
			return AgeAtContraction__BackingField;
		}
		set
		{
			AgeAtContraction__BackingField = value;
		}
	}

	private ArrayList _SufferedSymptomArray
	{
		get
		{
			return _SufferedSymptomArray__BackingField;
		}
		set
		{
			_SufferedSymptomArray__BackingField = value;
		}
	}

	public ArrayList SufferedSymptomArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private SimDisease()
	{
	}

	public SimDisease(bool initHere)
	{
	}

	public string Description()
	{
		return null;
	}

	public bool DoMoreUnsufferedSymptomsExist()
	{
		return false;
	}

	public bool HasAlreadySufferedFromSymptom(string symptom)
	{
		return false;
	}
}
