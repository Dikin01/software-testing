namespace Tests.TestUtils;

public static class HexExpressionUtils
{
    public static byte[] ConvertToBytes(string hexExpression)
    {
        var expression = hexExpression.Trim();
        if (!expression.StartsWith("0x"))
            throw new FormatException("Expression should start with 0x");
        expression = new string(expression
            .Skip(2)
            .SkipWhile(symbol => symbol == '0')
            .ToArray());

        var range = Enumerable.Range(0, expression.Length / 2);
        var result = new List<byte>();
        foreach (var item in range)
        {
            var partExpression = expression.Substring(item * 2, 2);
            var itemByte = Convert.ToByte(partExpression, 16);
            result.Add(itemByte);
        }

        return result.ToArray();
    }

    public static bool CompareAsHexExpression(string a, string b)
    {
        var aBytes = ConvertToBytes(a);
        var bBytes = ConvertToBytes(b);
        return aBytes.SequenceEqual(bBytes);
    }
}