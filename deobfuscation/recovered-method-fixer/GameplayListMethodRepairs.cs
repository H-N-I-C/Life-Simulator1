using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class GameplayListMethodRepairs
{
    public static void FixStreetPeople(MethodDefinition method)
    {
        var module = method.Module;
        var personType = FindType(module, "SimStreetPerson");
        var simPersonType = FindType(module, "SimPerson");
        var hustlerType = FindType(module, "SimSpecialJobHustler");
        var streetField = Field(module, hustlerType, "<Street>k__BackingField");
        var streetType = streetField.FieldType;
        var listItemType = FindType(module, "ListItem");
        var selectionListType = FindType(module, "List");
        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");

        var getHustler = ImportMethod(module, hustlerType, "GetCurrentSpecialJobHustler", 0, false);
        var refreshPeople = ImportMethod(module, streetType, "RefreshAvailablePeopleOnStreet", 0, true);
        var getPeople = ImportMethod(module, streetType, "get_PeopleArray", 0, true);
        var resultConstructor = ImportMethod(module, arrayListType, ".ctor", 0, true);
        var copyConstructor = ImportMethod(module, arrayListType, ".ctor", 1, true, "System.Collections.ICollection");
        var toArray = ImportMethod(module, arrayListType, "ToArray", 1, true, "System.Type");
        var arrayListAdd = ImportMethod(module, arrayListType, "Add", 1, true, "System.Object");
        var getTypeFromHandle = FrameworkMethod(module, "System.Type", "GetTypeFromHandle", 1, false, "System.RuntimeTypeHandle");

        var closureType = method.DeclaringType.NestedTypes.Single(type => type.Name == "<>c");
        var delegates = Enumerable.Range(0, 6)
            .Select(index => CreateCachedDelegate(module, closureType, $"<AvailableStreetPersonArray>b__71_{index}", $"<>9__71_{index}"))
            .ToArray();

        var cast = LinqMethod(module, "Cast", 1, null, personType);
        var orderBy = LinqMethod(module, "OrderBy", 2, null, personType, delegates[0].Type.GenericArguments[1]);
        var thenByProfessionalism = LinqMethod(module, "ThenBy", 2, null, personType, delegates[1].Type.GenericArguments[1]);
        var thenByName = LinqMethod(module, "ThenBy", 2, null, personType, delegates[2].Type.GenericArguments[1]);
        var where = LinqMethod(module, "Where", 2,
            candidate => ((GenericInstanceType)candidate.Parameters[1].ParameterType).GenericArguments.Count == 2,
            personType);
        var count = LinqMethod(module, "Count", 1, null, personType);

        var enumerableType = GenericType(FrameworkType(module, "System.Collections.Generic", "IEnumerable`1"), personType);
        var orderedType = GenericType(FrameworkType(module, "System.Linq", "IOrderedEnumerable`1"), personType);
        var enumeratorType = GenericType(FrameworkType(module, "System.Collections.Generic", "IEnumerator`1"), personType);
        var getEnumerator = ImportMethod(module, enumerableType, "GetEnumerator", 0, true);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = FrameworkMethod(module, "System.Collections.IEnumerator", "MoveNext", 0, true);
        var dispose = FrameworkMethod(module, "System.IDisposable", "Dispose", 0, true);

        var gameReferenceType = FindType(module, "GameReference");
        var capitalize = ImportMethod(module, gameReferenceType, "CapitalizeString", 1, false, "System.String");
        var honorificName = ImportMethod(module, personType, "HonorificName", 0, true);
        var personField = Field(module, personType, "<Person>k__BackingField");
        var descriptionField = Field(module, personType, "<DescriptionString>k__BackingField");
        var moneyField = Field(module, simPersonType, "<Att_money>k__BackingField");
        var emojiForPerson = ImportMethod(module, FindType(module, "EmojiHub"), "EmojiForPerson", 1, false, simPersonType.FullName);
        var dividerConstructor = ImportMethod(module, listItemType, ".ctor", 1, true, "System.String");
        var itemConstructor = ImportMethod(module, listItemType, ".ctor", 6, true,
            "System.String", "System.String", "Scenario", "System.String", "System.String", "System.Boolean");
        var selectionListConstructor = ImportMethod(module, selectionListType, ".ctor", 2, true, "System.String", "System.Collections.ArrayList");
        var relationshipItems = ImportMethod(module, method.DeclaringType, "RelationshipItemArrayForStreetPerson", 1, true, personType.FullName);
        var barGraphTitleField = Field(module, listItemType, "BarGraphTitle");
        var barGraphValueField = Field(module, listItemType, "BarGraphValue");
        var selectionListField = Field(module, listItemType, "SelectionList");

        var body = FreshBody(method);
        var result = new VariableDefinition(arrayListType);
        var ordered = new VariableDefinition(orderedType);
        var regulars = new VariableDefinition(enumerableType);
        var passersby = new VariableDefinition(enumerableType);
        var regularEnumerator = new VariableDefinition(enumeratorType);
        var passerbyEnumerator = new VariableDefinition(enumeratorType);
        var person = new VariableDefinition(personType);
        var listItem = new VariableDefinition(listItemType);
        foreach (var local in new[] { result, ordered, regulars, passersby, regularEnumerator, passerbyEnumerator, person, listItem })
            body.Variables.Add(local);

        var il = body.Instructions;
        var noRegulars = Instruction.Create(OpCodes.Nop);

        // Native 0x4680a28: refresh the current street and preserve its typed-array conversion.
        il.Add(Instruction.Create(OpCodes.Newobj, resultConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, result));
        il.Add(Instruction.Create(OpCodes.Call, getHustler));
        il.Add(Instruction.Create(OpCodes.Dup));
        il.Add(Instruction.Create(OpCodes.Ldfld, streetField));
        il.Add(Instruction.Create(OpCodes.Callvirt, refreshPeople));
        il.Add(Instruction.Create(OpCodes.Ldfld, streetField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getPeople));
        il.Add(Instruction.Create(OpCodes.Newobj, copyConstructor));
        il.Add(Instruction.Create(OpCodes.Ldtoken, personType));
        il.Add(Instruction.Create(OpCodes.Call, getTypeFromHandle));
        il.Add(Instruction.Create(OpCodes.Callvirt, toArray));
        il.Add(Instruction.Create(OpCodes.Call, cast));
        EmitCachedDelegate(il, delegates[0]);
        il.Add(Instruction.Create(OpCodes.Call, orderBy));
        EmitCachedDelegate(il, delegates[1]);
        il.Add(Instruction.Create(OpCodes.Call, thenByProfessionalism));
        EmitCachedDelegate(il, delegates[2]);
        il.Add(Instruction.Create(OpCodes.Call, thenByName));
        il.Add(Instruction.Create(OpCodes.Stloc, ordered));

        il.Add(Instruction.Create(OpCodes.Ldloc, ordered));
        EmitCachedDelegate(il, delegates[3]);
        il.Add(Instruction.Create(OpCodes.Call, where));
        EmitCachedDelegate(il, delegates[4]);
        il.Add(Instruction.Create(OpCodes.Call, where));
        il.Add(Instruction.Create(OpCodes.Stloc, regulars));
        il.Add(Instruction.Create(OpCodes.Ldloc, regulars));
        il.Add(Instruction.Create(OpCodes.Call, count));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ble, noRegulars));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Regulars"));
        il.Add(Instruction.Create(OpCodes.Newobj, dividerConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        EmitPeopleLoop(regulars, regularEnumerator);

        il.Add(noRegulars);
        il.Add(Instruction.Create(OpCodes.Ldloc, ordered));
        EmitCachedDelegate(il, delegates[5]);
        il.Add(Instruction.Create(OpCodes.Call, where));
        il.Add(Instruction.Create(OpCodes.Stloc, passersby));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Passersby"));
        il.Add(Instruction.Create(OpCodes.Newobj, dividerConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        EmitPeopleLoop(passersby, passerbyEnumerator);
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ret));

        void EmitPeopleLoop(VariableDefinition sequence, VariableDefinition enumerator)
        {
            var tryStart = Instruction.Create(OpCodes.Nop);
            var loop = Instruction.Create(OpCodes.Nop);
            var loopCheck = Instruction.Create(OpCodes.Nop);
            var disposal = Instruction.Create(OpCodes.Nop);
            var done = Instruction.Create(OpCodes.Nop);

            il.Add(Instruction.Create(OpCodes.Ldloc, sequence));
            il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
            il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
            il.Add(tryStart);
            il.Add(Instruction.Create(OpCodes.Br, loopCheck));
            il.Add(loop);
            il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
            il.Add(Instruction.Create(OpCodes.Callvirt, getCurrent));
            il.Add(Instruction.Create(OpCodes.Stloc, person));

            // Native 0x46811c0-0x46812d0 and 0x468159c-0x46816a8.
            il.Add(Instruction.Create(OpCodes.Ldloc, person));
            il.Add(Instruction.Create(OpCodes.Callvirt, honorificName));
            il.Add(Instruction.Create(OpCodes.Call, capitalize));
            il.Add(Instruction.Create(OpCodes.Ldloc, person));
            il.Add(Instruction.Create(OpCodes.Ldfld, personField));
            il.Add(Instruction.Create(OpCodes.Call, emojiForPerson));
            il.Add(Instruction.Create(OpCodes.Ldnull));
            il.Add(Instruction.Create(OpCodes.Ldloc, person));
            il.Add(Instruction.Create(OpCodes.Ldfld, descriptionField));
            il.Add(Instruction.Create(OpCodes.Call, capitalize));
            il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(OpCodes.Newobj, itemConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, listItem));

            il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
            il.Add(Instruction.Create(OpCodes.Ldstr, "Money"));
            il.Add(Instruction.Create(OpCodes.Stfld, barGraphTitleField));
            il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
            il.Add(Instruction.Create(OpCodes.Ldloc, person));
            il.Add(Instruction.Create(OpCodes.Ldfld, personField));
            il.Add(Instruction.Create(OpCodes.Ldfld, moneyField));
            il.Add(Instruction.Create(OpCodes.Stfld, barGraphValueField));
            il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
            il.Add(Instruction.Create(OpCodes.Ldloc, person));
            il.Add(Instruction.Create(OpCodes.Ldfld, descriptionField));
            il.Add(Instruction.Create(OpCodes.Call, capitalize));
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldloc, person));
            il.Add(Instruction.Create(OpCodes.Call, relationshipItems));
            il.Add(Instruction.Create(OpCodes.Newobj, selectionListConstructor));
            il.Add(Instruction.Create(OpCodes.Stfld, selectionListField));
            il.Add(Instruction.Create(OpCodes.Ldloc, result));
            il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
            il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
            il.Add(Instruction.Create(OpCodes.Pop));

            il.Add(loopCheck);
            il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
            il.Add(Instruction.Create(OpCodes.Callvirt, moveNext));
            il.Add(Instruction.Create(OpCodes.Brtrue, loop));
            il.Add(Instruction.Create(OpCodes.Leave, done));
            il.Add(disposal);
            il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
            il.Add(Instruction.Create(OpCodes.Callvirt, dispose));
            il.Add(Instruction.Create(OpCodes.Endfinally));
            il.Add(done);
            body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
            {
                TryStart = tryStart,
                TryEnd = disposal,
                HandlerStart = disposal,
                HandlerEnd = done
            });
        }
    }

    public static void FixZooAnimals(MethodDefinition method)
    {
        var module = method.Module;
        var gameManagerType = FindType(module, "GameManager");
        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var zooField = Field(module, lifeField.FieldType, "<Zoo>k__BackingField");
        var habitatsField = Field(module, zooField.FieldType, "<HabitatsList>k__BackingField");
        var habitatsType = (GenericInstanceType)habitatsField.FieldType;
        var habitatType = habitatsType.GenericArguments[0];
        var animalsField = Field(module, habitatType, "<AnimalsList>k__BackingField");
        var animalsType = (GenericInstanceType)animalsField.FieldType;
        var animalType = animalsType.GenericArguments[0];
        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var listItemType = FindType(module, "ListItem");
        var listType = FindType(module, "List");

        var closureType = method.DeclaringType.NestedTypes.Single(type => type.Name == "<>c");
        var delegates = Enumerable.Range(0, 3)
            .Select(index => CreateCachedDelegate(module, closureType, $"<GetZooAnimalsList>b__218_{index}", $"<>9__218_{index}"))
            .ToArray();
        var orderHabitats = LinqMethod(module, "OrderBy", 2, null, habitatType, delegates[0].Type.GenericArguments[1]);
        var habitatsToList = LinqMethod(module, "ToList", 1, null, habitatType);
        var orderAnimals = LinqMethod(module, "OrderBy", 2, null, animalType, delegates[1].Type.GenericArguments[1]);
        var groupAnimals = LinqMethod(module, "GroupBy", 2, null, animalType, delegates[2].Type.GenericArguments[1]);
        var firstAnimal = LinqMethod(module, "FirstOrDefault", 1, null, animalType);
        var countAnimals = LinqMethod(module, "Count", 1, null, animalType);
        var animalsToList = LinqMethod(module, "ToList", 1, null, animalType);

        var getHabitatEnumerator = ImportMethod(module, habitatsType, "GetEnumerator", 0, true);
        var habitatEnumeratorType = ListEnumeratorType((GenericInstanceType)getHabitatEnumerator.ReturnType, habitatType);
        var getHabitatCurrent = ImportMethod(module, habitatEnumeratorType, "get_Current", 0, true);
        var moveHabitatNext = ImportMethod(module, habitatEnumeratorType, "MoveNext", 0, true);
        var disposeHabitatEnumerator = ImportMethod(module, habitatEnumeratorType, "Dispose", 0, true);
        var getAnimalsCount = ImportMethod(module, animalsType, "get_Count", 0, true);

        var groupType = GenericType(FrameworkType(module, "System.Linq", "IGrouping`2"), delegates[2].Type.GenericArguments[1], animalType);
        var groupsType = GenericType(FrameworkType(module, "System.Collections.Generic", "IEnumerable`1"), groupType);
        var groupEnumeratorType = GenericType(FrameworkType(module, "System.Collections.Generic", "IEnumerator`1"), groupType);
        var getGroupEnumerator = ImportMethod(module, groupsType, "GetEnumerator", 0, true);
        var getGroupCurrent = ImportMethod(module, groupEnumeratorType, "get_Current", 0, true);
        var moveGroupNext = FrameworkMethod(module, "System.Collections.IEnumerator", "MoveNext", 0, true);
        var dispose = FrameworkMethod(module, "System.IDisposable", "Dispose", 0, true);

        var resultConstructor = ImportMethod(module, arrayListType, ".ctor", 0, true);
        var arrayListAdd = ImportMethod(module, arrayListType, "Add", 1, true, "System.Object");
        var dividerConstructor = ImportMethod(module, listItemType, ".ctor", 1, true, "System.String");
        var animalItemConstructor = ImportMethod(module, listItemType, ".ctor", 2, true, "System.String", listType.FullName);
        var listConstructor = ImportMethod(module, listType, ".ctor", 2, true, "System.String", "System.Collections.ArrayList");
        var habitatNameField = Field(module, habitatType, "<Name>k__BackingField");
        var animalNameField = Field(module, animalType, "<Name>k__BackingField");
        var breedField = Field(module, animalType, "<Breed>k__BackingField");
        var genderField = Field(module, animalType, "<Gender>k__BackingField");
        var getAnimalData = ImportMethod(module, animalType, "get_AnimalData", 0, true);
        var breedPluralField = Field(module, getAnimalData.ReturnType, "<BreedPlural>k__BackingField");
        var emojiString = ImportMethod(module, animalType, "EmojiString", 0, true);
        var getGenderString = ImportMethod(module, animalType, "GetGenderString", 2, false);
        var getHealth = ImportMethod(module, animalType, "GetHealth", 1, true, zooField.FieldType.FullName);
        var getAverageHealth = ImportMethod(module, habitatType, "GetAverageAnimalsHealth", 0, true);
        var gameReferenceType = FindType(module, "GameReference");
        var capitalizeWords = ImportMethod(module, gameReferenceType, "CapitalizeAllMajorWordsInString", 1, false, "System.String");
        var numberGrouping = ImportMethod(module, gameReferenceType, "NumberGroupingStringForAmount", 1, false, "System.Int32");
        var concat = FrameworkMethod(module, "System.String", "Concat", 3, false, "System.String", "System.String", "System.String");
        var getSpecificAnimalList = ImportMethod(module, method.DeclaringType, "GetZooSpecificAnimalList", 1, true, animalType.FullName);
        var getBreedList = ImportMethod(module, method.DeclaringType, "GetZooAnimalsListForBreedInHabitat", 2, true, "System.String", animalsType.FullName);
        var emojiField = Field(module, listItemType, "EmojiString");
        var subtitleField = Field(module, listItemType, "Subtitle");
        var barGraphTitleField = Field(module, listItemType, "BarGraphTitle");
        var barGraphValueField = Field(module, listItemType, "BarGraphValue");

        var body = FreshBody(method);
        var result = new VariableDefinition(arrayListType);
        var habitats = new VariableDefinition(habitatsType);
        var habitatEnumerator = new VariableDefinition(habitatEnumeratorType);
        var habitat = new VariableDefinition(habitatType);
        var groups = new VariableDefinition(groupsType);
        var groupEnumerator = new VariableDefinition(groupEnumeratorType);
        var group = new VariableDefinition(groupType);
        var animal = new VariableDefinition(animalType);
        var breed = new VariableDefinition(module.TypeSystem.String);
        var groupList = new VariableDefinition(animalsType);
        var listItem = new VariableDefinition(listItemType);
        foreach (var local in new[] { result, habitats, habitatEnumerator, habitat, groups, groupEnumerator, group, animal, breed, groupList, listItem })
            body.Variables.Add(local);

        var il = body.Instructions;
        var habitatTry = Instruction.Create(OpCodes.Nop);
        var habitatLoop = Instruction.Create(OpCodes.Nop);
        var habitatCheck = Instruction.Create(OpCodes.Nop);
        var habitatDisposal = Instruction.Create(OpCodes.Nop);
        var habitatDone = Instruction.Create(OpCodes.Nop);
        var groupTry = Instruction.Create(OpCodes.Nop);
        var groupLoop = Instruction.Create(OpCodes.Nop);
        var groupCheck = Instruction.Create(OpCodes.Nop);
        var multipleAnimals = Instruction.Create(OpCodes.Nop);
        var groupDisposal = Instruction.Create(OpCodes.Nop);
        var groupDone = Instruction.Create(OpCodes.Nop);

        // Native 0x46c0f88-0x46c0fb0: GameManager.Inst.Life.Zoo.HabitatsList.
        il.Add(Instruction.Create(OpCodes.Newobj, resultConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, result));
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, zooField));
        il.Add(Instruction.Create(OpCodes.Ldfld, habitatsField));
        EmitCachedDelegate(il, delegates[0]);
        il.Add(Instruction.Create(OpCodes.Call, orderHabitats));
        il.Add(Instruction.Create(OpCodes.Call, habitatsToList));
        il.Add(Instruction.Create(OpCodes.Stloc, habitats));
        il.Add(Instruction.Create(OpCodes.Ldloc, habitats));
        il.Add(Instruction.Create(OpCodes.Callvirt, getHabitatEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, habitatEnumerator));
        il.Add(habitatTry);
        il.Add(Instruction.Create(OpCodes.Br, habitatCheck));
        il.Add(habitatLoop);
        il.Add(Instruction.Create(OpCodes.Ldloca, habitatEnumerator));
        il.Add(Instruction.Create(OpCodes.Call, getHabitatCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, habitat));
        il.Add(Instruction.Create(OpCodes.Ldloc, habitat));
        il.Add(Instruction.Create(OpCodes.Ldfld, animalsField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getAnimalsCount));
        il.Add(Instruction.Create(OpCodes.Brfalse, habitatCheck));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, habitat));
        il.Add(Instruction.Create(OpCodes.Ldfld, habitatNameField));
        il.Add(Instruction.Create(OpCodes.Newobj, dividerConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));

        il.Add(Instruction.Create(OpCodes.Ldloc, habitat));
        il.Add(Instruction.Create(OpCodes.Ldfld, animalsField));
        EmitCachedDelegate(il, delegates[1]);
        il.Add(Instruction.Create(OpCodes.Call, orderAnimals));
        EmitCachedDelegate(il, delegates[2]);
        il.Add(Instruction.Create(OpCodes.Call, groupAnimals));
        il.Add(Instruction.Create(OpCodes.Stloc, groups));
        il.Add(Instruction.Create(OpCodes.Ldloc, groups));
        il.Add(Instruction.Create(OpCodes.Callvirt, getGroupEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, groupEnumerator));
        il.Add(groupTry);
        il.Add(Instruction.Create(OpCodes.Br, groupCheck));
        il.Add(groupLoop);
        il.Add(Instruction.Create(OpCodes.Ldloc, groupEnumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, getGroupCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, group));
        il.Add(Instruction.Create(OpCodes.Ldloc, group));
        il.Add(Instruction.Create(OpCodes.Call, firstAnimal));
        il.Add(Instruction.Create(OpCodes.Stloc, animal));
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Brfalse, groupCheck));
        il.Add(Instruction.Create(OpCodes.Ldloc, group));
        il.Add(Instruction.Create(OpCodes.Call, countAnimals));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Bne_Un, multipleAnimals));

        // Native 0x46c13e8-0x46c14d0: one animal of this breed in the habitat.
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Ldfld, animalNameField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Call, getSpecificAnimalList));
        il.Add(Instruction.Create(OpCodes.Newobj, animalItemConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Callvirt, emojiString));
        il.Add(Instruction.Create(OpCodes.Stfld, emojiField));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Ldfld, genderField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Call, getGenderString));
        il.Add(Instruction.Create(OpCodes.Ldstr, " "));
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Ldfld, breedField));
        il.Add(Instruction.Create(OpCodes.Call, capitalizeWords));
        il.Add(Instruction.Create(OpCodes.Call, concat));
        il.Add(Instruction.Create(OpCodes.Stfld, subtitleField));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Health"));
        il.Add(Instruction.Create(OpCodes.Stfld, barGraphTitleField));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Callvirt, getHealth));
        il.Add(Instruction.Create(OpCodes.Stfld, barGraphValueField));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Br, groupCheck));

        // Native 0x46c14d8-0x46c15c4: collapse multiple animals into a breed entry.
        il.Add(multipleAnimals);
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Callvirt, getAnimalData));
        il.Add(Instruction.Create(OpCodes.Ldfld, breedPluralField));
        il.Add(Instruction.Create(OpCodes.Call, capitalizeWords));
        il.Add(Instruction.Create(OpCodes.Stloc, breed));
        il.Add(Instruction.Create(OpCodes.Ldloc, group));
        il.Add(Instruction.Create(OpCodes.Call, animalsToList));
        il.Add(Instruction.Create(OpCodes.Stloc, groupList));
        il.Add(Instruction.Create(OpCodes.Ldloc, breed));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, breed));
        il.Add(Instruction.Create(OpCodes.Ldloc, groupList));
        il.Add(Instruction.Create(OpCodes.Call, getBreedList));
        il.Add(Instruction.Create(OpCodes.Newobj, animalItemConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, group));
        il.Add(Instruction.Create(OpCodes.Call, countAnimals));
        il.Add(Instruction.Create(OpCodes.Call, numberGrouping));
        il.Add(Instruction.Create(OpCodes.Stfld, subtitleField));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, animal));
        il.Add(Instruction.Create(OpCodes.Callvirt, emojiString));
        il.Add(Instruction.Create(OpCodes.Stfld, emojiField));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Health"));
        il.Add(Instruction.Create(OpCodes.Stfld, barGraphTitleField));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Ldloc, habitat));
        il.Add(Instruction.Create(OpCodes.Callvirt, getAverageHealth));
        il.Add(Instruction.Create(OpCodes.Stfld, barGraphValueField));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));

        il.Add(groupCheck);
        il.Add(Instruction.Create(OpCodes.Ldloc, groupEnumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, moveGroupNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, groupLoop));
        il.Add(Instruction.Create(OpCodes.Leave, groupDone));
        il.Add(groupDisposal);
        il.Add(Instruction.Create(OpCodes.Ldloc, groupEnumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, dispose));
        il.Add(Instruction.Create(OpCodes.Endfinally));
        il.Add(groupDone);
        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = groupTry,
            TryEnd = groupDisposal,
            HandlerStart = groupDisposal,
            HandlerEnd = groupDone
        });

        il.Add(habitatCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca, habitatEnumerator));
        il.Add(Instruction.Create(OpCodes.Call, moveHabitatNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, habitatLoop));
        il.Add(Instruction.Create(OpCodes.Leave, habitatDone));
        il.Add(habitatDisposal);
        il.Add(Instruction.Create(OpCodes.Ldloca, habitatEnumerator));
        il.Add(Instruction.Create(OpCodes.Call, disposeHabitatEnumerator));
        il.Add(Instruction.Create(OpCodes.Endfinally));
        il.Add(habitatDone);
        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = habitatTry,
            TryEnd = habitatDisposal,
            HandlerStart = habitatDisposal,
            HandlerEnd = habitatDone
        });
        il.Add(Instruction.Create(OpCodes.Ldstr, "Zoo Animals"));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Newobj, listConstructor));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixJewelers(MethodDefinition method)
    {
        var module = method.Module;
        var gameManagerType = FindType(module, "GameManager");
        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var luxuryField = Field(module, lifeField.FieldType, "<Luxury>k__BackingField");
        var jewelsField = Field(module, luxuryField.FieldType, "_jewelryForSale");
        var jewelsType = (GenericInstanceType)jewelsField.FieldType;
        var jewelType = jewelsType.GenericArguments[0];
        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var listItemType = FindType(module, "ListItem");
        var listType = FindType(module, "List");

        var closureType = method.DeclaringType.NestedTypes.Single(type => type.Name == "<>c");
        var selector = CreateCachedDelegate(module, closureType, "<GetLuxuryUpscaleJewelersList>b__262_0", "<>9__262_0");
        var orderByDescending = LinqMethod(module, "OrderByDescending", 2, null, jewelType, selector.Type.GenericArguments[1]);
        var toList = LinqMethod(module, "ToList", 1, null, jewelType);
        var getEnumerator = ImportMethod(module, jewelsType, "GetEnumerator", 0, true);
        var enumeratorType = ListEnumeratorType((GenericInstanceType)getEnumerator.ReturnType, jewelType);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var dispose = ImportMethod(module, enumeratorType, "Dispose", 0, true);

        var resultConstructor = ImportMethod(module, arrayListType, ".ctor", 0, true);
        var arrayListAdd = ImportMethod(module, arrayListType, "Add", 1, true, "System.Object");
        var arrayListCount = ImportMethod(module, arrayListType, "get_Count", 0, true);
        var addRange = ImportMethod(module, arrayListType, "AddRange", 1, true, "System.Collections.ICollection");
        var dividerConstructor = ImportMethod(module, listItemType, ".ctor", 1, true, "System.String");
        var itemConstructor = ImportMethod(module, listItemType, ".ctor", 6, true,
            "System.String", "System.String", "Scenario", "System.String", "System.String", "System.Boolean");
        var listConstructor = ImportMethod(module, listType, ".ctor", 2, true, "System.String", "System.Collections.ArrayList");
        var scenarioType = FindType(module, "ScenarioJewelForSale");
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 1, true, jewelType.FullName);
        var getJewelType = ImportMethod(module, jewelType, "get_Type", 0, true);
        var typeField = Field(module, getJewelType.ReturnType, "<Type>k__BackingField");
        var metalStringField = Field(module, jewelType, "_MetalString");
        var shortDescription = ImportMethod(module, jewelType, "ShortDescription", 0, true);
        var caratString = ImportMethod(module, jewelType, "CaratString", 0, true);
        var dealerPrice = ImportMethod(module, jewelType, "DealerPrice", 0, true);
        var gameReferenceType = FindType(module, "GameReference");
        var capitalizeWords = ImportMethod(module, gameReferenceType, "CapitalizeAllMajorWordsInString", 1, false, "System.String");
        var currencyString = ImportMethod(module, gameReferenceType, "CurrencyStringForAmount", 1, false, "System.Double");
        var emojiForJewel = ImportMethod(module, FindType(module, "EmojiHub"), "EmojiForJewel", 1, false, jewelType.FullName);

        var body = FreshBody(method);
        var jewels = new VariableDefinition(jewelsType);
        var result = new VariableDefinition(arrayListType);
        var jewelrySection = new VariableDefinition(arrayListType);
        var watchSection = new VariableDefinition(arrayListType);
        var jewelryEnumerator = new VariableDefinition(enumeratorType);
        var watchEnumerator = new VariableDefinition(enumeratorType);
        var jewel = new VariableDefinition(jewelType);
        var scenario = new VariableDefinition(scenarioType);
        var title = new VariableDefinition(module.TypeSystem.String);
        var emoji = new VariableDefinition(module.TypeSystem.String);
        var secondLine = new VariableDefinition(module.TypeSystem.String);
        var price = new VariableDefinition(module.TypeSystem.String);
        var listItem = new VariableDefinition(listItemType);
        foreach (var local in new[] { jewels, result, jewelrySection, watchSection, jewelryEnumerator, watchEnumerator, jewel, scenario, title, emoji, secondLine, price, listItem })
            body.Variables.Add(local);

        var il = body.Instructions;
        var skipJewelrySection = Instruction.Create(OpCodes.Nop);
        var skipWatchSection = Instruction.Create(OpCodes.Nop);

        // Native 0x46d4f90-0x46d5074: sort the backing jewelry list by EstimatedValue descending.
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Ldfld, luxuryField));
        il.Add(Instruction.Create(OpCodes.Ldfld, jewelsField));
        EmitCachedDelegate(il, selector);
        il.Add(Instruction.Create(OpCodes.Call, orderByDescending));
        il.Add(Instruction.Create(OpCodes.Call, toList));
        il.Add(Instruction.Create(OpCodes.Stloc, jewels));
        il.Add(Instruction.Create(OpCodes.Newobj, resultConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, result));
        il.Add(Instruction.Create(OpCodes.Newobj, resultConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, jewelrySection));
        EmitJewelLoop(jewelrySection, jewelryEnumerator, false);

        il.Add(Instruction.Create(OpCodes.Ldloc, jewelrySection));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ble, skipJewelrySection));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Jewelry"));
        il.Add(Instruction.Create(OpCodes.Newobj, dividerConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, jewelrySection));
        il.Add(Instruction.Create(OpCodes.Callvirt, addRange));
        il.Add(skipJewelrySection);

        il.Add(Instruction.Create(OpCodes.Newobj, resultConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, watchSection));
        EmitJewelLoop(watchSection, watchEnumerator, true);
        il.Add(Instruction.Create(OpCodes.Ldloc, watchSection));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ble, skipWatchSection));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Watches"));
        il.Add(Instruction.Create(OpCodes.Newobj, dividerConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, watchSection));
        il.Add(Instruction.Create(OpCodes.Callvirt, addRange));
        il.Add(skipWatchSection);
        il.Add(Instruction.Create(OpCodes.Ldstr, "Luxury Jewelers"));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Newobj, listConstructor));
        il.Add(Instruction.Create(OpCodes.Ret));

        void EmitJewelLoop(VariableDefinition section, VariableDefinition enumerator, bool watches)
        {
            var tryStart = Instruction.Create(OpCodes.Nop);
            var loop = Instruction.Create(OpCodes.Nop);
            var loopCheck = Instruction.Create(OpCodes.Nop);
            var included = Instruction.Create(OpCodes.Nop);
            var useCaratString = Instruction.Create(OpCodes.Nop);
            var secondLineReady = Instruction.Create(OpCodes.Nop);
            var disposal = Instruction.Create(OpCodes.Nop);
            var done = Instruction.Create(OpCodes.Nop);

            il.Add(Instruction.Create(OpCodes.Ldloc, jewels));
            il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
            il.Add(Instruction.Create(OpCodes.Stloc, enumerator));
            il.Add(tryStart);
            il.Add(Instruction.Create(OpCodes.Br, loopCheck));
            il.Add(loop);
            il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
            il.Add(Instruction.Create(OpCodes.Call, getCurrent));
            il.Add(Instruction.Create(OpCodes.Stloc, jewel));
            il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
            il.Add(Instruction.Create(OpCodes.Callvirt, getJewelType));
            il.Add(Instruction.Create(OpCodes.Ldfld, typeField));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_6));
            // The watch loop must compare, not branch on the constant: brtrue would leave
            // the jewel type on the evaluation stack and fail IL2CPP's global-variable check.
            il.Add(Instruction.Create(OpCodes.Beq, watches ? included : loopCheck));
            il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
            il.Add(Instruction.Create(OpCodes.Callvirt, getJewelType));
            il.Add(Instruction.Create(OpCodes.Ldfld, typeField));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_7));
            il.Add(Instruction.Create(watches ? OpCodes.Bne_Un : OpCodes.Beq, loopCheck));
            if (!watches) il.Add(Instruction.Create(OpCodes.Br, included));
            il.Add(included);

            // Native 0x46d50e8-0x46d520c and 0x46d52fc-0x46d5434 spill every
            // constructor argument to locals. In particular, the watch-only branch
            // must not carry a partially constructed ListItem argument list.
            il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
            il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, scenario));
            if (!watches)
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
                il.Add(Instruction.Create(OpCodes.Callvirt, shortDescription));
                il.Add(Instruction.Create(OpCodes.Call, capitalizeWords));
                il.Add(Instruction.Create(OpCodes.Stloc, title));
                il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
                il.Add(Instruction.Create(OpCodes.Call, emojiForJewel));
                il.Add(Instruction.Create(OpCodes.Stloc, emoji));
            }
            if (watches)
            {
                il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
                il.Add(Instruction.Create(OpCodes.Callvirt, getJewelType));
                il.Add(Instruction.Create(OpCodes.Ldfld, typeField));
                il.Add(Instruction.Create(OpCodes.Ldc_I4_6));
                il.Add(Instruction.Create(OpCodes.Bne_Un, useCaratString));
                il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
                il.Add(Instruction.Create(OpCodes.Ldfld, metalStringField));
                il.Add(Instruction.Create(OpCodes.Stloc, secondLine));
                il.Add(Instruction.Create(OpCodes.Br, secondLineReady));
                il.Add(useCaratString);
            }
            il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
            il.Add(Instruction.Create(OpCodes.Callvirt, caratString));
            il.Add(Instruction.Create(OpCodes.Stloc, secondLine));
            if (watches)
            {
                il.Add(secondLineReady);
                il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
                il.Add(Instruction.Create(OpCodes.Callvirt, shortDescription));
                il.Add(Instruction.Create(OpCodes.Call, capitalizeWords));
                il.Add(Instruction.Create(OpCodes.Stloc, title));
                il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
                il.Add(Instruction.Create(OpCodes.Call, emojiForJewel));
                il.Add(Instruction.Create(OpCodes.Stloc, emoji));
            }
            il.Add(Instruction.Create(OpCodes.Ldloc, jewel));
            il.Add(Instruction.Create(OpCodes.Callvirt, dealerPrice));
            il.Add(Instruction.Create(OpCodes.Conv_R8));
            il.Add(Instruction.Create(OpCodes.Call, currencyString));
            il.Add(Instruction.Create(OpCodes.Stloc, price));
            il.Add(Instruction.Create(OpCodes.Ldloc, title));
            il.Add(Instruction.Create(OpCodes.Ldloc, emoji));
            il.Add(Instruction.Create(OpCodes.Ldloc, scenario));
            il.Add(Instruction.Create(OpCodes.Ldloc, secondLine));
            il.Add(Instruction.Create(OpCodes.Ldloc, price));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(OpCodes.Newobj, itemConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, listItem));
            il.Add(Instruction.Create(OpCodes.Ldloc, section));
            il.Add(Instruction.Create(OpCodes.Ldloc, listItem));
            il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
            il.Add(Instruction.Create(OpCodes.Pop));

            il.Add(loopCheck);
            il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
            il.Add(Instruction.Create(OpCodes.Call, moveNext));
            il.Add(Instruction.Create(OpCodes.Brtrue, loop));
            il.Add(Instruction.Create(OpCodes.Leave, done));
            il.Add(disposal);
            il.Add(Instruction.Create(OpCodes.Ldloca, enumerator));
            il.Add(Instruction.Create(OpCodes.Call, dispose));
            il.Add(Instruction.Create(OpCodes.Endfinally));
            il.Add(done);
            body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
            {
                TryStart = tryStart,
                TryEnd = disposal,
                HandlerStart = disposal,
                HandlerEnd = done
            });
        }
    }

    private sealed class CachedDelegate
    {
        public CachedDelegate(FieldReference cache, FieldReference singleton, MethodReference method, MethodReference constructor)
        {
            Cache = cache;
            Singleton = singleton;
            Method = method;
            Constructor = constructor;
            Type = (GenericInstanceType)cache.FieldType;
        }

        public FieldReference Cache { get; }
        public FieldReference Singleton { get; }
        public MethodReference Method { get; }
        public MethodReference Constructor { get; }
        public GenericInstanceType Type { get; }
    }

    private static CachedDelegate CreateCachedDelegate(ModuleDefinition module, TypeDefinition closureType, string methodName, string cacheName)
    {
        var cache = Field(module, closureType, cacheName);
        var singleton = Field(module, closureType, "<>9");
        var method = ImportMethod(module, closureType, methodName, 1, true);
        var constructor = ImportMethod(module, cache.FieldType, ".ctor", 2, true);
        return new CachedDelegate(cache, singleton, method, constructor);
    }

    private static void EmitCachedDelegate(ICollection<Instruction> instructions, CachedDelegate cached)
    {
        var ready = Instruction.Create(OpCodes.Nop);
        instructions.Add(Instruction.Create(OpCodes.Ldsfld, cached.Cache));
        instructions.Add(Instruction.Create(OpCodes.Dup));
        instructions.Add(Instruction.Create(OpCodes.Brtrue, ready));
        instructions.Add(Instruction.Create(OpCodes.Pop));
        instructions.Add(Instruction.Create(OpCodes.Ldsfld, cached.Singleton));
        instructions.Add(Instruction.Create(OpCodes.Ldftn, cached.Method));
        instructions.Add(Instruction.Create(OpCodes.Newobj, cached.Constructor));
        instructions.Add(Instruction.Create(OpCodes.Dup));
        instructions.Add(Instruction.Create(OpCodes.Stsfld, cached.Cache));
        instructions.Add(ready);
    }

    private static GenericInstanceMethod LinqMethod(ModuleDefinition module, string name, int parameterCount,
        Func<MethodDefinition, bool>? predicate, params TypeReference[] arguments)
    {
        var type = FrameworkType(module, "System.Linq", "Enumerable");
        var definition = type.Resolve().Methods.Single(candidate =>
            candidate.Name == name && candidate.Parameters.Count == parameterCount &&
            (predicate is null || predicate(candidate)));
        return GenericMethod(module, definition, arguments);
    }

    private static GenericInstanceMethod GenericMethod(ModuleDefinition module, MethodReference definition, params TypeReference[] arguments)
    {
        var method = new GenericInstanceMethod(module.ImportReference(definition.Resolve()));
        foreach (var argument in arguments) method.GenericArguments.Add(argument);
        return method;
    }

    private static GenericInstanceType GenericType(TypeReference template, params TypeReference[] arguments)
    {
        var elementType = template is TypeSpecification specification ? specification.ElementType : template;
        var instance = new GenericInstanceType(elementType);
        foreach (var argument in arguments) instance.GenericArguments.Add(argument);
        return instance;
    }

    private static GenericInstanceType ListEnumeratorType(GenericInstanceType template, TypeReference argument)
    {
        return GenericType(template, argument);
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static TypeReference FrameworkType(ModuleDefinition module, string typeNamespace, string typeName)
    {
        var scope = module.TypeSystem.Object.Scope;
        if (typeNamespace == "System.Linq")
            scope = module.AssemblyReferences.Single(reference => reference.Name == "System.Core");
        return new TypeReference(typeNamespace, typeName, module, scope);
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name,
        int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        var candidates = name is ".ctor" or ".cctor"
            ? declaringType.Resolve().Methods
            : Hierarchy(declaringType.Resolve()).SelectMany(type => type.Methods);
        var definition = candidates.Single(candidate =>
            candidate.Name == name && candidate.Parameters.Count == parameterCount &&
            (!instance.HasValue || candidate.IsStatic != instance.Value) &&
            (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameterTypes)));
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
    }

    private static MethodReference FrameworkMethod(ModuleDefinition module, string typeName, string methodName,
        int parameterCount, bool instance, params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = FrameworkType(module, typeName.Substring(0, separator), typeName.Substring(separator + 1));
        return ImportMethod(module, type, methodName, parameterCount, instance, parameterTypes);
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        var field = Hierarchy(declaringType.Resolve()).SelectMany(type => type.Fields).Single(candidate => candidate.Name == name);
        return module.ImportReference(field);
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
