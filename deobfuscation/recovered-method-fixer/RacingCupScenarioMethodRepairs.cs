using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class RacingCupScenarioMethodRepairs
{
    // System.Void ScenarioRacingCupSeriesStreetRaceResults::RefreshScenario()
    // Native 0x396afec (949 instructions). Decoded from ARM64 by hand (task #68).
    //
    // Recovered C# (native-faithful):
    //
    //   HasDismissButton = false;                                  // [this+0x28] strb wzr
    //   ExteriorTitle = "Cup Series";                             // lit @0x974d1f8
    //   Title = _activeRace.HasAdditionalTracks()                  // 0x395301c
    //       ? "Race Results"                                       // lit @0x9771d28
    //       : "Cup results";                                      // lit @0x974d210
    //
    //   var driver = _activeRace.raceCupRegistration.simOpponents[0];   // reg (32B struct @0x10 on the
    //       // active race): simRacing@0x0, simRacingCup@0x8, simRaceCar@0x10, simOpponents@0x18.
    //       // [activeRace+0x28] == reg+0x18; List<SimRaceCarDriver>::get_Item(0) (generic thunk 0x633572c).
    //       // Native cbz null-guards: activeRace (via reg-field deref), the list (callvirt), the item
    //       // (Name ldfld) and Name (FullName callvirt) — all map to natural IL dereferences.
    //   var driverName = driver.Name.FullName();                   // SimPerson::<Name>@0x10 (SimPersonName),
    //                                                               // SimPersonName::FullName @0x3fdd064
    //   var rivalWord = driver.IsRival ? "rival" : "fellow";       // ldrb [driver+0x338]; csel
    //
    //   switch (_results.Placement)                                // ldr w8,[this+0x178] (struct field,
    //   {                                                          // RacingCupSeriesResults.Placement@0)
    //       case 0:   // vehicle broke down
    //           body = String.Concat("Your vehicle broke down during the race at ",
    //                                 _activeRace.ActiveTrack.TrackName,
    //                                 ", and you did not earn any points in the cup series.");
    //           break;                                            // Concat @0x6834748
    //       case 1:   // won
    //       default:  // lost (placement >= 2)
    //           string[] parts = { lead, _activeRace.ActiveTrack.TrackName, connect,
    //                             rivalWord, " racer, ", driverName, "." };   // new string[7], stelem x7
    //           body = String.Concat(parts);                      // Concat(string[]) @0x68350e0
    //           break;                                            // lead: won = "You won the race on " (@0x978ee58),
    //       //                                                    //      lost = "You lost the race on " (@0x978d2a8)
    //       //                                                    // connect: won = " against your " (@0x972e168),
    //       //                                                    //          lost = " to your " (@0x9733698)
    //   }
    //
    //   string suffix = null;
    //   if (!_activeRace.HasAdditionalTracks())                    // tbnz w0,#0 skips when true
    //       suffix = String.Concat("\n\nThis was the final race in the ", _cup.Name,
    //                              " series. Head to the grandstand for the results!");
    //   Body = String.Concat(body, suffix);                        // Concat(string,string) @0x6823894 → [this+0x48]
    //
    //   var choices = new ArrayList();                             // 0x1eca748 alloc + ctor @0x693c9ec
    //   choices.Add(new ScenarioChoice(
    //       _activeRace.HasAdditionalTracks() ? "Continue to the next race" : "Attend the trophy ceremony",
    //       false, true));                                         // ctor @0x3e9c520: (text, w2=0, w3=1)
    //   BuildChoiceIndexDictionary(choices);                       // @0x3ea252c
    //   TableDictionary = CreatePointTotalTable();                 // @0x396b918 → [this+0xa0]
    //   TableOrderArray = CreateTableOrder();                      // @0x396b484 → [this+0xa8]
    //
    // Literals decoded from global-metadata.dat usage tokens (kind = token bits 29-31,
    // index = token bits 1-28; slot decode verified against the runtime initializer at 0x1f3b730).
    public static void FixStreetRefreshScenario(MethodDefinition method)
    {
        var module = method.Module;
        var owner = method.DeclaringType;

        var activeRaceField = Field(module, owner, "_activeRace");
        var activeRaceType = activeRaceField.FieldType;
        var cupField = Field(module, owner, "_cup");
        var resultsField = Field(module, owner, "_results");
        var resultsType = resultsField.FieldType;
        var registrationField = Field(module, activeRaceType, "raceCupRegistration");
        var opponentsField = Field(module, registrationField.FieldType, "simOpponents");
        var opponentsType = (GenericInstanceType)opponentsField.FieldType;   // List<SimRaceCarDriver>
        var driverType = opponentsType.GenericArguments[0];

        var hasAdditionalTracks = ImportMethod(module, activeRaceType, "HasAdditionalTracks", 0, true);
        var getActiveTrack = ImportMethod(module, activeRaceType, "get_ActiveTrack", 0, true);
        var getOpponent = ImportMethod(module, opponentsType, "get_Item", 1, true, "System.Int32");
        var nameField = Field(module, driverType, "<Name>k__BackingField");           // SimPerson.Name
        var fullName = ImportMethod(module, nameField.FieldType, "FullName", 0, true);
        var isRivalField = Field(module, driverType, "<IsRival>k__BackingField");
        var placementField = Field(module, resultsType, "Placement");
        var trackNameField = Field(module, getActiveTrack.ReturnType, "<TrackName>k__BackingField");
        var cupNameField = Field(module, cupField.FieldType, "<Name>k__BackingField");

        var concat3 = FrameworkMethod(module, "System.String", "Concat", 3, false,
            "System.String", "System.String", "System.String");
        var concatArray = FrameworkMethod(module, "System.String", "Concat", 1, false, "System.String[]");
        var concat2 = FrameworkMethod(module, "System.String", "Concat", 2, false,
            "System.String", "System.String");

        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var arrayListCtor = ImportMethod(module, arrayListType, ".ctor", 0);
        var arrayListAdd = ImportMethod(module, arrayListType, "Add", 1, true, "System.Object");
        var choiceType = FindType(module, "ScenarioChoice");
        var choiceCtor = ImportMethod(module, choiceType, ".ctor", 3, null,
            "System.String", "System.Boolean", "System.Boolean");
        var buildChoiceIndexDictionary = ImportMethod(module, owner, "BuildChoiceIndexDictionary", 1, true,
            "System.Collections.ArrayList");
        var createPointTotalTable = ImportMethod(module, owner, "CreatePointTotalTable", 0, true);
        var createTableOrder = ImportMethod(module, owner, "CreateTableOrder", 0, true);

        var hasDismissButtonField = Field(module, owner, "HasDismissButton");
        var exteriorTitleField = Field(module, owner, "ExteriorTitle");
        var titleField = Field(module, owner, "Title");
        var bodyField = Field(module, owner, "Body");
        var tableDictionaryField = Field(module, owner, "TableDictionary");
        var tableOrderArrayField = Field(module, owner, "TableOrderArray");

        var body = FreshBody(method);
        var il = body.Instructions;

        var placement = new VariableDefinition(module.TypeSystem.Int32);
        var driver = new VariableDefinition(driverType);
        var driverName = new VariableDefinition(module.TypeSystem.String);
        var rivalWord = new VariableDefinition(module.TypeSystem.String);
        var lead = new VariableDefinition(module.TypeSystem.String);
        var connect = new VariableDefinition(module.TypeSystem.String);
        var parts = new VariableDefinition(new ArrayType(module.TypeSystem.String));
        var scenarioBody = new VariableDefinition(module.TypeSystem.String);
        var suffix = new VariableDefinition(module.TypeSystem.String);
        var choices = new VariableDefinition(arrayListType);
        var choice = new VariableDefinition(choiceType);
        foreach (var v in new[] { placement, driver, driverName, rivalWord, lead, connect, parts,
                                  scenarioBody, suffix, choices, choice })
            body.Variables.Add(v);

        // HasDismissButton = false;
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stfld, hasDismissButtonField));

        // ExteriorTitle = "Cup Series";
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Cup Series"));
        il.Add(Instruction.Create(OpCodes.Stfld, exteriorTitleField));

        // Title = _activeRace.HasAdditionalTracks() ? "Race Results" : "Cup results";
        var titleRaceResults = Instruction.Create(OpCodes.Ldarg_0);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, hasAdditionalTracks));
        il.Add(Instruction.Create(OpCodes.Brtrue, titleRaceResults));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Cup results"));
        il.Add(Instruction.Create(OpCodes.Stfld, titleField));
        var titleDone = Instruction.Create(OpCodes.Ldarg_0);
        il.Add(Instruction.Create(OpCodes.Br, titleDone));
        il.Add(titleRaceResults);
        il.Add(Instruction.Create(OpCodes.Ldstr, "Race Results"));
        il.Add(Instruction.Create(OpCodes.Stfld, titleField));
        il.Add(titleDone);

        // driver = _activeRace.raceCupRegistration.simOpponents[0];
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Ldflda, registrationField));
        il.Add(Instruction.Create(OpCodes.Ldfld, opponentsField));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Callvirt, getOpponent));
        il.Add(Instruction.Create(OpCodes.Stloc, driver));

        // driverName = driver.Name.FullName();
        il.Add(Instruction.Create(OpCodes.Ldloc, driver));
        il.Add(Instruction.Create(OpCodes.Ldfld, nameField));
        il.Add(Instruction.Create(OpCodes.Callvirt, fullName));
        il.Add(Instruction.Create(OpCodes.Stloc, driverName));

        // rivalWord = driver.IsRival ? "rival" : "fellow";
        var rivalTrue = Instruction.Create(OpCodes.Ldstr, "rival");
        il.Add(Instruction.Create(OpCodes.Ldloc, driver));
        il.Add(Instruction.Create(OpCodes.Ldfld, isRivalField));
        il.Add(Instruction.Create(OpCodes.Brtrue, rivalTrue));
        il.Add(Instruction.Create(OpCodes.Ldstr, "fellow"));
        il.Add(Instruction.Create(OpCodes.Stloc, rivalWord));
        var rivalDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, rivalDone));
        il.Add(rivalTrue);
        il.Add(Instruction.Create(OpCodes.Stloc, rivalWord));
        il.Add(rivalDone);

        // switch (_results.Placement)
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldflda, resultsField));
        il.Add(Instruction.Create(OpCodes.Ldfld, placementField));
        il.Add(Instruction.Create(OpCodes.Stloc, placement));

        var brokeDown = Instruction.Create(OpCodes.Ldstr,
            "Your vehicle broke down during the race at ");
        var wonRace = Instruction.Create(OpCodes.Ldstr, "You won the race on ");
        il.Add(Instruction.Create(OpCodes.Ldloc, placement));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Beq, wonRace));
        il.Add(Instruction.Create(OpCodes.Ldloc, placement));
        var lostRace = Instruction.Create(OpCodes.Ldstr, "You lost the race on ");
        il.Add(Instruction.Create(OpCodes.Brtrue, lostRace));   // != 0 && != 1

        // case 0: vehicle broke down.
        il.Add(brokeDown);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getActiveTrack));
        il.Add(Instruction.Create(OpCodes.Ldfld, trackNameField));
        il.Add(Instruction.Create(OpCodes.Ldstr, ", and you did not earn any points in the cup series."));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Stloc, scenarioBody));
        var switchDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, switchDone));

        // case 1 (won) and default (lost) share the string[7] shape.
        il.Add(wonRace);
        il.Add(Instruction.Create(OpCodes.Stloc, lead));
        il.Add(Instruction.Create(OpCodes.Ldstr, " against your "));
        il.Add(Instruction.Create(OpCodes.Stloc, connect));
        var leadJoin = Instruction.Create(OpCodes.Ldc_I4, 7);
        il.Add(Instruction.Create(OpCodes.Br, leadJoin));

        il.Add(lostRace);
        il.Add(Instruction.Create(OpCodes.Stloc, lead));
        il.Add(Instruction.Create(OpCodes.Ldstr, " to your "));
        il.Add(Instruction.Create(OpCodes.Stloc, connect));

        il.Add(leadJoin);
        il.Add(Instruction.Create(OpCodes.Newarr, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Stloc, parts));
        for (var i = 0; i < 7; i++)
        {
            il.Add(Instruction.Create(OpCodes.Ldloc, parts));
            il.Add(Instruction.Create(OpCodes.Ldc_I4, i));
            switch (i)
            {
                case 0: il.Add(Instruction.Create(OpCodes.Ldloc, lead)); break;
                case 1:
                    il.Add(Instruction.Create(OpCodes.Ldarg_0));
                    il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
                    il.Add(Instruction.Create(OpCodes.Callvirt, getActiveTrack));
                    il.Add(Instruction.Create(OpCodes.Ldfld, trackNameField));
                    break;
                case 2: il.Add(Instruction.Create(OpCodes.Ldloc, connect)); break;
                case 3: il.Add(Instruction.Create(OpCodes.Ldloc, rivalWord)); break;
                case 4: il.Add(Instruction.Create(OpCodes.Ldstr, " racer, ")); break;
                case 5: il.Add(Instruction.Create(OpCodes.Ldloc, driverName)); break;
                case 6: il.Add(Instruction.Create(OpCodes.Ldstr, ".")); break;
            }
            il.Add(Instruction.Create(OpCodes.Stelem_Ref));
        }
        il.Add(Instruction.Create(OpCodes.Ldloc, parts));
        il.Add(Instruction.Create(OpCodes.Call, concatArray));
        il.Add(Instruction.Create(OpCodes.Stloc, scenarioBody));
        il.Add(switchDone);

        // string suffix = null;
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, suffix));

        // if (!_activeRace.HasAdditionalTracks())
        //     suffix = String.Concat("\n\nThis was the final race in the ", _cup.Name,
        //                            " series. Head to the grandstand for the results!");
        var suffixDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, hasAdditionalTracks));
        il.Add(Instruction.Create(OpCodes.Brtrue, suffixDone));
        il.Add(Instruction.Create(OpCodes.Ldstr, "\n\nThis was the final race in the "));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, cupField));
        il.Add(Instruction.Create(OpCodes.Ldfld, cupNameField));
        il.Add(Instruction.Create(OpCodes.Ldstr, " series. Head to the grandstand for the results!"));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Stloc, suffix));
        il.Add(suffixDone);

        // Body = String.Concat(body, suffix);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, scenarioBody));
        il.Add(Instruction.Create(OpCodes.Ldloc, suffix));
        il.Add(Instruction.Create(OpCodes.Call, concat2));
        il.Add(Instruction.Create(OpCodes.Stfld, bodyField));

        // var choices = new ArrayList();
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListCtor));
        il.Add(Instruction.Create(OpCodes.Stloc, choices));

        // choices.Add(new ScenarioChoice(
        //     _activeRace.HasAdditionalTracks() ? "Continue to the next race" : "Attend the trophy ceremony",
        //     false, true));
        var continueNext = Instruction.Create(OpCodes.Ldstr, "Continue to the next race");
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, hasAdditionalTracks));
        il.Add(Instruction.Create(OpCodes.Brtrue, continueNext));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Attend the trophy ceremony"));
        var choiceJoin = Instruction.Create(OpCodes.Ldc_I4_0);
        il.Add(Instruction.Create(OpCodes.Br, choiceJoin));
        il.Add(continueNext);
        il.Add(choiceJoin);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Newobj, choiceCtor));
        il.Add(Instruction.Create(OpCodes.Stloc, choice));
        il.Add(Instruction.Create(OpCodes.Ldloc, choices));
        il.Add(Instruction.Create(OpCodes.Ldloc, choice));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));

        // BuildChoiceIndexDictionary(choices);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, choices));
        il.Add(Instruction.Create(OpCodes.Call, buildChoiceIndexDictionary));

        // TableDictionary = CreatePointTotalTable();
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, createPointTotalTable));
        il.Add(Instruction.Create(OpCodes.Stfld, tableDictionaryField));

        // TableOrderArray = CreateTableOrder();
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, createTableOrder));
        il.Add(Instruction.Create(OpCodes.Stfld, tableOrderArrayField));

        il.Add(Instruction.Create(OpCodes.Ret));
    }

    // System.Void ScenarioRacingCupSeriesCircuitRaceResults::RefreshScenario()  (native 0x3960e7c)
    // System.Void ScenarioRacingCupSeriesDemolitionRaceResults::RefreshScenario() (native 0x3964494)
    //
    // The two methods are the same program with two literals changed ("race" -> "derby");
    // both are recovered by FixResultsRefreshScenario below. Native notes:
    //  - circuit ends at the ret at 0x39612d0, demolition at 0x39648d4 (the enumerator loops
    //    seen in the earlier cross-check belong to the class helpers CreateTableOrder /
    //    CreatePointTotalDictionary, not to RefreshScenario itself);
    //  - demolition's own CreatePointTotalTable/CreateTableOrder are 0x3964d8c/0x39648f8,
    //    circuit's are 0x3961854/0x39612e0 (resolved via the method pointer table, not hard-coded).
    //
    // Recovered C# (native-faithful, shared):
    //
    //   HasDismissButton = false;
    //   ExteriorTitle = "Cup Series";
    //   Title = _activeRace.HasAdditionalTracks() ? "Race Results" : "Cup results";
    //
    //   switch (_results.Placement)                                   // [this+0x178]
    //   {
    //       case 0:   // vehicle broke down
    //           body = String.Concat("Your vehicle broke down during the race at ",
    //                                 _activeRace.ActiveTrack.TrackName,
    //                                 ", and you did not earn any points in the cup series.");
    //           break;
    //       case 1:   // won
    //           body = String.Concat("You won the race at ",
    //                                 _activeRace.ActiveTrack.TrackName,
    //                                 " and earned points in the cup series.");
    //           break;
    //       default:  // placed 2nd+
    //           body = String.Concat(new string[] {
    //               "You finished in ", SimRacing.GetPlacementString(_results.Placement),
    //               " in the race at ", _activeRace.ActiveTrack.TrackName,
    //               " and earned points in the cup series." });   // new string[5], Concat(string[])
    //           break;
    //   }
    //
    //   string suffix = null;
    //   if (!_activeRace.HasAdditionalTracks())
    //       suffix = String.Concat("\n\nThis was the final race in the ", _cup.Name,
    //                              " series. Head to the grandstand for the results!");
    //   Body = String.Concat(body, suffix);
    //
    //   var choices = new ArrayList();
    //   choices.Add(new ScenarioChoice(
    //       _activeRace.HasAdditionalTracks() ? "Continue to the next race" : "Attend the trophy ceremony",
    //       false, true));
    //   BuildChoiceIndexDictionary(choices);
    //   TableDictionary = CreatePointTotalTable();      // circuit's own 0x3961854
    //   TableOrderArray = CreateTableOrder();           // circuit's own 0x39612e0
    //
    //   if (!_activeRace.HasAdditionalTracks())         // tail the street method does not have
    //   {
    //       var graphs = new ArrayList();
    //       graphs.Add(new ScenarioBarGraph("Vehicle Condition",          // ctor(string, float)
    //           _activeRace.raceCupRegistration.simRaceCar.GetCondition()));
    //       BarGraphArray = graphs;                     // [this+0xb8] Scenario::BarGraphArray
    //   }
    //
    // Notes: [activeRace+0x20] is reg.simRaceCar (registration struct @0x10: simRacing@0,
    // simRacingCup@0x8, simRaceCar@0x10, simOpponents@0x18). All literals decoded from
    // global-metadata.dat usage tokens as in FixStreetRefreshScenario.
    public static void FixCircuitRefreshScenario(MethodDefinition method)
    {
        FixResultsRefreshScenario(method, "You won the race at ", " in the race at ");
    }

    public static void FixDemolitionRefreshScenario(MethodDefinition method)
    {
        FixResultsRefreshScenario(method, "You won the derby at ", " in the derby at ");
    }

    private static void FixResultsRefreshScenario(MethodDefinition method, string wonLead, string connectLiteral)
    {
        var module = method.Module;
        var owner = method.DeclaringType;

        var activeRaceField = Field(module, owner, "_activeRace");
        var activeRaceType = activeRaceField.FieldType;
        var cupField = Field(module, owner, "_cup");
        var resultsField = Field(module, owner, "_results");
        var resultsType = resultsField.FieldType;
        var registrationField = Field(module, activeRaceType, "raceCupRegistration");
        var raceCarField = Field(module, registrationField.FieldType, "simRaceCar");

        var hasAdditionalTracks = ImportMethod(module, activeRaceType, "HasAdditionalTracks", 0, true);
        var getActiveTrack = ImportMethod(module, activeRaceType, "get_ActiveTrack", 0, true);
        var placementField = Field(module, resultsType, "Placement");
        var trackNameField = Field(module, getActiveTrack.ReturnType, "<TrackName>k__BackingField");
        var cupNameField = Field(module, cupField.FieldType, "<Name>k__BackingField");
        var getPlacementString = ImportMethod(module, FindType(module, "SimRacing"),
            "GetPlacementString", 1, false, "System.Int32");
        var getCondition = ImportMethod(module, raceCarField.FieldType, "GetCondition", 0, true);

        var concat3 = FrameworkMethod(module, "System.String", "Concat", 3, false,
            "System.String", "System.String", "System.String");
        var concatArray = FrameworkMethod(module, "System.String", "Concat", 1, false, "System.String[]");
        var concat2 = FrameworkMethod(module, "System.String", "Concat", 2, false,
            "System.String", "System.String");

        var arrayListType = FrameworkType(module, "System.Collections", "ArrayList");
        var arrayListCtor = ImportMethod(module, arrayListType, ".ctor", 0);
        var arrayListAdd = ImportMethod(module, arrayListType, "Add", 1, true, "System.Object");
        var choiceType = FindType(module, "ScenarioChoice");
        var choiceCtor = ImportMethod(module, choiceType, ".ctor", 3, null,
            "System.String", "System.Boolean", "System.Boolean");
        var barGraphType = FindType(module, "ScenarioBarGraph");
        var barGraphCtor = ImportMethod(module, barGraphType, ".ctor", 2, null,
            "System.String", "System.Single");
        var buildChoiceIndexDictionary = ImportMethod(module, owner, "BuildChoiceIndexDictionary", 1, true,
            "System.Collections.ArrayList");
        var createPointTotalTable = ImportMethod(module, owner, "CreatePointTotalTable", 0, true);
        var createTableOrder = ImportMethod(module, owner, "CreateTableOrder", 0, true);

        var hasDismissButtonField = Field(module, owner, "HasDismissButton");
        var exteriorTitleField = Field(module, owner, "ExteriorTitle");
        var titleField = Field(module, owner, "Title");
        var bodyField = Field(module, owner, "Body");
        var tableDictionaryField = Field(module, owner, "TableDictionary");
        var tableOrderArrayField = Field(module, owner, "TableOrderArray");
        var barGraphArrayField = Field(module, owner, "BarGraphArray");

        var body = FreshBody(method);
        var il = body.Instructions;

        var placement = new VariableDefinition(module.TypeSystem.Int32);
        var parts = new VariableDefinition(new ArrayType(module.TypeSystem.String));
        var scenarioBody = new VariableDefinition(module.TypeSystem.String);
        var suffix = new VariableDefinition(module.TypeSystem.String);
        var choices = new VariableDefinition(arrayListType);
        var choice = new VariableDefinition(choiceType);
        var graphs = new VariableDefinition(arrayListType);
        var graph = new VariableDefinition(barGraphType);
        foreach (var v in new[] { placement, parts, scenarioBody, suffix, choices, choice, graphs, graph })
            body.Variables.Add(v);

        // HasDismissButton = false; ExteriorTitle = "Cup Series";
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Stfld, hasDismissButtonField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Cup Series"));
        il.Add(Instruction.Create(OpCodes.Stfld, exteriorTitleField));

        // Title = _activeRace.HasAdditionalTracks() ? "Race Results" : "Cup results";
        var titleRaceResults = Instruction.Create(OpCodes.Ldarg_0);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, hasAdditionalTracks));
        il.Add(Instruction.Create(OpCodes.Brtrue, titleRaceResults));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Cup results"));
        il.Add(Instruction.Create(OpCodes.Stfld, titleField));
        var titleDone = Instruction.Create(OpCodes.Ldarg_0);
        il.Add(Instruction.Create(OpCodes.Br, titleDone));
        il.Add(titleRaceResults);
        il.Add(Instruction.Create(OpCodes.Ldstr, "Race Results"));
        il.Add(Instruction.Create(OpCodes.Stfld, titleField));
        il.Add(titleDone);

        // placement = _results.Placement
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Ldflda, resultsField));
        il.Add(Instruction.Create(OpCodes.Ldfld, placementField));
        il.Add(Instruction.Create(OpCodes.Stloc, placement));

        // case 1: won
        var wonRace = Instruction.Create(OpCodes.Ldstr, wonLead);
        il.Add(Instruction.Create(OpCodes.Ldloc, placement));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Beq, wonRace));
        il.Add(Instruction.Create(OpCodes.Ldloc, placement));
        var lostRace = Instruction.Create(OpCodes.Ldc_I4, 5);
        il.Add(Instruction.Create(OpCodes.Brtrue, lostRace));   // != 0 && != 1

        // case 0: vehicle broke down.
        il.Add(Instruction.Create(OpCodes.Ldstr, "Your vehicle broke down during the race at "));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getActiveTrack));
        il.Add(Instruction.Create(OpCodes.Ldfld, trackNameField));
        il.Add(Instruction.Create(OpCodes.Ldstr, ", and you did not earn any points in the cup series."));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Stloc, scenarioBody));
        var switchDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Br, switchDone));

        // case 1 (won)
        il.Add(wonRace);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getActiveTrack));
        il.Add(Instruction.Create(OpCodes.Ldfld, trackNameField));
        il.Add(Instruction.Create(OpCodes.Ldstr, " and earned points in the cup series."));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Stloc, scenarioBody));
        il.Add(Instruction.Create(OpCodes.Br, switchDone));

        // default: placed 2nd+
        il.Add(lostRace);
        il.Add(Instruction.Create(OpCodes.Newarr, module.TypeSystem.String));
        il.Add(Instruction.Create(OpCodes.Stloc, parts));
        il.Add(Instruction.Create(OpCodes.Ldloc, parts));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_0));
        il.Add(Instruction.Create(OpCodes.Ldstr, "You finished in "));
        il.Add(Instruction.Create(OpCodes.Stelem_Ref));
        il.Add(Instruction.Create(OpCodes.Ldloc, parts));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Ldloc, placement));
        il.Add(Instruction.Create(OpCodes.Call, getPlacementString));
        il.Add(Instruction.Create(OpCodes.Stelem_Ref));
        il.Add(Instruction.Create(OpCodes.Ldloc, parts));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_2));
        il.Add(Instruction.Create(OpCodes.Ldstr, connectLiteral));
        il.Add(Instruction.Create(OpCodes.Stelem_Ref));
        il.Add(Instruction.Create(OpCodes.Ldloc, parts));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_3));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getActiveTrack));
        il.Add(Instruction.Create(OpCodes.Ldfld, trackNameField));
        il.Add(Instruction.Create(OpCodes.Stelem_Ref));
        il.Add(Instruction.Create(OpCodes.Ldloc, parts));
        il.Add(Instruction.Create(OpCodes.Ldc_I4_4));
        il.Add(Instruction.Create(OpCodes.Ldstr, " and earned points in the cup series."));
        il.Add(Instruction.Create(OpCodes.Stelem_Ref));
        il.Add(Instruction.Create(OpCodes.Ldloc, parts));
        il.Add(Instruction.Create(OpCodes.Call, concatArray));
        il.Add(Instruction.Create(OpCodes.Stloc, scenarioBody));
        il.Add(switchDone);

        // string suffix = null; if (!HasAdditionalTracks()) suffix = final-race text;
        il.Add(Instruction.Create(OpCodes.Ldnull));
        il.Add(Instruction.Create(OpCodes.Stloc, suffix));
        var suffixDone = Instruction.Create(OpCodes.Nop);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, hasAdditionalTracks));
        il.Add(Instruction.Create(OpCodes.Brtrue, suffixDone));
        il.Add(Instruction.Create(OpCodes.Ldstr, "\n\nThis was the final race in the "));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, cupField));
        il.Add(Instruction.Create(OpCodes.Ldfld, cupNameField));
        il.Add(Instruction.Create(OpCodes.Ldstr, " series. Head to the grandstand for the results!"));
        il.Add(Instruction.Create(OpCodes.Call, concat3));
        il.Add(Instruction.Create(OpCodes.Stloc, suffix));
        il.Add(suffixDone);

        // Body = String.Concat(body, suffix);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, scenarioBody));
        il.Add(Instruction.Create(OpCodes.Ldloc, suffix));
        il.Add(Instruction.Create(OpCodes.Call, concat2));
        il.Add(Instruction.Create(OpCodes.Stfld, bodyField));

        // var choices = new ArrayList(); choices.Add(new ScenarioChoice(..., false, true));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListCtor));
        il.Add(Instruction.Create(OpCodes.Stloc, choices));
        var continueNext = Instruction.Create(OpCodes.Ldstr, "Continue to the next race");
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, hasAdditionalTracks));
        il.Add(Instruction.Create(OpCodes.Brtrue, continueNext));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Attend the trophy ceremony"));
        var choiceJoin = Instruction.Create(OpCodes.Ldc_I4_0);
        il.Add(Instruction.Create(OpCodes.Br, choiceJoin));
        il.Add(continueNext);
        il.Add(choiceJoin);
        il.Add(Instruction.Create(OpCodes.Ldc_I4_1));
        il.Add(Instruction.Create(OpCodes.Newobj, choiceCtor));
        il.Add(Instruction.Create(OpCodes.Stloc, choice));
        il.Add(Instruction.Create(OpCodes.Ldloc, choices));
        il.Add(Instruction.Create(OpCodes.Ldloc, choice));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));

        // BuildChoiceIndexDictionary(choices);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, choices));
        il.Add(Instruction.Create(OpCodes.Call, buildChoiceIndexDictionary));

        // TableDictionary = CreatePointTotalTable(); TableOrderArray = CreateTableOrder();
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, createPointTotalTable));
        il.Add(Instruction.Create(OpCodes.Stfld, tableDictionaryField));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Call, createTableOrder));
        il.Add(Instruction.Create(OpCodes.Stfld, tableOrderArrayField));

        // if (!_activeRace.HasAdditionalTracks()) { graphs = ...; BarGraphArray = graphs; }
        var tailDone = Instruction.Create(OpCodes.Ret);
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Callvirt, hasAdditionalTracks));
        il.Add(Instruction.Create(OpCodes.Brtrue, tailDone));
        il.Add(Instruction.Create(OpCodes.Newobj, arrayListCtor));
        il.Add(Instruction.Create(OpCodes.Stloc, graphs));
        il.Add(Instruction.Create(OpCodes.Ldstr, "Vehicle Condition"));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldfld, activeRaceField));
        il.Add(Instruction.Create(OpCodes.Ldflda, registrationField));
        il.Add(Instruction.Create(OpCodes.Ldfld, raceCarField));
        il.Add(Instruction.Create(OpCodes.Callvirt, getCondition));
        il.Add(Instruction.Create(OpCodes.Newobj, barGraphCtor));
        il.Add(Instruction.Create(OpCodes.Stloc, graph));
        il.Add(Instruction.Create(OpCodes.Ldloc, graphs));
        il.Add(Instruction.Create(OpCodes.Ldloc, graph));
        il.Add(Instruction.Create(OpCodes.Callvirt, arrayListAdd));
        il.Add(Instruction.Create(OpCodes.Pop));
        il.Add(Instruction.Create(OpCodes.Ldarg_0));
        il.Add(Instruction.Create(OpCodes.Ldloc, graphs));
        il.Add(Instruction.Create(OpCodes.Stfld, barGraphArrayField));
        il.Add(tailDone);
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
        return new TypeReference(typeNamespace, typeName, module, scope);
    }

    private static MethodReference FrameworkMethod(ModuleDefinition module, string typeName, string methodName,
        int parameterCount, bool instance, params string[] parameterTypes)
    {
        var separator = typeName.LastIndexOf('.');
        var type = FrameworkType(module, typeName.Substring(0, separator), typeName.Substring(separator + 1));
        return ImportMethod(module, type, methodName, parameterCount, instance, parameterTypes);
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
