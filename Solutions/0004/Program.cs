int Solution(int digits)
    => Palindomes(digits).Max();

IEnumerable<int> Palindomes(int digits)
{
    var hi = (int)Math.Pow(10, digits) - 1;
    var lo = (int)Math.Pow(10, digits - 1);

    for (var i = lo; i <= hi; i++)
    {
        for (var j = i; j <= hi; j++)
        {
            var product = i * j;
            if (IsPalindrome(product))
            {
                yield return product;
            }
        }
    }
}

bool IsPalindrome(int number)
{
    var str = number.ToString();
    var half = str.Length / 2;
    return str[..half] == string.Join(string.Empty, str[(^half)..].Reverse());
}

Console.WriteLine(Solution(2)); // 9009
Console.WriteLine(Solution(3));