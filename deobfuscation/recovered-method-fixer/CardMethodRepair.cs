using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class CardMethodRepair
{
    internal static void FixCardFront(MethodDefinition method)
    {
        var module = method.Module;
        MethodReference OriginalCall(string name) => module.ImportReference(method.Body.Instructions
            .Select(instruction => instruction.Operand)
            .OfType<MethodReference>()
            .First(candidate => candidate.Name == name));

        var tryGetNormal = OriginalCall("TryGetValueAs");
        var imageForString = OriginalCall("ImageForString");
        var objectImplicit = OriginalCall("op_Implicit");
        var setEnabled = OriginalCall("set_enabled");
        var setSprite = OriginalCall("set_sprite");
        var getGameObject = OriginalCall("get_gameObject");
        var setActive = OriginalCall("SetActive");
        var getRect = OriginalCall("get_rect");
        var setAnchoredPosition = OriginalCall("set_anchoredPosition");
        var concat = OriginalCall("Concat");
        var intToString = module.ImportReference(module.TypeSystem.Int32.Resolve().Methods.Single(candidate => candidate.Name == "ToString" && candidate.Parameters.Count == 0));

        var eitherType = (GenericInstanceType)method.Parameters[0].ParameterType;
        var normalType = eitherType.GenericArguments[0];
        var normalDefinition = normalType.Resolve();
        var suitField = module.ImportReference(normalDefinition.Fields.Single(field => field.Name == "Suit"));
        var typeField = module.ImportReference(normalDefinition.Fields.Single(field => field.Name == "Type"));
        var suitImage = module.ImportReference(method.DeclaringType.Fields.Single(field => field.Name == "_suit"));
        var typeImage = module.ImportReference(method.DeclaringType.Fields.Single(field => field.Name == "_type"));

        var rectTransformType = getRect.DeclaringType;
        var rectType = getRect.ReturnType;
        var vector2Type = setAnchoredPosition.Parameters[0].ParameterType;
        var vector2Constructor = module.ImportReference(vector2Type.Resolve().Methods.Single(candidate => candidate.Name == ".ctor" && candidate.Parameters.Count == 2));
        var rectWidth = module.ImportReference(rectType.Resolve().Methods.Single(candidate => candidate.Name == "get_width"));
        var rectHeight = module.ImportReference(rectType.Resolve().Methods.Single(candidate => candidate.Name == "get_height"));

        var componentDefinition = suitImage.FieldType.Resolve();
        while (componentDefinition.FullName != "UnityEngine.Component")
            componentDefinition = componentDefinition.BaseType.Resolve();
        var getComponentDefinition = componentDefinition.Methods.Single(candidate => candidate.Name == "GetComponent" && candidate.HasGenericParameters && candidate.Parameters.Count == 0);
        var getRectTransform = new GenericInstanceMethod(module.ImportReference(getComponentDefinition));
        getRectTransform.GenericArguments.Add(rectTransformType);

        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        var normal = new VariableDefinition(normalType);
        var suitNumber = new VariableDefinition(module.TypeSystem.Int32);
        var typeNumber = new VariableDefinition(module.TypeSystem.Int32);
        var prefix = new VariableDefinition(module.TypeSystem.String);
        var root = new VariableDefinition(rectTransformType);
        var suitRect = new VariableDefinition(rectTransformType);
        var typeRect = new VariableDefinition(rectTransformType);
        foreach (var local in new[] { normal, suitNumber, typeNumber, prefix, root, suitRect, typeRect }) body.Variables.Add(local);
        var il = body.Instructions;

        var joker = Instruction.Create(OpCodes.Nop);
        var done = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_1));
        il.Add(Instruction.Create(OpCodes.Ldloca, normal));
        il.Add(Instruction.Create(OpCodes.Call, tryGetNormal));
        il.Add(Instruction.Create(OpCodes.Brfalse, joker));

        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, getGameObject));
        il.Add(Instruction.Create(OpCodes.Callvirt, getRectTransform));
        il.Add(Instruction.Create(OpCodes.Stloc, root));

        var afterSuit = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, suitImage));
        il.Add(Instruction.Create(OpCodes.Call, objectImplicit));
        il.Add(Instruction.Create(OpCodes.Brfalse, afterSuit));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, suitImage));
        il.Add(Instruction.Create(OpCodes.Call, getGameObject));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, setActive));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, suitNumber));
        var assignSuit = Instruction.Create(OpCodes.Nop);
        var suitAssigned = Instruction.Create(OpCodes.Nop);
        foreach (var value in new[] { 1, 2, 3 })
        {
            il.Add(Instruction.Create(OpCodes.Ldloca, normal));
            il.Add(Instruction.Create(OpCodes.Ldfld, suitField));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, value));
            il.Add(Instruction.Create(OpCodes.Beq, assignSuit));
        }
        il.Add(Instruction.Create(OpCodes.Br, suitAssigned));
        il.Add(assignSuit);
        il.Add(Instruction.Create(OpCodes.Ldloca, normal));
        il.Add(Instruction.Create(OpCodes.Ldfld, suitField));
        il.Add(Instruction.Create(OpCodes.Stloc, suitNumber));
        il.Add(suitAssigned);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, suitImage));
        il.Add(Instruction.Create(OpCodes.Ldstr, "a16_cardSuit"));
        il.Add(Instruction.Create(OpCodes.Ldloca, suitNumber));
        il.Add(Instruction.Create(OpCodes.Call, intToString));
        il.Add(Instruction.Create(OpCodes.Call, concat));
        il.Add(Instruction.Create(OpCodes.Call, imageForString));
        il.Add(Instruction.Create(OpCodes.Callvirt, setSprite));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, suitImage));
        il.Add(Instruction.Create(OpCodes.Callvirt, getRectTransform));
        il.Add(Instruction.Create(OpCodes.Stloc, suitRect));
        il.Add(Instruction.Create(OpCodes.Ldloc, suitRect));
        il.Add(Instruction.Create(OpCodes.Call, objectImplicit));
        il.Add(Instruction.Create(OpCodes.Brfalse, afterSuit));
        il.Add(Instruction.Create(OpCodes.Ldloc, suitRect));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 0f));
        il.Add(Instruction.Create(OpCodes.Ldloc, root));
        il.Add(Instruction.Create(OpCodes.Callvirt, getRect));
        il.Add(Instruction.Create(OpCodes.Call, rectHeight));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, -0.107f));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Newobj, vector2Constructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, setAnchoredPosition));
        il.Add(afterSuit);

        var normalDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, typeImage));
        il.Add(Instruction.Create(OpCodes.Call, objectImplicit));
        il.Add(Instruction.Create(OpCodes.Brfalse, normalDone));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, typeImage));
        il.Add(Instruction.Create(OpCodes.Call, getGameObject));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, setActive));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, typeImage));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, setEnabled));
        var redPrefix = Instruction.Create(OpCodes.Nop);
        var prefixReady = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloca, normal));
        il.Add(Instruction.Create(OpCodes.Ldfld, suitField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Clt_Un));
        il.Add(Instruction.Create(OpCodes.Brfalse, redPrefix));
        il.Add(Instruction.Create(OpCodes.Ldstr, "a16_cardTypeBlack"));
        il.Add(Instruction.Create(OpCodes.Stloc, prefix));
        il.Add(Instruction.Create(OpCodes.Br, prefixReady));
        il.Add(redPrefix);
        il.Add(Instruction.Create(OpCodes.Ldstr, "a16_cardTypeRed"));
        il.Add(Instruction.Create(OpCodes.Stloc, prefix));
        il.Add(prefixReady);
        il.Add(Instruction.Create(OpCodes.Ldloca, normal));
        il.Add(Instruction.Create(OpCodes.Ldfld, typeField));
        il.Add(Instruction.Create(OpCodes.Stloc, typeNumber));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, typeImage));
        il.Add(Instruction.Create(OpCodes.Ldloc, prefix));
        il.Add(Instruction.Create(OpCodes.Ldloca, typeNumber));
        il.Add(Instruction.Create(OpCodes.Call, intToString));
        il.Add(Instruction.Create(OpCodes.Call, concat));
        il.Add(Instruction.Create(OpCodes.Call, imageForString));
        il.Add(Instruction.Create(OpCodes.Callvirt, setSprite));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, typeImage));
        il.Add(Instruction.Create(OpCodes.Callvirt, getRectTransform));
        il.Add(Instruction.Create(OpCodes.Stloc, typeRect));
        il.Add(Instruction.Create(OpCodes.Ldloc, typeRect));
        il.Add(Instruction.Create(OpCodes.Call, objectImplicit));
        il.Add(Instruction.Create(OpCodes.Brfalse, normalDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, typeRect));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 48.5f));
        il.Add(Instruction.Create(OpCodes.Ldloc, root));
        il.Add(Instruction.Create(OpCodes.Callvirt, getRect));
        il.Add(Instruction.Create(OpCodes.Call, rectWidth));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 0.5f));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Ldloc, root));
        il.Add(Instruction.Create(OpCodes.Callvirt, getRect));
        il.Add(Instruction.Create(OpCodes.Call, rectHeight));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 0.5f));
        il.Add(Instruction.Create(OpCodes.Mul));
        il.Add(Instruction.Create(OpCodes.Ldc_R4, 53f));
        il.Add(Instruction.Create(OpCodes.Sub));
        il.Add(Instruction.Create(OpCodes.Newobj, vector2Constructor));
        il.Add(Instruction.Create(OpCodes.Callvirt, setAnchoredPosition));
        il.Add(normalDone);
        il.Add(Instruction.Create(OpCodes.Ret));

        il.Add(joker);
        var jokerType = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, suitImage));
        il.Add(Instruction.Create(OpCodes.Call, objectImplicit));
        il.Add(Instruction.Create(OpCodes.Brfalse, jokerType));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, suitImage));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Callvirt, setEnabled));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, suitImage));
        il.Add(Instruction.Create(OpCodes.Ldstr, "a16_cardJoker"));
        il.Add(Instruction.Create(OpCodes.Call, imageForString));
        il.Add(Instruction.Create(OpCodes.Callvirt, setSprite));
        il.Add(jokerType);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, typeImage));
        il.Add(Instruction.Create(OpCodes.Call, objectImplicit));
        il.Add(Instruction.Create(OpCodes.Brfalse, done));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, typeImage));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, setEnabled));
        il.Add(done);
        il.Add(Instruction.Create(OpCodes.Ret));
    }
}
