using System;
using System.Collections.Generic;

public class FightClubTrainingActivity : ActivityLazyCsvBase
{
	public class Skill
	{
		public string SkillName;

		public FIGHT_CLUB_ATTRIBUTE SkillAttribute;

		public Func<float> SkillValue;

		public Action<float> TrainingAction;
	}

	public class Balancing
	{
		public static int ChanceOfInjuryMax;

		public static int ChanceOfQualityFeedback;

		public static bool OverrideInjuryChance;

		public static int ChanceOfInjuryOverride;
	}

	public enum ScenarioID
	{
		FC_TRAINING_BEGIN = 0,
		FC_TRAINING_MAX_FEEDBACK = 1,
		FC_TRAINING_FEEDBACK = 2,
		FC_TRAINING_COACH_QUALITY_FEEDBACK = 3,
		FC_TRAINING_MOVE_LEARNED = 4,
		FC_TRAINING_INJURY = 5,
		FC_TRAINING_INJURY_SURGERY = 6
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Skill, string> _003C_003E9__39_0;

		public static Action _003C_003E9__40_1;

		public static Action _003C_003E9__40_3;

		public static Action _003C_003E9__40_5;

		public static Func<SimFightClubInjury, string> _003C_003E9__42_0;

		internal string _003CGetTrainableSkillNames_003Eb__39_0(Skill x)
		{
			return null;
		}

		internal void _003CTrainSkillChoiceMade_003Eb__40_1()
		{
		}

		internal void _003CTrainSkillChoiceMade_003Eb__40_3()
		{
		}

		internal void _003CTrainSkillChoiceMade_003Eb__40_5()
		{
		}

		internal string _003CPushInjury_003Eb__42_0(SimFightClubInjury x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass40_0
	{
		public SimFightClubMove moveToUnlock;

		public FightClubTrainingActivity _003C_003E4__this;

		internal void _003CTrainSkillChoiceMade_003Eb__0()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass40_1
	{
		public SimFightClubMove moveToUnlock;

		public FightClubTrainingActivity _003C_003E4__this;

		internal void _003CTrainSkillChoiceMade_003Eb__2()
		{
		}
	}

	private const string TRAIN_STATS_TEMPLATE = "At this level, your <training-stats> could use some work.";

	private const string TRAIN_COMPLETED_TEMPLATE = "Your <trained-stats> <is-are> at maximum for your current level.";

	private readonly SimFightClub _fightClub;

	private readonly SimFightClubFighterStats _stats;

	private readonly SimHero _hero;

	private Skill CurrentlyTraining__BackingField;

	private SimFightClubInjury Injury__BackingField;

	private bool InjuryApplied__BackingField;

	private readonly Dictionary<FIGHT_CLUB_ATTRIBUTE, Skill> _skillDict;

	private readonly FIGHT_CLUB_ATTRIBUTE[] _skillOrder;

	public SimFightClubFighterStats Stats => null;

	public Skill CurrentlyTraining
	{
		get
		{
			return CurrentlyTraining__BackingField;
		}
		private set
		{
			CurrentlyTraining__BackingField = value;
		}
	}

	public SimFightClubInjury Injury
	{
		get
		{
			return Injury__BackingField;
		}
		private set
		{
			Injury__BackingField = value;
		}
	}

	public bool InjuryApplied
	{
		get
		{
			return InjuryApplied__BackingField;
		}
		private set
		{
			InjuryApplied__BackingField = value;
		}
	}

	public SimFightClubCoach Coach => null;

	public float Strength => 0f;

	public float Toughness => 0f;

	public float Agility => 0f;

	public float Technique => 0f;

	public void ApplyInjury()
	{
	}

	public FightClubTrainingActivity(Life life)
		: base(null)
	{
	}

	public string GetTrainableSkillsText()
	{
		return null;
	}

	public string GetMaxedSkillsText()
	{
		return null;
	}

	public string GetStatsText()
	{
		return null;
	}

	public string[] GetTrainableSkillNames()
	{
		return null;
	}

	private void TrainSkillChoiceMade(ChoiceSelection selection)
	{
	}

	private void PushMoveUnlock(SimFightClubMove move)
	{
	}

	private void PushInjury()
	{
	}

	private void InjuryChoiceSelection(ChoiceSelection choiceArguments)
	{
	}

	private List<Skill> GetTrainableSkills()
	{
		return null;
	}

	private List<Skill> GetMaxedSkills()
	{
		return null;
	}

	private string FormatTrainableSkills(List<Skill> skills)
	{
		return null;
	}

	private float _003C_002Ector_003Eb__35_0()
	{
		return 0f;
	}

	private void _003C_002Ector_003Eb__35_1(float amount)
	{
	}

	private float _003C_002Ector_003Eb__35_2()
	{
		return 0f;
	}

	private void _003C_002Ector_003Eb__35_3(float amount)
	{
	}

	private float _003C_002Ector_003Eb__35_4()
	{
		return 0f;
	}

	private void _003C_002Ector_003Eb__35_5(float amount)
	{
	}

	private float _003C_002Ector_003Eb__35_6()
	{
		return 0f;
	}

	private void _003C_002Ector_003Eb__35_7(float amount)
	{
	}

	private void _003CTrainSkillChoiceMade_003Eb__40_4()
	{
	}
}
