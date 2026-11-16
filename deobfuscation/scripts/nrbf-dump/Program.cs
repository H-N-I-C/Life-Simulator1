using System.Formats.Nrbf;
using System.Text.Json;
using System.Reflection;

static object? Dump(SerializationRecord? rec)
{
    if (rec is null) return null;
    if (rec is ClassRecord c)
    {
        if (c.HasMember("_items") && c.HasMember("_size") && c.HasMember("_version"))
            return Dump(c.GetSerializationRecord("_items"));
        var d = new Dictionary<string, object?>();
        foreach (var m in c.MemberNames) d[m] = Member(c, m);
        return d;
    }
    if (rec is SZArrayRecord<string> sa) return sa.GetArray(true);
    if (rec is SZArrayRecord<SerializationRecord> ra) return ra.GetArray(true).Select(Dump).ToArray();
    if (rec is PrimitiveTypeRecord p) return p.Value;
    var prop = rec.GetType().GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
    return prop != null ? prop.GetValue(rec) : rec.ToString();
}

static object? Member(ClassRecord c, string name)
{
    var raw = c.GetRawValue(name);
    return raw is SerializationRecord r ? Dump(r) : raw;
}

if (args.Length < 2)
{
    Console.Error.WriteLine("usage: nrbf-dump <input> <output.json>");
    return 2;
}
using var fs = File.OpenRead(args[0]);
var root = NrbfDecoder.Decode(fs);
object? payload;
if (root is ClassRecord cr && cr.HasMember("_items")) payload = Dump(cr.GetSerializationRecord("_items"));
else payload = Dump(root);
var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
File.WriteAllText(args[1], json);
Console.WriteLine($"wrote {args[1]} ({json.Length} chars)");
return 0;
