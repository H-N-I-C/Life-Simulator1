using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class CultMethodRepairs
{
    public static void FixRelationshipPerson(MethodDefinition method)
    {
        var module = method.Module;
        var personType = FindType(module, "SimPerson");
        var childType = FindType(module, "SimChild");
        var lifeType = FindType(module, "Life");
        var gameManager = FindType(module, "GameManager");
        var instField = Field(module, gameManager, "Inst");
        var lifeField = Field(module, gameManager, "Life");
        var loverField = Field(module, lifeType, "<Lover>k__BackingField");
        var fatherField = Field(module, lifeType, "<Father>k__BackingField");
        var motherField = Field(module, lifeType, "<Mother>k__BackingField");
        var aliveField = Field(module, personType, "<Alive>k__BackingField");
        var friendStatusField = Field(module, personType, "<FriendStatus>k__BackingField");
        var getChildren = ImportMethod(module, lifeType, "get_ChildArray", 0, true);
        var getFriends = ImportMethod(module, lifeType, "get_FriendArray", 0, true);
        var tupleType = (GenericInstanceType)method.ReturnType;
        var tupleConstructor = ImportMethod(module, tupleType, ".ctor", 2, true);
        var candidatesType = GenericType(module, "System.Collections.Generic", "List`1", tupleType);
        var candidatesConstructor = ImportMethod(module, candidatesType, ".ctor", 0, true);
        var candidatesAdd = ImportMethod(module, candidatesType, "Add", 1, true);
        var candidatesGetItem = ImportMethod(module, candidatesType, "get_Item", 1, true);
        var candidatesGetCount = ImportMethod(module, candidatesType, "get_Count", 0, true);
        var random = ImportMethod(module, FindType(module, "GameReference"), "GetRandomNumber", 2, false);
        var arrayListGetEnumerator = FrameworkMethod(module, "System.Collections.ArrayList", "GetEnumerator", 0, true);
        var enumeratorGetCurrent = FrameworkMethod(module, "System.Collections.IEnumerator", "get_Current", 0, true);
        var enumeratorMoveNext = FrameworkMethod(module, "System.Collections.IEnumerator", "MoveNext", 0, true);
        var disposableType = FrameworkType(module, "System", "IDisposable");
        var dispose = FrameworkMethod(module, "System.IDisposable", "Dispose", 0, true);

        var body = FreshBody(method);
        var candidates = new VariableDefinition(candidatesType);
        var life = new VariableDefinition(lifeType);
        var child = new VariableDefinition(childType);
        var friend = new VariableDefinition(personType);
        var enumerator = new VariableDefinition(arrayListGetEnumerator.ReturnType);
        var disposable = new VariableDefinition(disposableType);
        foreach (var local in new[] { candidates, life, child, friend, enumerator, disposable }) body.Variables.Add(local);
        var il = body.Instructions;

        il.Add(Instruction.Create(OpCodes.Newobj, candidatesConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, candidates));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Stloc, life));

        void EmitLover()
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, candidates));
            il.Add(Instruction.Create(OpCodes.Ldloc, life));
            il.Add(Instruction.Create(OpCodes.Ldfld, loverField));
            il.Add(Instruction.Create(OpCodes.Ldstr, "Lover"));
            il.Add(Instruction.Create(OpCodes.Newobj, tupleConstructor));
            il.Add(Instruction.Create(OpCodes.Callvirt, candidatesAdd));
        }

        void EmitParent(FieldReference parentField)
        {
            var skip = Instruction.Create(OpCodes.Nop);
            il.Add(Instruction.Create(OpCodes.Ldloc, life));
            il.Add(Instruction.Create(OpCodes.Ldfld, parentField));
            il.Add(Instruction.Create(OpCodes.Brfalse, skip));
            il.Add(Instruction.Create(OpCodes.Ldloc, life));
            il.Add(Instruction.Create(OpCodes.Ldfld, parentField));
            il.Add(Instruction.Create(OpCodes.Ldfld, aliveField));
            il.Add(Instruction.Create(OpCodes.Brfalse, skip));
            il.Add(Instruction.Create(OpCodes.Ldloc, candidates));
            il.Add(Instruction.Create(OpCodes.Ldloc, life));
            il.Add(Instruction.Create(OpCodes.Ldfld, parentField));
            il.Add(Instruction.Create(OpCodes.Ldstr, "Parent"));
            il.Add(Instruction.Create(OpCodes.Newobj, tupleConstructor));
            il.Add(Instruction.Create(OpCodes.Callvirt, candidatesAdd));
            il.Add(skip);
        }

        var loverDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, loverField));
        il.Add(Instruction.Create(OpCodes.Brfalse, loverDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, loverField));
        il.Add(Instruction.Create(OpCodes.Ldfld, aliveField));
        il.Add(Instruction.Create(OpCodes.Brfalse, loverDone));
        EmitLover();
        EmitLover();
        il.Add(loverDone);

        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Call, getChildren));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        var childTry = Instruction.Create(OpCodes.Nop);
        var childLoop = Instruction.Create(OpCodes.Nop);
        var childLoopCheck = Instruction.Create(OpCodes.Nop);
        var nextChild = Instruction.Create(OpCodes.Nop);
        var childDisposal = Instruction.Create(OpCodes.Nop);
        var childDisposalDone = Instruction.Create(OpCodes.Nop);
        var childDisposalEnd = Instruction.Create(OpCodes.Nop);
        var childrenDone = Instruction.Create(OpCodes.Nop);
        il.Add(childTry);
        il.Add(Instruction.Create(OpCodes.Br, childLoopCheck));
        il.Add(childLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, enumeratorGetCurrent));
        il.Add(Instruction.Create(OpCodes.Castclass, childType));
        il.Add(Instruction.Create(OpCodes.Stloc, child));
        il.Add(Instruction.Create(OpCodes.Ldloc, child));
        il.Add(Instruction.Create(OpCodes.Ldfld, aliveField));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextChild));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidates));
        il.Add(Instruction.Create(OpCodes.Ldloc, child));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Child"));
        il.Add(Instruction.Create(OpCodes.Newobj, tupleConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, candidatesAdd));
        il.Add(nextChild);
        il.Add(childLoopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, enumeratorMoveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, childLoop));
        il.Add(Instruction.Create(OpCodes.Leave, childrenDone));
        il.Add(childDisposal);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Isinst, disposableType));
        il.Add(Instruction.Create(OpCodes.Stloc, disposable));
        il.Add(Instruction.Create(OpCodes.Ldloc, disposable));
        il.Add(Instruction.Create(OpCodes.Brfalse, childDisposalDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, disposable));
        il.Add(Instruction.Create(OpCodes.Callvirt, dispose));
        il.Add(childDisposalDone);
        il.Add(Instruction.Create(OpCodes.Endfinally));
        il.Add(childDisposalEnd);
        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = childTry,
            TryEnd = childDisposal,
            HandlerStart = childDisposal,
            HandlerEnd = childDisposalEnd
        });
        il.Add(childrenDone);

        EmitParent(motherField);
        EmitParent(fatherField);

        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Call, getFriends));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        var friendTry = Instruction.Create(OpCodes.Nop);
        var friendLoop = Instruction.Create(OpCodes.Nop);
        var friendLoopCheck = Instruction.Create(OpCodes.Nop);
        var nextFriend = Instruction.Create(OpCodes.Nop);
        var friendDisposal = Instruction.Create(OpCodes.Nop);
        var friendDisposalDone = Instruction.Create(OpCodes.Nop);
        var friendDisposalEnd = Instruction.Create(OpCodes.Nop);
        var friendsDone = Instruction.Create(OpCodes.Nop);
        il.Add(friendTry);
        il.Add(Instruction.Create(OpCodes.Br, friendLoopCheck));
        il.Add(friendLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, enumeratorGetCurrent));
        il.Add(Instruction.Create(OpCodes.Castclass, personType));
        il.Add(Instruction.Create(OpCodes.Stloc, friend));
        il.Add(Instruction.Create(OpCodes.Ldloc, friend));
        il.Add(Instruction.Create(OpCodes.Ldfld, friendStatusField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Beq, nextFriend));
        il.Add(Instruction.Create(OpCodes.Ldloc, friend));
        il.Add(Instruction.Create(OpCodes.Ldfld, aliveField));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextFriend));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidates));
        il.Add(Instruction.Create(OpCodes.Ldloc, friend));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Friend"));
        il.Add(Instruction.Create(OpCodes.Newobj, tupleConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, candidatesAdd));
        il.Add(nextFriend);
        il.Add(friendLoopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, enumeratorMoveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, friendLoop));
        il.Add(Instruction.Create(OpCodes.Leave, friendsDone));
        il.Add(friendDisposal);
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Isinst, disposableType));
        il.Add(Instruction.Create(OpCodes.Stloc, disposable));
        il.Add(Instruction.Create(OpCodes.Ldloc, disposable));
        il.Add(Instruction.Create(OpCodes.Brfalse, friendDisposalDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, disposable));
        il.Add(Instruction.Create(OpCodes.Callvirt, dispose));
        il.Add(friendDisposalDone);
        il.Add(Instruction.Create(OpCodes.Endfinally));
        il.Add(friendDisposalEnd);
        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = friendTry,
            TryEnd = friendDisposal,
            HandlerStart = friendDisposal,
            HandlerEnd = friendDisposalEnd
        });
        il.Add(friendsDone);

        var noCandidates = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, candidates));
        il.Add(Instruction.Create(OpCodes.Callvirt, candidatesGetCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, noCandidates));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidates));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, candidates));
        il.Add(Instruction.Create(OpCodes.Callvirt, candidatesGetCount));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Callvirt, candidatesGetItem));
        il.Add(Instruction.Create(OpCodes.Ret));
        il.Add(noCandidates);
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Newobj, tupleConstructor));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixRelationshipEligibility(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var personType = FindType(module, "SimPerson");
        var lifeType = FindType(module, "Life");
        var gameManager = FindType(module, "GameManager");
        var instField = Field(module, gameManager, "Inst");
        var lifeField = Field(module, gameManager, "Life");
        var heroField = Field(module, lifeType, "<Hero>k__BackingField");
        var getPlaceOfResidence = ImportMethod(module, personType, "get_PlaceOfResidence", 0, true);
        var cultField = Field(module, getPlaceOfResidence.ReturnType, "<Cult>k__BackingField");
        var ageField = Field(module, personType, "<Age>k__BackingField");
        var dataField = Field(module, owner, "_data");
        var getPerson = ImportMethod(module, owner, "GetPersonFromHeroLife", 0, false);
        var tupleType = (GenericInstanceType)getPerson.ReturnType;
        var item1Field = Field(module, tupleType, "Item1");
        item1Field.DeclaringType = tupleType;
        var item2Field = Field(module, tupleType, "Item2");
        item2Field.DeclaringType = tupleType;
        var relationshipStrength = ImportMethod(module, personType, "GetTotalHeroRelationshipStrength", 0, true);
        var random = ImportMethod(module, FindType(module, "GameReference"), "GetRandomNumber", 2, false);
        var getData = ImportMethod(module, FindType(module, "DataHub"), "GetCultAntiCommuneNagScenarioItem", 2, false, "System.String", "System.Int32");

        var body = FreshBody(method);
        var person = new VariableDefinition(tupleType);
        var data = new VariableDefinition(getData.ReturnType);
        body.Variables.Add(person);
        body.Variables.Add(data);
        var il = body.Instructions;
        var cultPresent = Instruction.Create(OpCodes.Nop);
        var personPresent = Instruction.Create(OpCodes.Nop);
        var lookup = Instruction.Create(OpCodes.Nop);
        var dataPresent = Instruction.Create(OpCodes.Nop);
        var notEligible = Instruction.Create(OpCodes.Nop);

        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, heroField));
        il.Add(Instruction.Create(OpCodes.Call, getPlaceOfResidence));
        il.Add(Instruction.Create(OpCodes.Ldfld, cultField));
        il.Add(Instruction.Create(OpCodes.Brtrue, cultPresent));
        il.Add(Instruction.Create(OpCodes.Br, notEligible));
        il.Add(cultPresent);
        il.Add(Instruction.Create(OpCodes.Call, getPerson));
        il.Add(Instruction.Create(OpCodes.Stloc, person));
        il.Add(Instruction.Create(OpCodes.Ldloca, person));
        il.Add(Instruction.Create(OpCodes.Ldfld, item1Field));
        il.Add(Instruction.Create(OpCodes.Brtrue, personPresent));
        il.Add(Instruction.Create(OpCodes.Br, notEligible));
        il.Add(personPresent);
        il.Add(Instruction.Create(OpCodes.Ldloca, person));
        il.Add(Instruction.Create(OpCodes.Ldfld, item1Field));
        il.Add(Instruction.Create(OpCodes.Callvirt, relationshipStrength));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 100));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Bgt_Un, lookup));
        il.Add(Instruction.Create(OpCodes.Br, notEligible));
        il.Add(lookup);
        il.Add(Instruction.Create(OpCodes.Ldloca, person));
        il.Add(Instruction.Create(OpCodes.Ldfld, item2Field));
        il.Add(Instruction.Create(OpCodes.Ldloca, person));
        il.Add(Instruction.Create(OpCodes.Ldfld, item1Field));
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        il.Add(Instruction.Create(OpCodes.Call, getData));
        il.Add(Instruction.Create(OpCodes.Stloc, data));
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Brtrue, dataPresent));
        il.Add(Instruction.Create(OpCodes.Br, notEligible));
        il.Add(dataPresent);
        il.Add(Instruction.Create(OpCodes.Ldloc, data));
        il.Add(Instruction.Create(OpCodes.Stsfld, dataField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Ret));
        il.Add(notEligible);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixDoctrineSetup(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var hasDismissField = Field(module, owner, "HasDismissButton");
        var exteriorTitleField = Field(module, owner, "ExteriorTitle");
        var titleField = Field(module, owner, "Title");
        var bodyField = Field(module, owner, "Body");
        var dropDownArrayField = Field(module, owner, "DropDownArray");
        var choiceArrayField = Field(module, owner, "ChoiceArray");
        var dataHub = FindType(module, "DataHub");
        var loadRules = ImportMethod(module, dataHub, "LoadCultDoctrineRuleSet", 0, false);
        var getRules = ImportMethod(module, dataHub, "GetRandomSetCultDoctrineRule", 2, false);
        var rulesType = (GenericInstanceType)getRules.ReturnType;
        var rulesGetEnumerator = ImportMethod(module, rulesType, "GetEnumerator", 0, true);
        var enumeratorType = ListEnumeratorType(module, rulesType);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);
        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var dropDownConstructor = ImportMethod(module, FindType(module, "ScenarioDropDown"), ".ctor", 3, true);
        var choiceConstructor = ImportMethod(module, FindType(module, "ScenarioChoice"), ".ctor", 3, true, "System.String", "System.Boolean", "System.Boolean");

        var body = FreshBody(method);
        var rules = new VariableDefinition(rulesType);
        var first = new VariableDefinition(arrayListType);
        var second = new VariableDefinition(arrayListType);
        var third = new VariableDefinition(arrayListType);
        var counter = new VariableDefinition(module.TypeSystem.Int32);
        var enumerator = new VariableDefinition(enumeratorType);
        var rule = new VariableDefinition(module.TypeSystem.String);
        foreach (var local in new[] { rules, first, second, third, counter, enumerator, rule }) body.Variables.Add(local);
        var il = body.Instructions;

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stfld, hasDismissField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Commune"));
        il.Add(Instruction.Create(OpCodes.Stfld, exteriorTitleField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Doctrine"));
        il.Add(Instruction.Create(OpCodes.Stfld, titleField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Determine which rules will serve as the foundation of your commune's belief system."));
        il.Add(Instruction.Create(OpCodes.Stfld, bodyField));
        il.Add(Instruction.Create(OpCodes.Call, loadRules));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 15));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Call, getRules));
        il.Add(Instruction.Create(OpCodes.Stloc, rules));
        foreach (var list in new[] { first, second, third })
        {
            il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, list));
        }
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, counter));
        il.Add(Instruction.Create(OpCodes.Ldloc, rules));
        il.Add(Instruction.Create(OpCodes.Callvirt, rulesGetEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        var tryStart = Instruction.Create(OpCodes.Nop);
        var loop = Instruction.Create(OpCodes.Nop);
        var loopCheck = Instruction.Create(OpCodes.Nop);
        var notFirst = Instruction.Create(OpCodes.Nop);
        var notSecond = Instruction.Create(OpCodes.Nop);
        var added = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var disposalEnd = Instruction.Create(OpCodes.Nop);
        var loopDone = Instruction.Create(OpCodes.Nop);
        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loop);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, rule));
        il.Add(Instruction.Create(OpCodes.Ldloc, counter));
        il.Add(Instruction.Create(OpCodes.Brtrue, notFirst));
        il.Add(Instruction.Create(OpCodes.Ldloc, first));
        il.Add(Instruction.Create(OpCodes.Ldloc, rule));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Br, added));
        il.Add(notFirst);
        il.Add(Instruction.Create(OpCodes.Ldloc, counter));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Bne_Un, notSecond));
        il.Add(Instruction.Create(OpCodes.Ldloc, second));
        il.Add(Instruction.Create(OpCodes.Ldloc, rule));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Br, added));
        il.Add(notSecond);
        il.Add(Instruction.Create(OpCodes.Ldloc, third));
        il.Add(Instruction.Create(OpCodes.Ldloc, rule));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(added);
        il.Add(Instruction.Create(OpCodes.Ldloc, counter));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, counter));
        il.Add(Instruction.Create(OpCodes.Ldloc, counter));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Bne_Un, loopCheck));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, counter));
        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loop));
        il.Add(Instruction.Create(OpCodes.Leave, loopDone));
        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, dispose));
        il.Add(Instruction.Create(OpCodes.Endfinally));
        il.Add(disposalEnd);
        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = tryStart,
            TryEnd = disposal,
            HandlerStart = disposal,
            HandlerEnd = disposalEnd
        });
        il.Add(loopDone);

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        foreach (var (text, list) in new[]
        {
            ("Decide your first rule", first),
            ("Decide your second rule", second),
            ("Decide your third rule", third)
        })
        {
            il.Add(Instruction.Create(OpCodes.Dup));
            il.Add(Instruction.Create(OpCodes.Ldstr, text));
            il.Add(Instruction.Create(OpCodes.Ldloc, list));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Newobj, dropDownConstructor));
            il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
            il.Add(Instruction.Create(OpCodes.Pop));
        }
        il.Add(Instruction.Create(OpCodes.Stfld, dropDownArrayField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Select"));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Newobj, choiceConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Stfld, choiceArrayField));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixPrisonTeach(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var gameManager = FindType(module, "GameManager");
        var lifeType = FindType(module, "Life");
        var personType = FindType(module, "SimPerson");
        var gameReference = FindType(module, "GameReference");
        var instField = Field(module, gameManager, "Inst");
        var lifeField = Field(module, gameManager, "Life");
        var heroField = Field(module, lifeType, "<Hero>k__BackingField");
        var ageField = Field(module, personType, "<Age>k__BackingField");
        var charismaField = Field(module, personType, "<Att_charisma>k__BackingField");
        var cultField = Field(module, owner, "_cult");
        var teachOptionsField = Field(module, owner, "_teachOptions");
        var narrativeField = Field(module, owner, "_narrative");
        var nextScenarioField = Field(module, owner, "<NextScenario>k__BackingField");
        var lastPullField = Field(module, cultField.FieldType, "<HeroAgeAtLastPullInteractionWithCult>k__BackingField");
        var lastTeachField = Field(module, cultField.FieldType, "<HeroAgeAtLastTeachInPrison>k__BackingField");
        var followersField = Field(module, cultField.FieldType, "<Followers>k__BackingField");
        var cultPhilosophyField = Field(module, cultField.FieldType, "<Philosophy>k__BackingField");
        var teachOptionsType = (GenericInstanceType)teachOptionsField.FieldType;
        var teachOptionType = teachOptionsType.GenericArguments[0];
        var topicField = Field(module, teachOptionType, "<Topic>k__BackingField");
        var optionPhilosophyField = Field(module, teachOptionType, "<Philosophy>k__BackingField");
        var getPlaceOfResidence = ImportMethod(module, personType, "get_PlaceOfResidence", 0, true);
        var placeCultField = Field(module, getPlaceOfResidence.ReturnType, "<Cult>k__BackingField");
        var adjustFollowers = ImportMethod(module, cultField.FieldType, "AdjustFollowers", 2, true);
        var impactCharisma = ImportMethod(module, personType, "ImpactCharisma", 1, true);
        var impactHappiness = ImportMethod(module, personType, "ImpactHappiness", 1, true);
        var random = ImportMethod(module, gameReference, "GetRandomNumber", 2, false);
        var pickTitle = ImportMethod(module, gameReference, "PickRandomFromStringArray", 1, false);
        var uncapitalize = ImportMethod(module, gameReference, "UncapitalizeFirstLetterOfSentenceString", 1, false);
        var bracketParserType = FindType(module, "BracketParser");
        var bracketParserConstructor = ImportMethod(module, bracketParserType, ".ctor", 0, true);
        var getParsedBody = ImportMethod(module, bracketParserType, "GetParsedBodyString", 1, true);
        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var arrayListGetItem = FrameworkMethod(module, "System.Collections.ArrayList", "get_Item", 1, true);
        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var stringEquals = FrameworkMethod(module, "System.String", "Equals", 1, true, "System.String");
        var toLower = FrameworkMethod(module, "System.String", "ToLower", 0, true);
        var concat = FrameworkMethod(module, "System.String", "Concat", 1, false, "System.String[]");
        var scenarioType = FindType(module, "Scenario");
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 3, true, "System.String", "System.String", "Scenario");
        var optionsGetEnumerator = ImportMethod(module, teachOptionsType, "GetEnumerator", 0, true);
        var enumeratorType = ListEnumeratorType(module, teachOptionsType);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);

        var body = FreshBody(method);
        var selectedTopic = new VariableDefinition(module.TypeSystem.String);
        var selectedOption = new VariableDefinition(teachOptionType);
        var enumerator = new VariableDefinition(enumeratorType);
        var option = new VariableDefinition(teachOptionType);
        var parsedText = new VariableDefinition(module.TypeSystem.String);
        var title = new VariableDefinition(module.TypeSystem.String);
        var resultBody = new VariableDefinition(module.TypeSystem.String);
        var chainedScenario = new VariableDefinition(scenarioType);
        foreach (var local in new[] { selectedTopic, selectedOption, enumerator, option, parsedText, title, resultBody, chainedScenario }) body.Variables.Add(local);
        var il = body.Instructions;
        var dropDownStrings = method.Parameters.Single(parameter => parameter.Name == "dropDownStrings");

        void EmitLoadHero()
        {
            il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
            il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
            il.Add(Instruction.Create(OpCodes.Ldfld, heroField));
        }

        il.Add(Instruction.Create(OpCodes.Ldarg, dropDownStrings));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListGetItem));
        il.Add(Instruction.Create(OpCodes.Castclass, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedTopic));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, cultField));
        EmitLoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        il.Add(Instruction.Create(OpCodes.Stfld, lastPullField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, cultField));
        EmitLoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, ageField));
        il.Add(Instruction.Create(OpCodes.Stfld, lastTeachField));

        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedOption));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, teachOptionsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, optionsGetEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
        var tryStart = Instruction.Create(OpCodes.Nop);
        var optionLoop = Instruction.Create(OpCodes.Nop);
        var optionLoopCheck = Instruction.Create(OpCodes.Nop);
        var nextOption = Instruction.Create(OpCodes.Nop);
        var disposal = Instruction.Create(OpCodes.Nop);
        var disposalEnd = Instruction.Create(OpCodes.Nop);
        var optionLoopDone = Instruction.Create(OpCodes.Nop);
        il.Add(tryStart);
        il.Add(Instruction.Create(OpCodes.Br, optionLoopCheck));
        il.Add(optionLoop);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, option));
        il.Add(Instruction.Create(OpCodes.Ldloc, option));
        il.Add(Instruction.Create(OpCodes.Ldfld, topicField));
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedTopic));
        il.Add(Instruction.Create(OpCodes.Callvirt, stringEquals));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextOption));
        il.Add(Instruction.Create(OpCodes.Ldloc, option));
        il.Add(Instruction.Create(OpCodes.Stloc, selectedOption));
        il.Add(Instruction.Create(OpCodes.Leave, optionLoopDone));
        il.Add(nextOption);
        il.Add(optionLoopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, optionLoop));
        il.Add(Instruction.Create(OpCodes.Leave, optionLoopDone));
        il.Add(disposal);
        il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
        il.Add(Instruction.Create(OpCodes.Call, dispose));
        il.Add(Instruction.Create(OpCodes.Endfinally));
        il.Add(disposalEnd);
        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = tryStart,
            TryEnd = disposal,
            HandlerStart = disposal,
            HandlerEnd = disposalEnd
        });
        il.Add(optionLoopDone);

        var philosophyCheck = Instruction.Create(OpCodes.Nop);
        var success = Instruction.Create(OpCodes.Nop);
        var failure = Instruction.Create(OpCodes.Nop);
        var outcomeReady = Instruction.Create(OpCodes.Nop);
        EmitLoadHero();
        il.Add(Instruction.Create(OpCodes.Ldfld, charismaField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4, 101));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Conv_R4));
        il.Add(Instruction.Create(OpCodes.Bge_Un, philosophyCheck));
        il.Add(Instruction.Create(OpCodes.Br, failure));
        il.Add(philosophyCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, selectedOption));
        il.Add(Instruction.Create(OpCodes.Ldfld, optionPhilosophyField));
        EmitLoadHero();
        il.Add(Instruction.Create(OpCodes.Call, getPlaceOfResidence));
        il.Add(Instruction.Create(OpCodes.Ldfld, placeCultField));
        il.Add(Instruction.Create(OpCodes.Ldfld, cultPhilosophyField));
        il.Add(Instruction.Create(OpCodes.Beq, success));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
        il.Add(Instruction.Create(OpCodes.Call, random));
        il.Add(Instruction.Create(OpCodes.Brtrue, failure));
        il.Add(success);

        void EmitParsedText(string text)
        {
            il.Add(Instruction.Create(OpCodes.Newobj, bracketParserConstructor));
            il.Add(Instruction.Create(OpCodes.Ldstr, text));
            il.Add(Instruction.Create(OpCodes.Call, getParsedBody));
            il.Add(Instruction.Create(OpCodes.Call, uncapitalize));
            il.Add(Instruction.Create(OpCodes.Stloc, parsedText));
        }

        void EmitFollowerAdjustment(int minimum, int maximum, bool negate)
        {
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, cultField));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, minimum));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, maximum));
            il.Add(Instruction.Create(OpCodes.Call, random));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Ldc_R4, 100f));
            il.Add(Instruction.Create(OpCodes.Div));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldfld, cultField));
            il.Add(Instruction.Create(OpCodes.Ldfld, followersField));
            il.Add(Instruction.Create(OpCodes.Conv_R4));
            il.Add(Instruction.Create(OpCodes.Mul));
            il.Add(Instruction.Create(OpCodes.Conv_I4));
            if (negate) il.Add(Instruction.Create(OpCodes.Neg));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(OpCodes.Call, adjustFollowers));
        }

        void EmitTitle(params string[] choices)
        {
            il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
            foreach (var choice in choices)
            {
                il.Add(Instruction.Create(OpCodes.Dup));
                il.Add(Instruction.Create(OpCodes.Ldstr, choice));
                il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
                il.Add(Instruction.Create(OpCodes.Pop));
            }
            il.Add(Instruction.Create(OpCodes.Call, pickTitle));
            il.Add(Instruction.Create(OpCodes.Stloc, title));
        }

        void EmitResultBody()
        {
            il.Add(Instruction.Create(OpCodes.Ldc_I4_5));
            il.Add(Instruction.Create(OpCodes.Newarr, module.TypeSystem.String));
            il.Add(Instruction.Create(OpCodes.Dup));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(OpCodes.Ldstr, "Several of your followers "));
            il.Add(Instruction.Create(OpCodes.Stelem_Ref));
            il.Add(Instruction.Create(OpCodes.Dup));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Ldloc, parsedText));
            il.Add(Instruction.Create(OpCodes.Stelem_Ref));
            il.Add(Instruction.Create(OpCodes.Dup));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
            il.Add(Instruction.Create(OpCodes.Ldstr, " after they received your manifesto about "));
            il.Add(Instruction.Create(OpCodes.Stelem_Ref));
            il.Add(Instruction.Create(OpCodes.Dup));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
            il.Add(Instruction.Create(OpCodes.Ldloc, selectedTopic));
            il.Add(Instruction.Create(OpCodes.Callvirt, toLower));
            il.Add(Instruction.Create(OpCodes.Stelem_Ref));
            il.Add(Instruction.Create(OpCodes.Dup));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_4));
            il.Add(Instruction.Create(OpCodes.Ldstr, "."));
            il.Add(Instruction.Create(OpCodes.Stelem_Ref));
            il.Add(Instruction.Create(OpCodes.Call, concat));
            il.Add(Instruction.Create(OpCodes.Stloc, resultBody));
        }

        void EmitHeroImpacts(int charisma, int happiness)
        {
            EmitLoadHero();
            il.Add(Instruction.Create(OpCodes.Ldc_I4, charisma));
            il.Add(Instruction.Create(OpCodes.Call, impactCharisma));
            EmitLoadHero();
            il.Add(Instruction.Create(OpCodes.Ldc_I4, happiness));
            il.Add(Instruction.Create(OpCodes.Callvirt, impactHappiness));
        }

        EmitParsedText("sent letters <wishing for your swift release/of encouragement/wishing you well/conveying their devotion/saying you're not guilty, just misunderstood>");
        EmitFollowerAdjustment(1, 3, false);
        EmitTitle("And on the third day...", "Thank you! Thank you!", "Motivational speaker", "Charisma's through the roof");
        EmitResultBody();
        EmitHeroImpacts(2, 2);
        il.Add(Instruction.Create(OpCodes.Br, outcomeReady));

        il.Add(failure);
        EmitParsedText("sent letters <questioning your judgment/asking if you're okay/wondering if you have cabin fever/wondering if you're alright/questioning your sanity>");
        EmitFollowerAdjustment(1, 4, true);
        EmitTitle("That's not what I...", "Four score and seven years ago-", "*Snore*", "You got everybody hype...");
        EmitResultBody();
        EmitHeroImpacts(-3, -2);
        il.Add(outcomeReady);

        il.Add(Instruction.Create(OpCodes.Ldloc, title));
        il.Add(Instruction.Create(OpCodes.Ldloc, resultBody));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, chainedScenario));
        il.Add(Instruction.Create(OpCodes.Ldloc, chainedScenario));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, chainedScenario));
        il.Add(Instruction.Create(OpCodes.Stfld, nextScenarioField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "I sent a missive to my followers from prison."));
        il.Add(Instruction.Create(OpCodes.Stfld, narrativeField));
        EmitLifeLogAndRefresh(module, il, narrativeField);
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static void EmitLifeLogAndRefresh(ModuleDefinition module, Mono.Collections.Generic.Collection<Instruction> il, FieldReference narrativeField)
    {
        var gameManager = FindType(module, "GameManager");
        var lifeField = Field(module, gameManager, "Life");
        var instField = Field(module, gameManager, "Inst");
        var lifeLogField = Field(module, lifeField.FieldType, "<LifeLog>k__BackingField");
        var lifeInterfaceField = Field(module, lifeField.FieldType, "LifeInterface");
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeLogField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, narrativeField));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, lifeLogField.FieldType, "Log", 1, true)));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeInterfaceField));
        il.Add(Instruction.Create(OpCodes.Call, ImportMethod(module, lifeInterfaceField.FieldType, "RefreshUI", 0, true)));
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static TypeReference FrameworkType(ModuleDefinition module, string namespaceName, string name)
    {
        var type = new TypeReference(namespaceName, name, module, module.TypeSystem.Object.Scope);
        return module.ImportReference(type);
    }

    private static GenericInstanceType GenericType(ModuleDefinition module, string namespaceName, string name, params TypeReference[] arguments)
    {
        var definition = FrameworkType(module, namespaceName, name).Resolve();
        var instance = new GenericInstanceType(module.ImportReference(definition));
        foreach (var argument in arguments) instance.GenericArguments.Add(module.ImportReference(argument));
        return instance;
    }

    private static GenericInstanceType ListEnumeratorType(ModuleDefinition module, GenericInstanceType listType)
    {
        var definition = listType.Resolve().NestedTypes.Single(type => type.Name == "Enumerator");
        var instance = new GenericInstanceType(module.ImportReference(definition));
        foreach (var argument in listType.GenericArguments) instance.GenericArguments.Add(module.ImportReference(argument));
        return instance;
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name, int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        foreach (var type in Hierarchy(declaringType.Resolve()))
        {
            var match = type.Methods.SingleOrDefault(candidate =>
                candidate.Name == name && candidate.Parameters.Count == parameterCount &&
                (!instance.HasValue || candidate.IsStatic != instance.Value) &&
                (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameterTypes)));
            if (match is null) continue;
            var reference = module.ImportReference(match);
            if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
            return reference;
        }
        throw new InvalidOperationException($"Method not found: {declaringType.FullName}::{name}/{parameterCount}");
    }

    private static MethodReference FrameworkMethod(ModuleDefinition module, string typeName, string methodName, int parameterCount, bool instance, params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = new TypeReference(typeName.Substring(0, separator), typeName.Substring(separator + 1), module, module.TypeSystem.Object.Scope);
        return ImportMethod(module, type, methodName, parameterCount, instance, parameterTypes);
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        foreach (var type in Hierarchy(declaringType.Resolve()))
        {
            var field = type.Fields.SingleOrDefault(candidate => candidate.Name == name);
            if (field is not null) return module.ImportReference(field);
        }
        throw new InvalidOperationException($"Field not found: {declaringType.FullName}::{name}");
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
        foreach (var descendant in Flatten(nested)) yield return descendant;
    }
}
