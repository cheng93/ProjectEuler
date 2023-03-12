using MoreLinq;

long Solution(int n, int maxDigits)
    => MagicGonRing(n)
        .Select(x => x.Set)
        .Where(x => (int)Math.Log10(x) + 1 == maxDigits)
        .Max();

IEnumerable<(long Set, int Total)> MagicGonRing(int n)
{
    var values = Enumerable.Range(1, n * 2);
    var outer = values.Subsets(n);
    var solutions = new Dictionary<long, int>();

    foreach (var o in outer)
    {
        var inner = values.Except(o);
        var ordered = o.OrderBy(x => x).ToList();

        foreach (var ip in inner.Permutations())
        {
            var triplet = new[] { ordered[0], ip[0], ip[1] };
            var initialTriplet = triplet;
            var total = triplet.Sum();
            foreach (var op in ordered.Skip(1).Permutations())
            {
                var keyList = new List<int>();
                keyList.Add((int)ToNumber(initialTriplet));
                for (var i = 1; i < n; i++)
                {
                    triplet = new[] { op[i - 1], ip[i % n], ip[(i + 1) % n] };
                    if (triplet.Sum() != total)
                    {
                        break;
                    }
                    keyList.Add((int)ToNumber(triplet));
                }
                if (keyList.Count == n)
                {
                    yield return (ToNumber(keyList), total);
                }
            }
        }
    }
}

long ToNumber(IEnumerable<int> values)
    => values.Aggregate(0L, (agg, cur) =>
    {
        var log10 = (int)Math.Log10(cur);
        var n = agg * (long)Math.Pow(10, log10 + 1);
        return n + cur;
    });

Console.WriteLine(Solution(3, 9)); // 432621513
Console.WriteLine(Solution(5, 16));