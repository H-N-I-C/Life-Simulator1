using System.Collections.Generic;

public class ChangeSchoolActivity
{
	public enum Outcome
	{
		ParentsAgree = 0,
		ParentsRefuse = 1,
		ParentsDontHaveMoney = 2,
		ParentsRefuseSmartParents = 3,
		ParentsRefuseRoyal = 4
	}

	private const string TargetSchoolTag = "<new-school>";

	private const string LocationTag = "<location>";

	public static bool OverrideOutcome;

	public static Outcome OutcomeOverrideValue;

	private const int MinParentRelationship = 50;

	private const int MinParentSmart = 80;

	private const int MinParentMoney = 70;

	private const int HappinessImpact = 2;

	private const int ParentsHappinessImpact = 1;

	private const int RelationshipImpact = 1;

	private const int MinGradeToGetScholarship = 80;

	private readonly BracketParser _parser;

	private readonly Dictionary<string, string> _bracketDictionary;

	private SimOccupationStudent Student__BackingField;

	private SimSchool CurrentSchool__BackingField;

	private SimSchool _targetSchool;

	private SimOccupationStudent _targetOccupation;

	private Outcome ScenarioOutcome__BackingField;

	private SimHero _hero;

	private bool _hasGoodRelationshipWithAllParents;

	private float _parentsSmartScore;

	private float _parentsAgregatedMoney;

	public SimOccupationStudent Student
	{
		get
		{
			return Student__BackingField;
		}
		private set
		{
			Student__BackingField = value;
		}
	}

	public SimSchool CurrentSchool
	{
		get
		{
			return CurrentSchool__BackingField;
		}
		private set
		{
			CurrentSchool__BackingField = value;
		}
	}

	public SimSchool TargetSchool
	{
		get
		{
			return null;
		}
		private set
		{
		}
	}

	public string Location => null;

	public bool CurrentlyInPrivateSchool => false;

	public Outcome ScenarioOutcome
	{
		get
		{
			return ScenarioOutcome__BackingField;
		}
		private set
		{
			ScenarioOutcome__BackingField = value;
		}
	}

	public bool IsOrphan()
	{
		return false;
	}

	public bool HasChangedSchoolThisYear()
	{
		return false;
	}

	public void CreateTargetSchool()
	{
	}

	private Outcome CalculateOutcome()
	{
		return Outcome.ParentsAgree;
	}

	private void CalculateParentScore()
	{
	}

	private SimOccupationStudent CreateTargetOccupation()
	{
		return null;
	}

	public void CalculateAndApplyImpact()
	{
	}

	private void ImpactParentsHappiness(int impact)
	{
	}

	private void ImpactHeroHappiness(int impact)
	{
	}

	private void ImpactParentsRelationship(int impact)
	{
	}

	public void MovetoSchoolIfApproved()
	{
	}

	public void MoveToSchool()
	{
	}

	public bool GetScholarship()
	{
		return false;
	}

	public void ApplyScholarshipImpact()
	{
	}

	public void ApplyRefusedScholarshipImpact()
	{
	}

	public string ParseBody(string body)
	{
		return null;
	}

	public void LogNarrative(string narrativeTemplate)
	{
	}

	private void ChangeSchoolThisYear()
	{
	}
}
