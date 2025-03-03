using BankAccount.Interfaces;

public enum BelgianCodeEnum
{
    B = 11,
    E = 14
}
class BelgianConvertAccount : IConvertAccount
{
    public string ConvertToIban(string bban)
    {
        string cleanBban = bban.Replace("-", "");
        int leading_zeros_count = 0;
        if (!long.TryParse(cleanBban, out long numericbban))
        {
            throw new ArgumentException("Invalid bban. , must contain twelve numbers (separated with dash)");
        }
        leading_zeros_count = 12 - numericbban.ToString().Length;

        cleanBban += "111400";

        numericbban = long.Parse(cleanBban);
        int modulo = (int)numericbban % 97;
        int ibanKey = 98 - modulo;



        string iban = $"BE{ibanKey:D2}{cleanBban}";


        string formattedIban = $"{iban.Substring(0, 4)}-{iban.Substring(4, 4)}-{iban.Substring(8, 4)}-{iban.Substring(12, 4)}";


        return formattedIban;
    }
}