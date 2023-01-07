int Solution() =>
    Enumerable
        .Range(1, 1000000)
        .MaxBy(x => Collatz(x).Count());

var cache = new Dictionary<long, ICollection<long>>();
IEnumerable<long> Collatz(long number)
{
    if (number == 1)
    {
        return new long[] { 1 };
    }

    if (cache.TryGetValue(number, out var cached))
    {
        return cached;
    }
    var next = number % 2 == 0
        ? number / 2
        : 3 * number + 1;

    var collatz = Collatz(next);
    cache[number] = new[] { number }.Concat(collatz).ToArray();
    return cache[number];
}

Console.WriteLine(Solution());