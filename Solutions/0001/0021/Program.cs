int Solution(int limit) => Amicable().TakeWhile(x => x < limit).Sum();

IEnumerable<int> Amicable()
{
    var found = new HashSet<int>();

    var i = 1;
    while (true)
    {
        if (found.Contains(i))
        {
            yield return i;
        }
        else
        {
            var sum = SumOfProperDivisors(i);
            if (sum > i && SumOfProperDivisors(sum) == i)
            {
                yield return i;
                found.Add(sum);
            }
        }
        i++;
    }
}

int SumOfProperDivisors(int n)
{
    var properDivisors = Factors(n).Where(x => x != n);
    return properDivisors.Sum();
}

IEnumerable<int> Factors(int n)
{
    var sqrt = (int)Math.Sqrt(n);
    for (var i = 1; i <= sqrt; i++)
    {
        if (n % i == 0)
        {
            yield return i;
            if (i != n / i)
            {
                yield return n / i;
            }
        }
    }
}

Console.WriteLine(Solution(10000));