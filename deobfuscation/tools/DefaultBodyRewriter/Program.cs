using Mono.Cecil;
using Mono.Cecil.Cil;

// Replaces the body of each listed method with a valid default-return body.
//
//   DefaultBodyRewriter <assembly.dll> <methods.txt>            # dry run, writes nothing
//   DefaultBodyRewriter <assembly.dll> <methods.txt> --apply    # rewrites the assembly
//
// This is the option-2 tool described in herdr-reports/HANDOFF.md section 15: it makes an
// IL2CPP-invalid method convertible at the cost of fidelity (the method returns default).
// It must not be run on gameplay methods without the user's authorisation.
if (args.Length < 2)
{
    Console.Error.WriteLine("Usage: DefaultBodyRewriter <assembly.dll> <methods.txt> [--apply]");
    return 2;
}
var assemblyPath = Path.GetFullPath(args[0]);
var listPath = Path.GetFullPath(args[1]);
var apply = args.Contains("--apply");
var referenceDirectories = args.Skip(2).Where(argument => !argument.StartsWith("--") && Directory.Exists(argument)).ToList();
if (!File.Exists(assemblyPath) || !File.Exists(listPath))
{
    Console.Error.WriteLine("assembly or method list not found");
    return 2;
}

var wanted = File.ReadAllLines(listPath)
    .Select(line => line.Trim())
    .Where(line => line.Length > 0 && !line.StartsWith("#"))
    .ToList();

var resolver = new DefaultAssemblyResolver();
resolver.AddSearchDirectory(Path.GetDirectoryName(assemblyPath)!);
foreach (var referenceDirectory in referenceDirectories) resolver.AddSearchDirectory(referenceDirectory);
using var assembly = AssemblyDefinition.ReadAssembly(assemblyPath, new ReaderParameters { InMemory = true, AssemblyResolver = resolver });

var byName = new Dictionary<string, MethodDefinition>(StringComparer.Ordinal);
foreach (var type in assembly.MainModule.Types.SelectMany(Flatten))
foreach (var method in type.Methods)
    byName[method.FullName] = method;

var missing = new List<string>();
var rewritten = new List<string>();
foreach (var name in wanted)
{
    if (!byName.TryGetValue(name, out var method))
    {
        missing.Add(name + " (not found)");
        continue;
    }
    if (!method.HasBody)
    {
        missing.Add(name + " (no body)");
        continue;
    }
    ReplaceWithDefault(method);
    rewritten.Add(method.FullName);
}

Console.WriteLine($"requested {wanted.Count}, rewritten {rewritten.Count}, missing {missing.Count}");
foreach (var name in missing) Console.WriteLine("  MISSING " + name);
if (!apply)
{
    Console.WriteLine("dry run: the assembly was not written (pass --apply to write it)");
    return missing.Count == 0 ? 0 : 1;
}
var temporary = assemblyPath + ".default-body.tmp";
assembly.Write(temporary);
File.Move(temporary, assemblyPath, true);
Console.WriteLine("assembly written");
return missing.Count == 0 ? 0 : 1;

static void ReplaceWithDefault(MethodDefinition method)
{
    var body = new MethodBody(method) { InitLocals = true, MaxStackSize = 8 };
    method.Body = body;
    var il = body.Instructions;
    if (method.ReturnType.MetadataType == MetadataType.Void)
    {
        il.Add(Instruction.Create(OpCodes.Ret));
        return;
    }
    var result = new VariableDefinition(method.ReturnType);
    body.Variables.Add(result);
    il.Add(Instruction.Create(OpCodes.Ldloca_S, result));
    il.Add(Instruction.Create(OpCodes.Initobj, method.ReturnType));
    il.Add(Instruction.Create(OpCodes.Ldloc_0));
    il.Add(Instruction.Create(OpCodes.Ret));
}

static IEnumerable<TypeDefinition> Flatten(TypeDefinition type)
{
    yield return type;
    foreach (var nested in type.NestedTypes)
    foreach (var descendant in Flatten(nested)) yield return descendant;
}
