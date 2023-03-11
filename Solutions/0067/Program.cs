using System.Reflection;

int Solution()
{
    var path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, "triangle.txt");
    var str = File.ReadAllText(path);
    var grid = str
        .Split(Environment.NewLine)
        .Select(l => l
            .Split(" ")
            .Select(int.Parse)
            .ToArray())
        .ToArray();

    var cache = new Dictionary<(int X, int Y), int>();

    return Dfs(0, 0);

    int Dfs(int x, int y)
    {
        var number = grid[y][x];
        if (y == grid.Length - 1)
        {
            return number;
        }

        if (cache.TryGetValue((x, y), out var cached))
        {
            return cached;
        }

        var max = Math.Max(Dfs(x, y + 1), Dfs(x + 1, y + 1)) + number;

        cache[(x, y)] = max;
        return max;
    }
}

Console.WriteLine(Solution());