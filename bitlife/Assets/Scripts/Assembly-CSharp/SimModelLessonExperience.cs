using System;

[Serializable]
public class SimModelLessonExperience
{
	private int NumLessons__BackingField;

	private int AgeAtLastLesson__BackingField;

	private int NumLessonsAtCurrentAge__BackingField;

	private float Att_skill__BackingField;

	public int NumLessons
	{
		get
		{
			return NumLessons__BackingField;
		}
		set
		{
			NumLessons__BackingField = value;
		}
	}

	public int AgeAtLastLesson
	{
		get
		{
			return AgeAtLastLesson__BackingField;
		}
		set
		{
			AgeAtLastLesson__BackingField = value;
		}
	}

	public int NumLessonsAtCurrentAge
	{
		get
		{
			return NumLessonsAtCurrentAge__BackingField;
		}
		set
		{
			NumLessonsAtCurrentAge__BackingField = value;
		}
	}

	public float Att_skill
	{
		get
		{
			return Att_skill__BackingField;
		}
		set
		{
			Att_skill__BackingField = value;
		}
	}

	public void ImpactSkill(int impactScore)
	{
	}
}
