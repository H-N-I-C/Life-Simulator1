using Mono.Cecil;
using Mono.Cecil.Cil;

public static class EligibilityHandleMethodRepairs
{
    // ==================================================================
    // Eligibility-handle family (#55): ScenarioXXX::ScenarioIsEligibleForRandomPush
    //
    // Native evidence (BitLife 3.26.1 UnityFramework; ipsw disassembly with
    // --cache /tmp/t2m.a2s; call targets resolved by scanning the metadata
    // method-pointer table at Mach-O file offset 0x959C438, low 36 bits = VA):
    //
    //   ScenarioPetFind::ScenarioIsEligibleForRandomPush
    //     metadata method idx 26010 -> native VA 0x38E34A4
    //     (inside __TEXT.il2cpp 0x370A280-0x76AF590)
    //   ScenarioSexualityDeclare::ScenarioIsEligibleForRandomPush
    //     metadata method idx 27912 -> native VA 0x3A7D0E0
    //
    // Shared static chain (both methods): usage slot 0x96CE080 holds token
    // 0x2000C3D3 (kind=TypeInfo, index 50131) resolved to the GameManager
    // class; klass+0xB8 is the static-fields pointer and static field 0x0 is
    // `public static GameManager Inst` (DiffableCs stub offset 0x0). The
    // named-metadata equivalent of the native chain [[slot]+0xB8 deref]+0x20
    // is the GameManager.Inst / GameManager.Life field access; the singleton's
    // instance fields at +0x20/+0x28/+0x30 hold the Life/hero/SimPast
    // references, reconstructed here through the named Life.<Hero>/<Past>/
    // <Finances> backing fields (0x88/0x98/0xE8 — 0xE8 is the exact native
    // offset used for the SimFinances::EffectiveBankBalance receiver).
    // ==================================================================

