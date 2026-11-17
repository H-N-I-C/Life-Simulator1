using Mono.Cecil;
using Mono.Cecil.Cil;

public static class MuseumArtifactMethodRepair
{
    public static void Fix(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;
        var listItemType = FindType(module, "ListItem");
        var artifactType = FindType(module, "SimArtifact");
        var astronautType = FindType(module, "SimSpecialJobAstronaut");
        var scenarioType = FindType(module, "ScenarioBlackMarketMuseumDisplayAddAssetItem");
        var gameReferenceType = FindType(module, "GameReference");
        var arrayExtensionType = FindType(module, "ArrayExtension");

        var arrayListConstructor = FrameworkMethod(module, "System.Collections.ArrayList", ".ctor", 0, true);
        var arrayListAdd = FrameworkMethod(module, "System.Collections.ArrayList", "Add", 1, true);
        var arrayListAddRange = FrameworkMethod(module, "System.Collections.ArrayList", "AddRange", 1, true, "System.Collections.ICollection");
        var countNotNull = ImportMethod(module, arrayExtensionType, "CountNotNull", 1, false);
        var getMarketItems = ImportMethod(module, owner, "GetBelongingsMarketItemsToAddDisplay", 4, true,
            "SimMuseumWing", "MARKET_ITEM_TYPE", "SimDisplayItem", "System.Int32");
        var titleConstructor = ImportMethod(module, listItemType, ".ctor", 1, true, "System.String");
        var rowConstructor = ImportMethod(module, listItemType, ".ctor", 6, true,
            "System.String", "System.String", "Scenario", "System.String", "System.String", "System.Boolean");
        var scenarioConstructor = ImportMethod(module, scenarioType, ".ctor", 4, true,
            "SimMuseumWing", "SimArtifact", "SimDisplayItem", "System.Int32");
        var getSpecialJob = ImportMethod(module, astronautType, "GetSpecialJobIfAstronaut", 0, false);
        var getDiscoveries = ImportMethod(module, astronautType, "get_Discoveries", 0, true);
        var getArtifactOrigin = ImportMethod(module, artifactType, "GetArtifactOrigin", 0, true);
        var getArtifactName = ImportMethod(module, artifactType, "get_ArtifactName", 0, true);
        var getEmoji = ImportMethod(module, artifactType, "EmojiString", 0, true);
        var getCondition = ImportMethod(module, artifactType, "get_Condition", 0, true);
        var capitalize = ImportMethod(module, gameReferenceType, "CapitalizeString", 1, false, "System.String");
        var barGraphTitleField = Field(module, listItemType, "BarGraphTitle");
        var barGraphValueField = Field(module, listItemType, "BarGraphValue");

        var discoveryListType = (GenericInstanceType)getDiscoveries.ReturnType;
        var artifactListType = GenericInstance(discoveryListType, module.TypeSystem.Object);
        var listConstructor = ImportMethod(module, artifactListType, ".ctor", 0, true);
        var listAdd = ImportMethod(module, artifactListType, "Add", 1, true);
        var listGetCount = ImportMethod(module, artifactListType, "get_Count", 0, true);
        var getDiscoveryEnumerator = ImportMethod(module, discoveryListType, "GetEnumerator", 0, true);
        var getArtifactEnumerator = ImportMethod(module, artifactListType, "GetEnumerator", 0, true);
        var discoveryEnumeratorType = GenericInstance((GenericInstanceType)getDiscoveryEnumerator.ReturnType, discoveryListType.GenericArguments[0]);
        var artifactEnumeratorType = GenericInstance((GenericInstanceType)getArtifactEnumerator.ReturnType, module.TypeSystem.Object);
        var discoveryGetCurrent = ImportMethod(module, discoveryEnumeratorType, "get_Current", 0, true);
        var artifactGetCurrent = ImportMethod(module, artifactEnumeratorType, "get_Current", 0, true);
        var discoveryMoveNext = ImportMethod(module, discoveryEnumeratorType, "MoveNext", 0, true);
        var artifactMoveNext = ImportMethod(module, artifactEnumeratorType, "MoveNext", 0, true);
        var discoveryDispose = ImportMethod(module, discoveryEnumeratorType, "Dispose", 0, true);
        var artifactDispose = ImportMethod(module, artifactEnumeratorType, "Dispose", 0, true);

        var body = FreshBody(method);
        var result = new VariableDefinition(method.ReturnType);
        var marketItems = new VariableDefinition(method.ReturnType);
        var lunarArtifacts = new VariableDefinition(artifactListType);
        var martianArtifacts = new VariableDefinition(artifactListType);
        var astronaut = new VariableDefinition(astronautType);
        var discoveries = new VariableDefinition(discoveryListType);
        var discoveryEnumerator = new VariableDefinition(discoveryEnumeratorType);
        var artifactEnumerator = new VariableDefinition(artifactEnumeratorType);
        var discovery = new VariableDefinition(discoveryListType.GenericArguments[0]);
        var artifact = new VariableDefinition(artifactType);
        var scenario = new VariableDefinition(scenarioType);
        var row = new VariableDefinition(listItemType);
        foreach (var local in new[]
                 {
                     result, marketItems, lunarArtifacts, martianArtifacts, astronaut, discoveries,
                     discoveryEnumerator, artifactEnumerator, discovery, artifact, scenario, row
                 })
            body.Variables.Add(local);

        var il = body.Instructions;
        var wing = method.Parameters.Single(parameter => parameter.Name == "wing");
        var displayItem = method.Parameters.Single(parameter => parameter.Name == "simDisplayItem");
        var slotIndex = method.Parameters.Single(parameter => parameter.Name == "slotIndex");

        il.Add(Instruction.Create(OpCodes.Newobj, arrayListConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, result));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldarg, wing));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_7));
        il.Add(Instruction.Create(OpCodes.Ldarg, displayItem));
        il.Add(Instruction.Create(OpCodes.Ldarg, slotIndex));
        il.Add(Instruction.Create(OpCodes.Call, getMarketItems));
        il.Add(Instruction.Create(OpCodes.Stloc, marketItems));
        il.Add(Instruction.Create(OpCodes.Ldloc, marketItems));
        il.Add(Instruction.Create(OpCodes.Call, countNotNull));
        var noMarketItems = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Blt, noMarketItems));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Historical Artifacts"));
        il.Add(Instruction.Create(OpCodes.Newobj, titleConstructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ldloc, marketItems));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAddRange));
        il.Add(noMarketItems);

        il.Add(Instruction.Create(OpCodes.Newobj, listConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, lunarArtifacts));
        il.Add(Instruction.Create(OpCodes.Newobj, listConstructor));
        il.Add(Instruction.Create(OpCodes.Stloc, martianArtifacts));
        il.Add(Instruction.Create(OpCodes.Call, getSpecialJob));
        il.Add(Instruction.Create(OpCodes.Stloc, astronaut));
        var discoveriesDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, astronaut));
        il.Add(Instruction.Create(OpCodes.Brfalse, discoveriesDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, astronaut));
        il.Add(Instruction.Create(OpCodes.Call, getDiscoveries));
        il.Add(Instruction.Create(OpCodes.Stloc, discoveries));
        il.Add(Instruction.Create(OpCodes.Ldloc, discoveries));
        il.Add(Instruction.Create(OpCodes.Brfalse, discoveriesDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, discoveries));
        il.Add(Instruction.Create(OpCodes.Callvirt, getDiscoveryEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, discoveryEnumerator));

        var discoveryTry = Instruction.Create(OpCodes.Nop);
        var discoveryLoop = Instruction.Create(OpCodes.Nop);
        var discoveryLoopCheck = Instruction.Create(OpCodes.Nop);
        var nextDiscovery = Instruction.Create(OpCodes.Nop);
        var notMartian = Instruction.Create(OpCodes.Nop);
        var discoveryDisposal = Instruction.Create(OpCodes.Nop);
        var discoveryDisposalEnd = Instruction.Create(OpCodes.Nop);
        il.Add(discoveryTry);
        il.Add(Instruction.Create(OpCodes.Br, discoveryLoopCheck));
        il.Add(discoveryLoop);
        il.Add(Instruction.Create(OpCodes.Ldloca, discoveryEnumerator));
        il.Add(Instruction.Create(OpCodes.Call, discoveryGetCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, discovery));
        il.Add(Instruction.Create(OpCodes.Ldloc, discovery));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextDiscovery));
        il.Add(Instruction.Create(OpCodes.Ldloc, discovery));
        il.Add(Instruction.Create(OpCodes.Isinst, artifactType));
        il.Add(Instruction.Create(OpCodes.Stloc, artifact));
        il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
        il.Add(Instruction.Create(OpCodes.Brfalse, nextDiscovery));
        il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
        il.Add(Instruction.Create(OpCodes.Call, getArtifactOrigin));
        il.Add(Instruction.Create(OpCodes.Brtrue, notMartian));
        il.Add(Instruction.Create(OpCodes.Ldloc, lunarArtifacts));
        il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
        il.Add(Instruction.Create(OpCodes.Callvirt, listAdd));
        il.Add(Instruction.Create(OpCodes.Br, nextDiscovery));
        il.Add(notMartian);
        il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
        il.Add(Instruction.Create(OpCodes.Call, getArtifactOrigin));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Bne_Un, nextDiscovery));
        il.Add(Instruction.Create(OpCodes.Ldloc, martianArtifacts));
        il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
        il.Add(Instruction.Create(OpCodes.Callvirt, listAdd));
        il.Add(nextDiscovery);
        il.Add(discoveryLoopCheck);
        il.Add(Instruction.Create(OpCodes.Ldloca, discoveryEnumerator));
        il.Add(Instruction.Create(OpCodes.Call, discoveryMoveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, discoveryLoop));
        il.Add(Instruction.Create(OpCodes.Leave, discoveriesDone));
        il.Add(discoveryDisposal);
        il.Add(Instruction.Create(OpCodes.Ldloca, discoveryEnumerator));
        il.Add(Instruction.Create(OpCodes.Call, discoveryDispose));
        il.Add(Instruction.Create(OpCodes.Endfinally));
        il.Add(discoveryDisposalEnd);
        body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
        {
            TryStart = discoveryTry,
            TryEnd = discoveryDisposal,
            HandlerStart = discoveryDisposal,
            HandlerEnd = discoveryDisposalEnd
        });
        il.Add(discoveriesDone);

        void EmitSection(VariableDefinition artifacts, string title)
        {
            var sectionDone = Instruction.Create(OpCodes.Nop);
            il.Add(Instruction.Create(OpCodes.Ldloc, artifacts));
            il.Add(Instruction.Create(OpCodes.Callvirt, listGetCount));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(OpCodes.Blt, sectionDone));
            il.Add(Instruction.Create(OpCodes.Ldloc, result));
            il.Add(Instruction.Create(OpCodes.Ldstr, title));
            il.Add(Instruction.Create(OpCodes.Newobj, titleConstructor));
            il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
            il.Add(Instruction.Create(OpCodes.Pop));
            il.Add(Instruction.Create(OpCodes.Ldloc, artifacts));
            il.Add(Instruction.Create(OpCodes.Callvirt, getArtifactEnumerator));
            il.Add(Instruction.Create(OpCodes.Stloc, artifactEnumerator));

            var tryStart = Instruction.Create(OpCodes.Nop);
            var loop = Instruction.Create(OpCodes.Nop);
            var loopCheck = Instruction.Create(OpCodes.Nop);
            var disposal = Instruction.Create(OpCodes.Nop);
            var disposalEnd = Instruction.Create(OpCodes.Nop);
            il.Add(tryStart);
            il.Add(Instruction.Create(OpCodes.Br, loopCheck));
            il.Add(loop);
            il.Add(Instruction.Create(OpCodes.Ldloca, artifactEnumerator));
            il.Add(Instruction.Create(OpCodes.Call, artifactGetCurrent));
            il.Add(Instruction.Create(OpCodes.Castclass, artifactType));
            il.Add(Instruction.Create(OpCodes.Stloc, artifact));
            il.Add(Instruction.Create(OpCodes.Ldarg, wing));
            il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
            il.Add(Instruction.Create(OpCodes.Ldarg, displayItem));
            il.Add(Instruction.Create(OpCodes.Ldarg, slotIndex));
            il.Add(Instruction.Create(OpCodes.Newobj, scenarioConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, scenario));
            il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
            il.Add(Instruction.Create(OpCodes.Call, getArtifactName));
            il.Add(Instruction.Create(OpCodes.Call, capitalize));
            il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
            il.Add(Instruction.Create(OpCodes.Callvirt, getEmoji));
            il.Add(Instruction.Create(OpCodes.Ldloc, scenario));
            il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
            il.Add(Instruction.Create(OpCodes.Ldstr, string.Empty));
            il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(OpCodes.Newobj, rowConstructor));
            il.Add(Instruction.Create(OpCodes.Stloc, row));
            il.Add(Instruction.Create(OpCodes.Ldloc, row));
            il.Add(Instruction.Create(OpCodes.Ldstr, "Condition"));
            il.Add(Instruction.Create(OpCodes.Stfld, barGraphTitleField));
            il.Add(Instruction.Create(OpCodes.Ldloc, row));
            il.Add(Instruction.Create(OpCodes.Ldloc, artifact));
            il.Add(Instruction.Create(OpCodes.Call, getCondition));
            il.Add(Instruction.Create(OpCodes.Stfld, barGraphValueField));
            il.Add(Instruction.Create(OpCodes.Ldloc, result));
            il.Add(Instruction.Create(OpCodes.Ldloc, row));
            il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
            il.Add(Instruction.Create(OpCodes.Pop));
            il.Add(loopCheck);
            il.Add(Instruction.Create(OpCodes.Ldloca, artifactEnumerator));
            il.Add(Instruction.Create(OpCodes.Call, artifactMoveNext));
            il.Add(Instruction.Create(OpCodes.Brtrue, loop));
            il.Add(Instruction.Create(OpCodes.Leave, sectionDone));
            il.Add(disposal);
            il.Add(Instruction.Create(OpCodes.Ldloca, artifactEnumerator));
            il.Add(Instruction.Create(OpCodes.Call, artifactDispose));
            il.Add(Instruction.Create(OpCodes.Endfinally));
            il.Add(disposalEnd);
            body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Finally)
            {
                TryStart = tryStart,
                TryEnd = disposal,
                HandlerStart = disposal,
                HandlerEnd = disposalEnd
            });
            il.Add(sectionDone);
        }

        EmitSection(lunarArtifacts, "Lunar Artifacts");
        EmitSection(martianArtifacts, "Martian Artifacts");
        il.Add(Instruction.Create(OpCodes.Ldloc, result));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    private static GenericInstanceType GenericInstance(GenericInstanceType template, TypeReference argument)
    {
        var instance = new GenericInstanceType(template.ElementType);
        instance.GenericArguments.Add(argument);
        return instance;
    }

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static MethodReference ImportMethod(ModuleDefinition module, TypeReference declaringType, string name,
        int parameterCount, bool? instance = null, params string[] parameterTypes)
    {
        var definition = FindMethod(declaringType, name, parameterCount, instance, parameterTypes)
            ?? throw new InvalidOperationException($"Method not found: {declaringType.FullName}::{name}/{parameterCount}");
        var reference = module.ImportReference(definition);
        if (declaringType is GenericInstanceType generic) reference.DeclaringType = generic;
        return reference;
    }

    private static MethodDefinition? FindMethod(TypeReference declaringType, string name, int parameterCount,
        bool? instance = null, params string[] parameterTypes)
    {
        foreach (var type in Hierarchy(declaringType.Resolve()))
        {
            var match = type.Methods.SingleOrDefault(candidate =>
                candidate.Name == name && candidate.Parameters.Count == parameterCount &&
                (!instance.HasValue || candidate.IsStatic != instance.Value) &&
                (parameterTypes.Length == 0 || candidate.Parameters.Select(parameter => parameter.ParameterType.FullName)
                    .SequenceEqual(parameterTypes)));
            if (match is not null) return match;
        }
        return null;
    }

    private static MethodReference FrameworkMethod(ModuleDefinition module, string typeName, string methodName,
        int parameterCount, bool instance, params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = new TypeReference(typeName.Substring(0, separator), typeName.Substring(separator + 1), module,
            module.TypeSystem.Object.Scope);
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
        foreach (var descendant in Flatten(nested))
            yield return descendant;
    }
}
