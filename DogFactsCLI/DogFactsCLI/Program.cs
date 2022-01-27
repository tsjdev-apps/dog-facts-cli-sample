using System.Reflection;

var versionString = Assembly.GetEntryAssembly()?
    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
    .InformationalVersion.ToString();

Console.WriteLine($"dog-facts v{versionString}");
Console.WriteLine("---------------");

var dogFactsService = new DogFactsService();

if (args.Length == 0)
{
    var result = await dogFactsService.GetDogFactItemAsync();
    if (result?.Facts == null)
    {
        Console.WriteLine($"{Environment.NewLine}Something went wrong. Please try again.");
        return;
    }

    Console.WriteLine($"{Environment.NewLine}Here is your dog fact: {Environment.NewLine}{result.Facts.First()}");
}
else
{
    var success = int.TryParse(args[0], out var number);
    if (success)
    {
        var result = await dogFactsService.GetDogFactItemAsync(number);
        if (result?.Facts == null)
        {
            Console.WriteLine($"{Environment.NewLine}Something went wrong. Please try again.");
            return;
        }

        if (result.Facts.Count() > 1)
        {
            Console.WriteLine($"{Environment.NewLine}Here are your dog facts:");
        }
        else
        {
            Console.WriteLine($"{Environment.NewLine}Here is your dog fact:");
        }

        foreach (var fact in result.Facts)
        {
            Console.WriteLine($"{fact}{Environment.NewLine}");
        }
    }
    else
    {
        Console.WriteLine($"{Environment.NewLine}Usage:");
        Console.WriteLine($"  dog-facts <number>");
    }
}