    public static void FixPetFindEligibility(MethodDefinition method)
    {
        RequireMethod(method, "ScenarioPetFind", "ScenarioIsEligibleForRandomPush", "System.Boolean");

        var module = method.Module;
        var owner = method.DeclaringType;
        var gameManagerType = FindType(module, "GameManager");
        var lifeType = FindType(module, "Life");
        var simHeroType = FindType(module, "SimHero");
        var simPersonType = FindType(module, "SimPerson");
        var simPetType = FindType(module, "SimPet");
        var simFinancesType = FindType(module, "SimFinances");

        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var heroField = Field(module, lifeType, "<Hero>k__BackingField");
        var financesField = Field(module, lifeType, "<Finances>k__BackingField");
        var petAliveField = Field(module, simPetType, "<Alive>k__BackingField");

        var getPetArray = ImportMethod(module, lifeType, "get_PetArray", 0, true);
        var getAge = ImportMethod(module, simPersonType, "get_Age", 0, true);
        var presentedCount = ImportMethod(module, lifeType, "PresentedCountForScenario", 1, true);
        var maxPets = ImportMethod(module, simHeroType, "MaxNumberOfPetsPermissable", 0, true);
        var bankBalance = ImportMethod(module, simFinancesType, "EffectiveBankBalance", 0, true);
        var inPrison = ImportMethod(module, simHeroType, "CurrentlyInPrison", 0, true);

        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var enumeratorType = FrameworkType(module, "System.Collections", "IEnumerator");
        var getEnumerator = ImportMethod(module, arrayListType, "GetEnumerator", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);

        var typeType = FrameworkType(module, "System", "Type").Resolve();
        var memberInfoType = FrameworkType(module, "System.Reflection", "MemberInfo").Resolve();
        var getTypeFromHandle = module.ImportReference(typeType.Methods.Single(candidate =>
            candidate.Name == "GetTypeFromHandle" && candidate.Parameters.Count == 1 &&
            candidate.Parameters[0].ParameterType.Name == "RuntimeTypeHandle"));
        var getName = module.ImportReference(memberInfoType.Methods.Single(candidate => candidate.Name == "get_Name"));

        var body = FreshBody(method);
        var life = new VariableDefinition(lifeType);
        var hero = new VariableDefinition(simHeroType);
        var finances = new VariableDefinition(simFinancesType);
        var petArray = new VariableDefinition(arrayListType);
        var enumerator = new VariableDefinition(enumeratorType);
        var item = new VariableDefinition(module.TypeSystem.Object);
        var pet = new VariableDefinition(simPetType);
        var alivePets = new VariableDefinition(module.TypeSystem.Int32);
        foreach (var local in new[] { life, hero, finances, petArray, enumerator, item, pet, alivePets }) body.Variables.Add(local);
        var il = body.Instructions;

        // life = GameManager.Inst.Life
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Stloc, life));

        // hero = life.Hero; finances = life.Finances
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, heroField));
        il.Add(Instruction.Create(OpCodes.Stloc, hero));
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, financesField));
        il.Add(Instruction.Create(OpCodes.Stloc, finances));

        // petArray = life.PetArray
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Callvirt, getPetArray));
        il.Add(Instruction.Create(OpCodes.Stloc, petArray));

        // alivePets = 0; enumerator = petArray.GetEnumerator()
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, alivePets));
        il.Add(Instruction.Create(OpCodes.Ldloc, petArray));
        il.Add(Instruction.Create(OpCodes.Callvirt, getEnumerator));
        il.Add(Instruction.Create(OpCodes.Stloc, enumerator));

        // foreach (object item in petArray):
        //   pet = item as SimPet   (native isinst against class slot 0x96D9430;
        //     SimPet is the only type-consistent reading: PetArray holds SimPet,
        //     the counted field +0x28 is SimPet.<Alive>, and the scenario's own
        //     _petCandidate field is a SimPet)
        //   if (pet != null && pet.Alive) alivePets++
        var loopCheck = Instruction.Create(OpCodes.Ldloc, enumerator);
        var loopBody = Instruction.Create(OpCodes.Ldloc, enumerator);
        var loopDone = Instruction.Create(OpCodes.Nop);
        var notPet = Instruction.Create(OpCodes.Br, loopCheck);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loopBody);
        il.Add(Instruction.Create(OpCodes.Callvirt, moveNext));
        il.Add(Instruction.Create(OpCodes.Brfalse, loopDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, item));
        il.Add(Instruction.Create(OpCodes.Ldloc, item));
        il.Add(Instruction.Create(OpCodes.Isinst, simPetType));
        il.Add(Instruction.Create(OpCodes.Stloc, pet));
        il.Add(Instruction.Create(OpCodes.Ldloc, pet));
        il.Add(Instruction.Create(OpCodes.Brfalse, notPet));
        il.Add(Instruction.Create(OpCodes.Ldloc, pet));
        il.Add(Instruction.Create(OpCodes.Ldfld, petAliveField));
        il.Add(Instruction.Create(OpCodes.Brfalse, notPet));
        il.Add(Instruction.Create(OpCodes.Ldloc, alivePets));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Add));
        il.Add(Instruction.Create(OpCodes.Stloc, alivePets));
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(notPet);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Callvirt, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loopBody));
        il.Add(loopDone);

        // if (hero.Age <= 17) return false   (native: cmp w9, #0x11; b.le fail)
        var ageOk = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Callvirt, getAge));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_S, (sbyte)17));
        il.Add(Instruction.Create(OpCodes.Bgt, ageOk));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ret));
        il.Add(ageOk);

        // if (Life.PresentedCountForScenario(typeof(ScenarioPetFind).Name) > 2) return false
        // (native: bl Life::PresentedCountForScenario @0x47F42A0; cmp w0, #2; b.gt fail)
        // INFERENCE: the argument is produced natively as typeof(T).Name
        // (Il2CppType usage slot 0x96BB008 -> type object -> virtual get_Name
        // via vtable slot 0x208); T is not statically recoverable (the type
        // table lives in the binary's metadata registration), and the
        // scenario's own type is the only self-consistent reading.
        var countOk = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldtoken, owner));
        il.Add(Instruction.Create(OpCodes.Call, getTypeFromHandle));
        il.Add(Instruction.Create(OpCodes.Callvirt, getName));
        il.Add(Instruction.Create(OpCodes.Callvirt, presentedCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Ble, countOk));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ret));
        il.Add(countOk);

        // if (alivePets < hero.MaxNumberOfPetsPermissable()) -> continue
        // (native: bl SimHero::MaxNumberOfPetsPermissable @0x3F0CB2C;
        //  cmp w22, w0; b.ge fail)
        var petsOk = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, alivePets));
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Callvirt, maxPets));
        il.Add(Instruction.Create(OpCodes.Blt, petsOk));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ret));
        il.Add(petsOk);

        // if (life.Finances.EffectiveBankBalance() <= 0.0) return false
        // (native: [Life+0xE8] -> bl SimFinances::EffectiveBankBalance
        //  @0x3EF6E54; fcmp d0, 0.0; b.le fail)
        var balanceOk = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, finances));
        il.Add(Instruction.Create(OpCodes.Callvirt, bankBalance));
        il.Add(Instruction.Create(OpCodes.Ldc_R8, 0.0));
        il.Add(Instruction.Create(OpCodes.Bgt, balanceOk));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ret));
        il.Add(balanceOk);

        // return !hero.CurrentlyInPrison()
        // (native: bl SimHero::CurrentlyInPrison @0x3EF9C7C; eor w0, w0, #1)
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Callvirt, inPrison));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ceq));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    public static void FixSexualityEligibility(MethodDefinition method)
    {
        RequireMethod(method, "ScenarioSexualityDeclare", "ScenarioIsEligibleForRandomPush", "System.Boolean");

        var module = method.Module;
        var owner = method.DeclaringType;
        var gameManagerType = FindType(module, "GameManager");
        var lifeType = FindType(module, "Life");
        var simHeroType = FindType(module, "SimHero");
        var simPersonType = FindType(module, "SimPerson");
        var simPastType = FindType(module, "SimPast");

        var instField = Field(module, gameManagerType, "Inst");
        var lifeField = Field(module, gameManagerType, "Life");
        var heroField = Field(module, lifeType, "<Hero>k__BackingField");
        var pastField = Field(module, lifeType, "<Past>k__BackingField");
        var genderField = Field(module, simPersonType, "<Gender>k__BackingField");

        var getExLoverArray = ImportMethod(module, simPastType, "get_ExLoverArray", 0, true);
        var getAge = ImportMethod(module, simPersonType, "get_Age", 0, true);
        var getSexuality = ImportMethod(module, simPersonType, "get_Sexuality", 0, true);
        var getLgbtqGenderIdentity = ImportMethod(module, simPersonType, "get_LgbtqGenderIdentity", 0, true);
        var presentedCount = ImportMethod(module, lifeType, "PresentedCountForScenario", 1, true);
        var yearsSince = ImportMethod(module, lifeType, "YearsSinceScenario", 1, true);

        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var enumeratorType = FrameworkType(module, "System.Collections", "IEnumerator");
        var getEnumerator = ImportMethod(module, arrayListType, "GetEnumerator", 0, true);
        var moveNext = ImportMethod(module, enumeratorType, "MoveNext", 0, true);
        var getCurrent = ImportMethod(module, enumeratorType, "get_Current", 0, true);

        var typeType = FrameworkType(module, "System", "Type").Resolve();
        var memberInfoType = FrameworkType(module, "System.Reflection", "MemberInfo").Resolve();
        var getTypeFromHandle = module.ImportReference(typeType.Methods.Single(candidate =>
            candidate.Name == "GetTypeFromHandle" && candidate.Parameters.Count == 1 &&
            candidate.Parameters[0].ParameterType.Name == "RuntimeTypeHandle"));
        var getName = module.ImportReference(memberInfoType.Methods.Single(candidate => candidate.Name == "get_Name"));
        var objectToString = module.ImportReference(FrameworkType(module, "System", "Object").Resolve()
            .Methods.Single(candidate => candidate.Name == "ToString" && candidate.Parameters.Count == 0));

        var body = FreshBody(method);
        var life = new VariableDefinition(lifeType);
        var hero = new VariableDefinition(simHeroType);
        var past = new VariableDefinition(simPastType);
        var exLovers = new VariableDefinition(arrayListType);
        var enumerator = new VariableDefinition(enumeratorType);
        var item = new VariableDefinition(module.TypeSystem.Object);
        var person = new VariableDefinition(simPersonType);
        var matched = new VariableDefinition(module.TypeSystem.Boolean);
        foreach (var local in new[] { life, hero, past, exLovers, enumerator, item, person, matched }) body.Variables.Add(local);
        var il = body.Instructions;

        // life = GameManager.Inst.Life; hero = life.Hero; past = life.Past
        il.Add(Instruction.Create(OpCodes.Ldsfld, instField));
        il.Add(Instruction.Create(OpCodes.Ldfld, lifeField));
        il.Add(Instruction.Create(OpCodes.Stloc, life));
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, heroField));
        il.Add(Instruction.Create(OpCodes.Stloc, hero));
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldfld, pastField));
        il.Add(Instruction.Create(OpCodes.Stloc, past));

        // exLovers = past.ExLoverArray  (native bl SimPast::get_ExLoverArray @0x3F103B8)
        il.Add(Instruction.Create(OpCodes.Ldloc, past));
        il.Add(Instruction.Create(OpCodes.Callvirt, getExLoverArray));
        il.Add(Instruction.Create(OpCodes.Stloc, exLovers));

        // matched = false
        // NOTE: the native also ORs an initial comparison of a person stored
        // in an unnamed Life field at +0xD8 ([Life+0xD8]+0x1C == hero+0x1C);
        // that field is unnamed in the DiffableCs stubs and is omitted here.
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stloc, matched));

        // foreach (object item in exLovers):
        //   person = item as SimPerson
        //   INFERENCE: the native casts each item to the class at usage slot
        //   0x96D91F0 (TypeInfo index 52877), not nameable from the metadata
        //   available; the compared field (+0x1C) is SimPerson.<Gender>, so
        //   SimPerson is the closest named cast target.
        //   if (person != null && person.Gender == hero.Gender) matched = true
        var loopCheck = Instruction.Create(OpCodes.Ldloc, enumerator);
        var loopBody = Instruction.Create(OpCodes.Ldloc, enumerator);
        var loopDone = Instruction.Create(OpCodes.Nop);
        var notPerson = Instruction.Create(OpCodes.Br, loopCheck);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loopBody);
        il.Add(Instruction.Create(OpCodes.Callvirt, moveNext));
        il.Add(Instruction.Create(OpCodes.Brfalse, loopDone));
        il.Add(Instruction.Create(OpCodes.Ldloc, enumerator));
        il.Add(Instruction.Create(OpCodes.Callvirt, getCurrent));
        il.Add(Instruction.Create(OpCodes.Stloc, item));
        il.Add(Instruction.Create(OpCodes.Ldloc, item));
        il.Add(Instruction.Create(OpCodes.Isinst, simPersonType));
        il.Add(Instruction.Create(OpCodes.Stloc, person));
        il.Add(Instruction.Create(OpCodes.Ldloc, person));
        il.Add(Instruction.Create(OpCodes.Brfalse, notPerson));
        il.Add(Instruction.Create(OpCodes.Ldloc, person));
        il.Add(Instruction.Create(OpCodes.Ldfld, genderField));
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Ldfld, genderField));
        il.Add(Instruction.Create(OpCodes.Bne_Un, notPerson));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Stloc, matched));
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(notPerson);
        il.Add(Instruction.Create(OpCodes.Br, loopCheck));
        il.Add(loopCheck);
        il.Add(Instruction.Create(OpCodes.Callvirt, moveNext));
        il.Add(Instruction.Create(OpCodes.Brtrue, loopBody));
        il.Add(loopDone);

        // if (hero.Age <= 11) return false
        // (native: ldr w10,[hero+0x20]; cmp #0xB; b.le fail)
        var ageOk = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Callvirt, getAge));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_S, (sbyte)11));
        il.Add(Instruction.Create(OpCodes.Bgt, ageOk));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ret));
        il.Add(ageOk);

        // if (hero.Sexuality != 0) return false
        // (native: ldr w10,[hero+0x24]; cbnz fail — <Sexuality> 0x24)
        var sexualityOk = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Callvirt, getSexuality));
        il.Add(Instruction.Create(OpCodes.Brfalse, sexualityOk));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ret));
        il.Add(sexualityOk);

        // NOTE: the native additionally requires an unnamed float field at
        // hero+0xA0 >= 15.0 (ldr s0,[x9,#0xA0]; fcmp 15.0), ORed with the
        // loop's matched flag; that field is unnamed in the DiffableCs stubs
        // (SimPerson att floats end at <Att_discipline> 0x98) and the gate is
        // omitted rather than guessed.

        // if (hero.LgbtqGenderIdentity != 0) return false
        // (native: ldr w9,[hero+0x44]; cbnz fail — <LgbtqGenderIdentity> 0x44)
        var identityOk = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, hero));
        il.Add(Instruction.Create(OpCodes.Callvirt, getLgbtqGenderIdentity));
        il.Add(Instruction.Create(OpCodes.Brfalse, identityOk));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ret));
        il.Add(identityOk);

        // if (Life.PresentedCountForScenario(typeof(ScenarioSexualityDeclare).Name) > 2) return false
        // (native: bl Life::PresentedCountForScenario @0x47F42A0; cmp w0, #2; b.gt fail)
        // INFERENCE: same typeof(T).Name construction as the PetFind method
        // (usage slot 0x96BB200; vtable slot 0x208 = get_Name).
        var countOk = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldtoken, owner));
        il.Add(Instruction.Create(OpCodes.Call, getTypeFromHandle));
        il.Add(Instruction.Create(OpCodes.Callvirt, getName));
        il.Add(Instruction.Create(OpCodes.Callvirt, presentedCount));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Ble, countOk));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ret));
        il.Add(countOk);

        // return Life.YearsSinceScenario(typeof(ScenarioSexualityDeclare).ToString()) > 2
        // (native: second string via a DIFFERENT vtable slot 0x168 — the dummy
        // renders this one as System.Type::ToString() — then
        // bl Life::YearsSinceScenario @0x47F48C4; cmp w0, #2; cset gt; ret)
        il.Add(Instruction.Create(OpCodes.Ldloc, life));
        il.Add(Instruction.Create(OpCodes.Ldtoken, owner));
        il.Add(Instruction.Create(OpCodes.Call, getTypeFromHandle));
        il.Add(Instruction.Create(OpCodes.Callvirt, objectToString));
        il.Add(Instruction.Create(OpCodes.Callvirt, yearsSince));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Cgt));
        il.Add(Instruction.Create(OpCodes.Ret));
    }

    // ------------------------------------------------------------------
    // Helpers (self-contained copies of the shared repair-file helpers)
    // ------------------------------------------------------------------

    private static MethodBody FreshBody(MethodDefinition method)
    {
        var body = new MethodBody(method) { InitLocals = true };
        method.Body = body;
        return body;
    }

    private static TypeDefinition FindType(ModuleDefinition module, string fullName)
    {
        return module.GetType(fullName) ?? module.Types.SelectMany(Flatten).Single(type => type.FullName == fullName);
    }

    private static TypeReference FrameworkType(ModuleDefinition module, string namespaceName, string name)
    {
        var type = new TypeReference(namespaceName, name, module, module.TypeSystem.Object.Scope);
        return module.ImportReference(type);
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
        throw new InvalidOperationException($"Missing method {declaringType.FullName}::{name}({parameterCount} params)");
    }

    private static FieldReference Field(ModuleDefinition module, TypeReference declaringType, string name)
    {
        foreach (var type in Hierarchy(declaringType.Resolve()))
        {
            var match = type.Fields.SingleOrDefault(candidate => candidate.Name == name);
            if (match is not null) return module.ImportReference(match);
        }
        throw new InvalidOperationException($"Missing field {declaringType.FullName}::{name}");
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

    private static void RequireMethod(MethodDefinition method, string declaringType, string name, string returnType, params string[] parameters)
    {
        Require(method.DeclaringType.FullName == declaringType && method.Name == name && method.HasBody &&
                method.ReturnType.FullName == returnType && method.Parameters.Select(parameter => parameter.ParameterType.FullName).SequenceEqual(parameters),
            $"Unexpected method signature: {method.FullName}");
    }

    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}
