using ConsoleApp1;
using System.Reflection;

static Dictionary<string, string> GetFQNForAllActivites()
    => AppDomain.CurrentDomain.GetAssemblies()
        .ToList()
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => type.IsDefined(typeof(ActivityAttribute), inherit: false) && type.FullName is not null)
        .ToDictionary(type => type.GetCustomAttribute<ActivityAttribute>()!.Name, type => type.FullName!);

var fqns = GetFQNForAllActivites();
Console.WriteLine(fqns.TryGetValue("DelayActivity", out string? fqn) ? fqn : "Not found");
Console.WriteLine(fqns.TryGetValue("LoggingActivity", out string? fqn2) ? fqn2 : "Not found");


