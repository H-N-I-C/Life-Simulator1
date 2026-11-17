using Mono.Cecil;

if (args.Length < 2)
{
    Console.Error.WriteLine("Usage: IlMethodDump <assembly.dll> <method-name-substring> [search-directory ...]");
    return 2;
}
var resolver = new DefaultAssemblyResolver();
resolver.AddSearchDirectory(Path.GetDirectoryName(Path.GetFullPath(args[0]))!);
foreach (var directory in args.Skip(2).Where(Directory.Exists)) resolver.AddSearchDirectory(directory);
using var assembly = AssemblyDefinition.ReadAssembly(args[0], new ReaderParameters { InMemory = true, AssemblyResolver = resolver });
foreach (var type in assembly.MainModule.Types.SelectMany(Flatten))
foreach (var method in type.Methods)
{
    if (!method.FullName.Contains(args[1], StringComparison.Ordinal)) continue;
    Console.WriteLine($"METHOD {method.FullName}");
    Console.WriteLine($"FLAGS Static={method.IsStatic} HasThis={method.HasThis} ExplicitThis={method.ExplicitThis} Parameters={method.Parameters.Count}");
    if (!method.HasBody) continue;
    foreach (var variable in method.Body.Variables) Console.WriteLine($"LOCAL {variable.Index}: {variable.VariableType.FullName}");
    foreach (var instruction in method.Body.Instructions) Console.WriteLine(instruction);
    foreach (var handler in method.Body.ExceptionHandlers) Console.WriteLine($"EH {handler.HandlerType} try {handler.TryStart}-{handler.TryEnd} handler {handler.HandlerStart}-{handler.HandlerEnd} catch {handler.CatchType}");
}
return 0;

static IEnumerable<TypeDefinition> Flatten(TypeDefinition type)
{
    yield return type;
    foreach (var nested in type.NestedTypes)
    foreach (var descendant in Flatten(nested)) yield return descendant;
}
