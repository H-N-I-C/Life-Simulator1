using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class MilitaryMethodRepair
{
    public static void Fix(MethodDefinition method)
    {
        if (method.Name != "ChoiceSelectedAtIndex" ||
            method.DeclaringType.FullName != "ScenarioMilitaryTermination" ||
            method.ReturnType.FullName != "System.Void" ||
            method.Parameters.Count != 1 ||
            method.Parameters[0].ParameterType.FullName != "System.Int32")
            throw new InvalidOperationException($"Unexpected military termination signature: {method.FullName}");

        var module = method.Module;
        var owner = method.DeclaringType;
        var scenarioType = FindType(module, "Scenario");
        var choiceType = FindType(module, "ScenarioChoice");
        var militaryType = FindType(module, "SimOccupationMilitary");
        var workplaceType = FindType(module, "SimWorkplace");
        var supervisorType = FindType(module, "SimCoworker");
        var gameManagerType = FindType(module, "GameManager");
        var lifeType = FindType(module, "Life");
        var personType = FindType(module, "SimPerson");
        var heroType = FindType(module, "SimHero");
        var loverArgumentType = FindType(module, "ScenarioLoverArgues");
        var trackerType = FindType(module, "Apollo16ChallengeTracker");
        var personNameType = FindType(module, "SimPersonName");
        var lifeLogType = FindType(module, "LifeLog");
        var lifeInterfaceType = FindType(module, "LifeInterface");

        var militaryField = Field(module, owner, "_military");
        var reasonField = Field(module, owner, "_reason");
        var choiceArrayField = Field(module, scenarioType, "ChoiceArray");
        var cancelScenarioField = Field(module, scenarioType, "CancelScenario");
        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var queueField = Field(module, lifeType, "IncrementAgeScenarioQueueArray");
        var lifeInterfaceField = Field(module, lifeType, "LifeInterface");

        var getTitle = ImportMethod(module, choiceType, "get_Title", 0, true);
        var toLower = FrameworkMethod(module, "System.String", "ToLower", 0, true);
        var format2 = FrameworkMethod(module, "System.String", "Format", 3, false,
            "System.String", "System.Object", "System.Object");
        var format3 = FrameworkMethod(module, "System.String", "Format", 4, false,
            "System.String", "System.Object", "System.Object", "System.Object");
        var contains = FrameworkMethod(module, "System.String", "Contains", 1, true, "System.String");
        var getType = FrameworkMethod(module, "System.Object", "GetType", 0, true);
        var getFullName = FrameworkMethod(module, "System.Type", "get_FullName", 0, true);
        var arrayListGetItem = FrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true, "System.Int32");

        var getMilitaryBranch = ImportMethod(module, militaryType, "get_MilitaryBranch", 0, true);
        var getBranchName = ImportMethod(module, FindType(module, "DataHub"), "GetStringForMilitaryBranch", 1, false, "System.Int32");
        var getWorkplace = ImportMethod(module, militaryType, "get_Workplace", 0, true);
        var getSupervisor = ImportMethod(module, workplaceType, "GetSupervisor", 0, true);
        var getNextScenario = ImportMethod(module, scenarioType, "get_NextScenario", 0, true);
        var setNextScenario = ImportMethod(module, scenarioType, "set_NextScenario", 1, true, "Scenario");
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 3, true,
            "System.String", "System.String", "Scenario");

        var queueType = (GenericInstanceType)queueField.FieldType;
        var queueGetCount = ImportMethod(module, queueType, "get_Count", 0, true);
        var queueGetItem = ImportMethod(module, queueType, "get_Item", 1, true);
        var getLover = ImportMethod(module, lifeType, "get_Lover", 0, true);
        var getHero = ImportMethod(module, lifeType, "get_Hero", 0, true);
        var getRandom = ImportMethod(module, FindType(module, "GameReference"), "GetRandomNumber", 2, false,
            "System.Int32", "System.Int32");
        var getCraziness = ImportMethod(module, personType, "get_Att_craziness", 0, true);
        var impactRelationship = ImportMethod(module, personType, "ImpactRelationshipStrength", 1, true, "System.Int32");
        var loverArgumentConstructor = ImportMethod(module, loverArgumentType, ".ctor", 1, true, "System.Int32");

        var impactDiscipline = ImportMethod(module, personType, "ImpactDiscipline", 1, true, "System.Int32");
        var impactKarma = ImportMethod(module, personType, "ImpactKarma", 1, true, "System.Int32");
        var impactHappiness = ImportMethod(module, heroType, "ImpactHappiness", 1, true, "System.Int32");
        var getNumInsults = ImportMethod(module, lifeType, "get_NumTimesInsultedSomeone", 0, true);
        var setNumInsults = ImportMethod(module, lifeType, "set_NumTimesInsultedSomeone", 1, true, "System.Int32");
        var getRandomInsult = ImportMethod(module, FindType(module, "DataHub"), "GetRandomInsultForGender", 2, false,
            "System.Int32", "System.Boolean");
        var relationshipStatus = ImportMethod(module, supervisorType, "RelationshipStatusStringAndCapitalize", 1, true, "System.Boolean");
        var getChallengeTracker = ImportMethod(module, lifeType, "get_ChallengeTracker", 0, true);
        var getPeopleInsulted = ImportMethod(module, trackerType, "get_ListPeopleNamesInsulted", 0, true);
        var peopleInsultedType = (GenericInstanceType)getPeopleInsulted.ReturnType;
        var addInsultedName = ImportMethod(module, peopleInsultedType, "Add", 1, true);
        var getName = ImportMethod(module, personType, "get_Name", 0, true);
        var getFullPersonName = ImportMethod(module, personNameType, "FullName", 0, true);
        var leaveMilitary = ImportMethod(module, heroType, "LeaveMilitaryWithDischargeType", 1, true, "System.Int32");
        var getLifeLog = ImportMethod(module, lifeType, "get_LifeLog", 0, true);
        var log = ImportMethod(module, lifeLogType, "Log", 1, true, "System.String");
        var refreshUI = ImportMethod(module, lifeInterfaceType, "RefreshUI", 0, true);

        method.Body = new MethodBody(method) { InitLocals = true };
        var body = method.Body;
        var choiceText = new VariableDefinition(module.TypeSystem.String);
        var narrative = new VariableDefinition(module.TypeSystem.String);
        var supervisor = new VariableDefinition(supervisorType);
        var queue = new VariableDefinition(queueType);
        var loopIndex = new VariableDefinition(module.TypeSystem.Int32);
        var queuedScenario = new VariableDefinition(scenarioType);
        var roll = new VariableDefinition(module.TypeSystem.Int32);
        var loverArgument = new VariableDefinition(loverArgumentType);
        var insult = new VariableDefinition(module.TypeSystem.String);
        var relationship = new VariableDefinition(module.TypeSystem.String);
        var feedbackTitle = new VariableDefinition(module.TypeSystem.String);
        foreach (var local in new[]
                 {
                     choiceText, narrative, supervisor, queue, loopIndex, queuedScenario,
                     roll, loverArgument, insult, relationship, feedbackTitle
                 })
            body.Variables.Add(local);

        var il = body.Instructions;
        void LoadLife()
        {
            il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
            il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        }
        void LoadHero()
        {
            LoadLife();
            il.Add(Instruction.Create(OpCodes.Call, getHero));
        }
        void LoadLover()
        {
            LoadLife();
            il.Add(Instruction.Create(OpCodes.Call, getLover));
        }

        // Native 0x4fe5f68..0x4fe5fd8: selected choice title, converted to lowercase.
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, choiceArrayField));
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, choiceType));
        il.Add(Instruction.Create(OpCodes.Call, getTitle));
        il.Add(Instruction.Create(OpCodes.Callvirt, toLower));
        il.Add(Instruction.Create(OpCodes.Stloc, choiceText));

        // Native 0x4fe5fd8..0x4fe6040: construct the dishonorable-discharge narrative.
        il.Add(Instruction.Create(OpCodes.Ldstr, "I was dishonorably discharged from the {0}{1}."));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, militaryField));
        il.Add(Instruction.Create(OpCodes.Call, getMilitaryBranch));
        il.Add(Instruction.Create(OpCodes.Call, getBranchName));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, reasonField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        var noDrugTest = Instruction.Create(OpCodes.Nop);
        var reasonDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Bne_Un, noDrugTest));
        il.Add(Instruction.Create(OpCodes.Ldstr, " after failing a drug test"));
        il.Add(Instruction.Create(OpCodes.Br, reasonDone));
        il.Add(noDrugTest);
        il.Add(Instruction.Create(OpCodes.Ldstr, ""));
        il.Add(reasonDone);
        il.Add(Instruction.Create(OpCodes.Call, format2));
        il.Add(Instruction.Create(OpCodes.Stloc, narrative));

        // Native 0x4fe6044..0x4fe6068: resolve the military supervisor.
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, militaryField));
        il.Add(Instruction.Create(OpCodes.Call, getWorkplace));
        il.Add(Instruction.Create(OpCodes.Call, getSupervisor));
        il.Add(Instruction.Create(OpCodes.Stloc, supervisor));

        // Native 0x4fe6068..0x4fe6158: cancel every queued military scenario.
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Ldfld, queueField));
        il.Add(Instruction.Create(OpCodes.Stloc, queue));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        var queueLoop = Instruction.Create(OpCodes.Nop);
        var queueNext = Instruction.Create(OpCodes.Nop);
        var queueCheck = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, queueCheck));
        il.Add(queueLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, queue));
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Callvirt, queueGetItem));
        il.Add(Instruction.Create(OpCodes.Stloc, queuedScenario));
        il.Add(Instruction.Create(OpCodes.Ldloc, queuedScenario));
        il.Add(Instruction.Create(OpCodes.Call, getType));
        il.Add(Instruction.Create(OpCodes.Callvirt, getFullName));
        il.Add(Instruction.Create(OpCodes.Ldstr, "ScenarioMilitary"));
        il.Add(Instruction.Create(OpCodes.Callvirt, contains));
        il.Add(Instruction.Create(OpCodes.Brfalse, queueNext));
        il.Add(Instruction.Create(OpCodes.Ldloc, queuedScenario));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stfld, cancelScenarioField));
        il.Add(queueNext);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, loopIndex));
        il.Add(queueCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, loopIndex));
        il.Add(Instruction.Create(OpCodes.Ldloc, queue));
        il.Add(Instruction.Create(OpCodes.Callvirt, queueGetCount));
        il.Add(Instruction.Create(OpCodes.Blt, queueLoop));

        // Native 0x4fe6158..0x4fe6218: lover argument, or a relationship penalty.
        var noLoverArgument = Instruction.Create(OpCodes.Nop);
        var loverDone = Instruction.Create(OpCodes.Nop);
        LoadLover();
        il.Add(Instruction.Create(OpCodes.Brfalse, loverDone));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 100));
        il.Add(Instruction.Create(OpCodes.Call, getRandom));
        il.Add(Instruction.Create(OpCodes.Stloc, roll));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 100f));
        LoadLover();
        il.Add(Instruction.Create(OpCodes.Call, getCraziness));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Ldloc, roll));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Ble, noLoverArgument));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 28));
        il.Add(Instruction.Create(OpCodes.Newobj, loverArgumentConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, loverArgument));
        il.Add(Instruction.Create(OpCodes.Ldloc, loverArgument));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, getNextScenario));
        il.Add(Instruction.Create(OpCodes.Call, setNextScenario));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, loverArgument));
        il.Add(Instruction.Create(OpCodes.Call, setNextScenario));
        il.Add(Instruction.Create(OpCodes.Br, loverDone));
        il.Add(noLoverArgument);
        LoadLover();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_M1));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Callvirt, impactRelationship));
        il.Add(loverDone);

        // Native 0x4fe6218..0x4fe644c: insult outcome and feedback scenario.
        var checkWish = Instruction.Create(OpCodes.Nop);
        var outcomesDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, choiceText));
        il.Add(Instruction.Create(OpCodes.Ldstr, "insult"));
        il.Add(Instruction.Create(OpCodes.Callvirt, contains));
        il.Add(Instruction.Create(OpCodes.Brfalse, checkWish));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_M1));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Call, impactDiscipline));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_M1));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Call, impactKarma));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, impactHappiness));
        LoadLife();
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Call, getNumInsults));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Call, setNumInsults));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Call, getRandomInsult));
        il.Add(Instruction.Create(OpCodes.Stloc, insult));
        il.Add(Instruction.Create(OpCodes.Ldloc, supervisor));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Call, relationshipStatus));
        il.Add(Instruction.Create(OpCodes.Stloc, relationship));
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0} I called my {1} {2} on my way out."));
        il.Add(Instruction.Create(OpCodes.Ldloc, narrative));
        il.Add(Instruction.Create(OpCodes.Ldloc, relationship));
        il.Add(Instruction.Create(OpCodes.Ldloc, insult));
        il.Add(Instruction.Create(OpCodes.Call, format3));
        il.Add(Instruction.Create(OpCodes.Stloc, narrative));

        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Call, getRandom));
        var laterMeatHead = Instruction.Create(OpCodes.Nop);
        var feedbackTitleDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Brtrue, laterMeatHead));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Stay classy"));
        il.Add(Instruction.Create(OpCodes.Br, feedbackTitleDone));
        il.Add(laterMeatHead);
        il.Add(Instruction.Create(OpCodes.Ldstr, "Later, meat head"));
        il.Add(feedbackTitleDone);
        il.Add(Instruction.Create(OpCodes.Stloc, feedbackTitle));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, feedbackTitle));
        il.Add(Instruction.Create(OpCodes.Ldstr, "You called your {0} {1} on your way out!"));
        il.Add(Instruction.Create(OpCodes.Ldloc, relationship));
        il.Add(Instruction.Create(OpCodes.Ldloc, insult));
        il.Add(Instruction.Create(OpCodes.Call, format2));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, getNextScenario));
        il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
        il.Add(Instruction.Create(OpCodes.Call, setNextScenario));

        LoadLife();
        il.Add(Instruction.Create(OpCodes.Call, getChallengeTracker));
        il.Add(Instruction.Create(OpCodes.Call, getPeopleInsulted));
        il.Add(Instruction.Create(OpCodes.Ldloc, supervisor));
        il.Add(Instruction.Create(OpCodes.Call, getName));
        il.Add(Instruction.Create(OpCodes.Call, getFullPersonName));
        il.Add(Instruction.Create(OpCodes.Callvirt, addInsultedName));
        il.Add(Instruction.Create(OpCodes.Br, outcomesDone));

        // Native 0x4fe6450..0x4fe64e8: appreciative outcome, with no feedback scenario.
        il.Add(checkWish);
        il.Add(Instruction.Create(OpCodes.Ldloc, choiceText));
        il.Add(Instruction.Create(OpCodes.Ldstr, "wish"));
        il.Add(Instruction.Create(OpCodes.Callvirt, contains));
        il.Add(Instruction.Create(OpCodes.Brfalse, outcomesDone));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Call, impactDiscipline));
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Call, impactKarma));
        il.Add(Instruction.Create(OpCodes.Ldstr, "{0} I thanked my {1} for the opportunity to serve."));
        il.Add(Instruction.Create(OpCodes.Ldloc, narrative));
        il.Add(Instruction.Create(OpCodes.Ldloc, supervisor));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Call, relationshipStatus));
        il.Add(Instruction.Create(OpCodes.Call, format2));
        il.Add(Instruction.Create(OpCodes.Stloc, narrative));

        // Native 0x4fe6500..0x4fe6578: discharge, log, and refresh on every path.
        il.Add(outcomesDone);
        LoadHero();
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Call, leaveMilitary));
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Call, getLifeLog));
        il.Add(Instruction.Create(OpCodes.Ldloc, narrative));
        il.Add(Instruction.Create(OpCodes.Call, log));
        LoadLife();
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeInterfaceField));
        il.Add(Instruction.Create(OpCodes.Call, refreshUI));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static MethodReference ImportMethod(
        ModuleDefinition module,
        TypeReference declaringType,
        string name,
        int parameterCount,
        bool? instance = null,
        params string[] parameterTypes)
    {
        var candidates = name is ".ctor" or ".cctor"
            ? declaringType.Resolve().Methods
            : Hierarchy(declaringType.Resolve()).SelectMany(type => type.Methods);
        var definition = candidates.First(candidate =>
            candidate.Name == name && candidate.Parameters.Count == parameterCount &&
            (!instance.HasValue || candidate.IsStatic != instance.Value) &&
            (parameterTypes.Length == 0 ||
             candidate.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameterTypes)));
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
    }

    private static MethodReference FrameworkMethod(
        ModuleDefinition module,
        string typeName,
        string methodName,
        int parameterCount,
        bool instance,
        params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = new TypeReference(
            typeName.Substring(0, separator),
            typeName.Substring(separator + 1),
            module,
            module.TypeSystem.Object.Scope);
        return ImportMethod(module, type, methodName, parameterCount, instance, parameterTypes);
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        var definition = Hierarchy(declaringType.Resolve()).SelectMany(type => type.Fields)
            .Single(candidate => candidate.Name == name);
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
    }

    private static TypeDefinition FindType(ModuleDefinition module, string fullName)
    {
        return module.GetType(fullName) ?? module.Types.SelectMany(Flatten).Single(type => type.FullName == fullName);
    }

    private static IEnumerable<TypeDefinition> Hierarchy(TypeDefinition type)
    {
        for (var current = type; current is not null; current = current.BaseType?.Resolve()) yield return current;
    }

    private static IEnumerable<TypeDefinition> Flatten(TypeDefinition type)
    {
        yield return type;
        foreach (var nested in type.NestedTypes)
        foreach (var descendant in Flatten(nested))
            yield return descendant;
    }
}
