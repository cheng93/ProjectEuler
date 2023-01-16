long Solution(int n) => CubicPermutation(n).First();

IEnumerable<long> CubicPermutation(int n)
{
    Dictionary<string, long> firsts = new();
    Dictionary<string, long> counts = new();

    var i = 1;
    while (true)
    {
        var cubed = 1L * i * i * i;
        var str = string.Join(",", CountDigits(cubed));
        if (!firsts.TryGetValue(str, out var first))
        {
            firsts[str] = cubed;
            counts[str] = 1;
        }
        else
        {
            counts[str]++;
        }

        if (counts[str] == n)
        {
            yield return first;
        }

        i++;
    }
}

int[] CountDigits(long n)
{
    var counts = new int[10];

    while (n != 0)
    {
        counts[n % 10]++;
        n /= 10;
    }

    return counts;
}

Console.WriteLine(Solution(3)); // 41063625
Console.WriteLine(Solution(5));
