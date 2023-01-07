int Solution()
{
    var k = 2;

    while (true)
    {
        var hi = Pentagonal(k);
        for (var i = 1; i < k; i++)
        {
            var lo = Pentagonal(i);
            var sum = hi + lo;
            var diff = hi - lo;

            if (IsPentagonal(sum) && IsPentagonal(diff))
            {
                return diff;
            }
        }

        k++;
    }

    throw new Exception();
}

int Pentagonal(int n) => (n * (3 * n - 1)) / 2;


bool IsPentagonal(int n)
    => Math.Sqrt(24 * n + 1) % 6 == 5;


Console.WriteLine(Solution());
