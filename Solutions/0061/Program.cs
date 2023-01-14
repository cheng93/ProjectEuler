int Solution()
{
    var sets = Enumerable.Range(0, 6)
        .Select(x => new HashSet<int>())
        .ToArray();
    var tests = new Func<int, bool>[]
    {
        IsTriangle,
        IsSquare,
        IsPentagonal,
        IsHexagonal,
        IsHeptagonal,
        IsOctagonal
    };

    for (var i = 1000; i < 10000; i++)
    {
        for (var j = 0; j < tests.Length; j++)
        {
            if (tests[j](i))
            {
                sets[j].Add(i);
            }
        }
    };

    return Dfs(new HashSet<int>(), new List<int>()).Sum();

    ICollection<int> Dfs(HashSet<int> tested, List<int> seen)
    {
        var end = seen.LastOrDefault() % 100;
        if (tested.Count == tests.Length && end == seen.First() / 100)
        {
            return seen;
        }

        for (var i = 0; i < tests.Length; i++)
        {
            if (tested.Contains(i))
            {
                continue;
            }

            var newTested = new HashSet<int>(tested);
            newTested.Add(i);

            foreach (var j in sets[i])
            {
                if (tested.Count == 0 || end == j / 100)
                {
                    var newSeen = seen.ToList();
                    newSeen.Add(j);
                    var set = Dfs(newTested, newSeen);
                    if (set.Any())
                    {
                        return set;
                    }
                }
            }
        }

        return Array.Empty<int>();
    }
}

bool IsTriangle(int n)
    => Math.Sqrt(8 * n + 1) % 2 == 1;

bool IsSquare(int n)
    => Math.Sqrt(n) % 1 == 0;

bool IsPentagonal(int n)
    => Math.Sqrt(24 * n + 1) % 6 == 5;

bool IsHexagonal(int n)
    => Math.Sqrt(8 * n + 1) % 4 == 3;

bool IsHeptagonal(int n)
    => Math.Sqrt(40 * n + 9) % 10 == 7;

bool IsOctagonal(int n)
    => Math.Sqrt(3 * n + 1) % 3 == 2;

Console.WriteLine(Solution());