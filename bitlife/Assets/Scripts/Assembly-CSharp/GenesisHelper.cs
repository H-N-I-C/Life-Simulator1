using System;
using System.Collections.Generic;
using Genesis.Controllers;

public static class GenesisHelper
{
	private class ImpactDicitonaries
	{
		public Dictionary<string, Action<int>> DirectImpact;

		public Dictionary<string, Action<string, int>> PassthroughImpact;
	}

	private static readonly Dictionary<string, Func<string, BracketParser, bool, SimPerson>> GenesisNpcsFunctions;

	private static Dictionary<string, ImpactDicitonaries> _attributeActionsPerTarget;

	public static readonly string ParameterRegex;

	public static void ClearAttributeDictionary()
	{
	}

	public static void DoImpacts(string fullImpacts, ScenarioController owningController)
	{
	}

	private static string CleanImpactAttributeName(string attributeName)
	{
		return null;
	}

	private static void BuildTargetAttributeActions(string name, SimPerson target)
	{
	}

	private static void BuildTargetAttributeActionsHero(string name)
	{
	}

	private static void BuildTargetAttributeActionsNpc(string name, SimPerson target)
	{
	}

	private static void BuildTargetAttributeActions(string name, SimZooAnimal target)
	{
	}

	private static void BuildTargetAttributeActions(string name, SimPet target)
	{
	}

	private static void BuildTargetAttributeActions(string name, SimCar target)
	{
	}

	private static void BuildTargetAttributeActions(string name, SimHouse target)
	{
	}

	private static void BuildTargetAttributeActions(string name, SimGhost target)
	{
	}

	public static void EmptyImpact(int value)
	{
	}

	public static bool CheckForParameters(ref string text, out object[] parameters)
	{
		parameters = null;
		return false;
	}

	public static string[] TrimAndSplitCSVArray(string value, char splitter)
	{
		return null;
	}

	public static string ParseGenesisInfo(string text, ScenarioController owningController)
	{
		return null;
	}

	public static void SetUpExtraBracketParsing(ScenarioController controller)
	{
	}

	public static string ParseGenesisSpecialLivesTags(string text, ScenarioController owningController)
	{
		return null;
	}

	public static SimPerson GetNpcByType(string type, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedFriend(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedParent(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedChild(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedSibling(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedEx(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedSwinger(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedSwingerLover(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedCoworker(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedClassmate(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedTenant(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedVampireHunter(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}

	private static SimPerson FindAndSetSelectedSenior(string name, BracketParser parser, bool shouldSet)
	{
		return null;
	}
}
