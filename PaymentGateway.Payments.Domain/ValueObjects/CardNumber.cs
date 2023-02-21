namespace PaymentGateway.Payments.Domain.ValueObjects;

public record CardNumber(string Number)
{
    public string Obfuscate()
    {
        Span<char> chars = stackalloc char[16];
        var curr = 0;

        while (curr < 6)
        {
            chars[curr] = Number[curr];
            curr++;
        }
        
        while (curr < 12)
        {
            chars[curr] = '*';
            curr++;
        }

        while (curr < Number.Length)
        {
            chars[curr] = Number[curr];
            curr++;
        }

        return new string(chars);
    }
}