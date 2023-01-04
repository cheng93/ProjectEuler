int Solution(int limit)
    => DoubleBasePalindromes()
        .TakeWhile(x => x < limit)
        .Sum();

IEnumerable<int> DoubleBasePalindromes()
{
    var i = 1;
    while (true)
    {
        if (IsPalindrome(i.ToString()))
        {
            var binary = Convert.ToString(i, 2);
            if (IsPalindrome(binary))
            {
                yield return i;
            }
        }
        i += 2;
    }
}

bool IsPalindrome(string str)
{
    var half = str.Length / 2;
    return str[..half] == string.Join(string.Empty, str[(^half)..].Reverse());
}

Console.WriteLine(Solution(1000000));
