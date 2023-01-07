int Solution(int length) => Triangle().First(x => Factors(x).Count() > length);

IEnumerable<int> Triangle()
{
    var sum = 0;
    var i = 1;
    while (true)
    {
        sum += i;
        yield return sum;
        i++;
    }
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

Console.WriteLine(Solution(5)); // 28
Console.WriteLine(Solution(500));