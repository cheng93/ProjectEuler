string Solution(int[] digits, int n)
{
    var copy = digits.ToList();
    var permuation = string.Empty;
    n = n - 1;
    while (copy.Any())
    {
        var permutations = Factorial(copy.Count - 1);
        var digit = copy[n / permutations];
        permuation += digit;
        copy.Remove(digit);
        n = n % permutations;
    }

    return permuation;
}

int Factorial(int n)
{
    var product = 1;
    for (var i = 1; i <= n; i++)
    {
        product *= i;
    }
    return product;
}

Console.WriteLine(Solution(new[] { 0, 1, 2 }, 4)); // 120
Console.WriteLine(Solution(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 1000000));