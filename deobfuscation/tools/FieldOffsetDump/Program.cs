using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

// Dump instance-field offsets for named types using il2cpp layout rules
// (header 0x10: klass+monitor; base-class fields first; declaration order;
// natural alignment). Calibrated against known native offsets.
internal static class Program
{
    private static int SizeOf(TypeReference t)
    {
        t = t.GetElementType();
        switch (t.MetadataType)
        {
            case MetadataType.Boolean:
            case MetadataType.SByte:
            case MetadataType.Byte:
                return 1;
            case MetadataType.Int16:
            case MetadataType.UInt16:
            case MetadataType.Char:
                return 2;
            case MetadataType.Int32:
            case MetadataType.UInt32:
            case MetadataType.Single:
                return 4;
            case MetadataType.Int64:
            case MetadataType.UInt64:
            case MetadataType.Double:
                return 8;
            default:
                return t.IsValueType ? 0 : 8; // 0 => struct: measure recursively
        }
    }

    private static int StructSize(TypeDefinition td, HashSet<string> seen)
    {
        if (!seen.Add(td.FullName)) return 0;
        int size = 0;
        foreach (var f in td.Fields.Where(f => !f.IsStatic))
        {
            int s = SizeOf(f.FieldType);
            if (s == 0)
            {
                var def = f.FieldType.Resolve();
                s = def != null ? StructSize(def, seen) : 8;
            }
            size = (size + s - 1) / s * s + s;
        }
        return size;
    }

    private static IEnumerable<(string name, int off, string type)> Layout(TypeDefinition td)
    {
        var chain = new List<TypeDefinition>();
        for (var t = td; t != null; t = t.BaseType?.Resolve()) chain.Add(t);
        chain.Reverse();
        int off = 0x10;
        var seen = new HashSet<string>();
        foreach (var t in chain)
        {
            foreach (var f in t.Fields)
            {
                if (f.IsStatic) continue;
                int s = SizeOf(f.FieldType);
                if (s == 0)
                {
                    var def = f.FieldType.Resolve();
                    s = def != null ? StructSize(def, new HashSet<string>()) : 8;
                    if (s == 0) s = 8;
                }
                off = (off + s - 1) / s * s;
                yield return ($"{t.Name}::{f.Name}", off, f.FieldType.FullName);
                off += s;
            }
        }
    }

    private static void Main(string[] args)
    {
        var asm = AssemblyDefinition.ReadAssembly(args[0]);
        var module = asm.MainModule;
        foreach (var name in args.Skip(1))
        {
            var td = module.GetType(name) ?? (TypeDefinition)module.GetType("Game", name);
            if (td == null)
            {
                var hits = module.Types.Where(t => t.Name == name).ToList();
                if (hits.Count == 1) td = hits[0];
            }
            if (td == null) { Console.WriteLine($"== {name}: NOT FOUND"); continue; }
            Console.WriteLine($"== {td.FullName} (base {td.BaseType?.FullName})");
            foreach (var (n, off, type) in Layout(td))
                Console.WriteLine($"   0x{off:x3}  {n} : {type}");
        }
    }
}
